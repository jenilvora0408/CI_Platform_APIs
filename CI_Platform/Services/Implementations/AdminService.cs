using AutoMapper;
using Common.Constants;
using Common.CustomExceptions;
using Common.Enums;
using Common.Utils;
using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Services.Interfaces;
using System.Linq.Expressions;
using System.Net;

namespace Services.Implementations;

public class AdminService : BaseService<Admin>, IAdminService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHostEnvironment _env;

    public AdminService(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment env) : base(unitOfWork.AdminRepo, unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _env = env;
    }

    public async Task<PageListResponseDTO<AdminInfoDTO>> GetAllAsync(AdminListRequestDTO adminListRequest)
    {
        PageListRequestEntity<Admin> pageListRequestEntity = _mapper.Map<PageListRequestEntity<Admin>>(adminListRequest);

        pageListRequestEntity.IncludeExpressions = new Expression<Func<Admin, object>>[] { x => x.User };

        if (!string.IsNullOrEmpty(adminListRequest.SearchQuery))
        {
            string searchQuery = adminListRequest.SearchQuery.Trim().ToLower();

            pageListRequestEntity.Predicate =
                admin => admin.User.FirstName.Trim().ToLower().Contains(searchQuery)
                      || admin.User.LastName.Trim().ToLower().Contains(searchQuery)
                      || (admin.User.FirstName + " " + admin.User.LastName).Trim().ToLower().Contains(searchQuery)
                      || (admin.User.LastName + " " + admin.User.FirstName).Trim().ToLower().Contains(searchQuery)
                      || admin.User.Email.Contains(searchQuery);
        }

        PageListResponseDTO<Admin> pageListResponse = await _unitOfWork.AdminRepo.GetAllAsync(pageListRequestEntity);

        List<AdminInfoDTO> adminInfoDTOs = _mapper.Map<List<AdminInfoDTO>>(pageListResponse.Records);

        return new PageListResponseDTO<AdminInfoDTO>(pageListResponse.PageIndex, pageListResponse.PageSize, pageListResponse.TotalRecords, adminInfoDTOs);
    }

    public async Task UpsertAsync(AdminDTO adminDTO, long sessionUserId)
    {
        bool isExist = await _unitOfWork.AdminRepo.IsEntityExist(admin => admin.User.Email == adminDTO.Email && admin.Id != adminDTO.Id);

        if (isExist) throw new DataAlreadyExistsException(ExceptionMessage.ADMIN_ALREADY_EXIST);

        Admin? admin = adminDTO.Id != 0 ? await GetAsync(adminDTO.Id) : new Admin();
        if (adminDTO.Id == 0)
        {
            admin = _mapper.Map<Admin>(adminDTO);
            admin.User.Password = PasswordHelper.HashPassword(adminDTO.Password, out byte[] salt);
            admin.User.Salt = Convert.ToHexString(salt);
            admin.User.UserType = UserType.Admin;
            admin.CreatedBy = sessionUserId;
            admin.User.Status = UserStatus.ACTIVE;
        }
        else
        {
            admin.User.FirstName = adminDTO.FirstName;
            admin.User.LastName = adminDTO.LastName;
            admin.User.Status = adminDTO.Status ?? admin.User.Status;
        }
        admin.ModifiedBy = sessionUserId;

        if (adminDTO.Avatar != null)
        {
            if (adminDTO.Id == 0)
            {
                KeyValuePair<string, string> fileData = await new FileHepler(_env).UploadFileToDestination(adminDTO.Avatar, SystemConstant.DIR_AVATAR, admin.Id);
                admin.User.Avatar = fileData.Key;
            }
            else
            {
                bool isAvatarDeleted = new FileHepler(_env).DeleteFileFromDestination(admin.User.Avatar);
                if (isAvatarDeleted)
                {
                    KeyValuePair<string, string> fileData = await new FileHepler(_env).UploadFileToDestination(adminDTO.Avatar, SystemConstant.DIR_AVATAR, admin.Id);
                    admin.User.Avatar = fileData.Key;
                }
            }
        }

        if (adminDTO.Id == 0)
            await AddAsync(admin);
        else
            await UpdateAsync(admin);
    }

    public async Task<AdminInfoDTO> GetById(long id)
    {
        Admin? admin = await GetAsync(id);

        AdminInfoDTO? adminInfoDTO = _mapper.Map<AdminInfoDTO>(admin);

        return adminInfoDTO;
    }

    public async Task RemoveAsync(long id, long sessionUserId)
    {
        Admin? admin = await GetAsync(id);

        admin.User.Status = UserStatus.DELETED;
        admin.ModifiedBy = sessionUserId;

        await UpdateAsync(admin);
    }

    private async Task<Admin> GetAsync(long id)
    {
        Admin? admin = await _unitOfWork.AdminRepo.GetAsync(admin => admin.Id == id, new Expression<Func<Admin, object>>[] { x => x.User }) ?? throw new EntityNullException(ExceptionMessage.ADMIN_NOT_FOUND);

        return admin;
    }

    public async Task UpdateAvatarAsync(IFormFile avatar, long id)
    {
        if (!avatar.ContentType.Contains("image")) throw new BadImageFormatException(ExceptionMessage.AVATAR_NOT_VALID);

        Admin? admin = await _unitOfWork.AdminRepo.GetAsync(x => x.User.Id == id, new Expression<Func<Admin, object>>[] { x => x.User }) ?? throw new EntityNullException(ExceptionMessage.ADMIN_NOT_FOUND);

        new FileHepler(_env).DeleteFileFromDestination(admin.User.Avatar);

        KeyValuePair<string, string> fileData = await new FileHepler(_env).UploadFileToDestination(avatar, SystemConstant.DIR_AVATAR, id);

        admin.User.Avatar = fileData.Key;

        _unitOfWork.AdminRepo.Update(admin);
        await _unitOfWork.SaveAsync();
    }
}
