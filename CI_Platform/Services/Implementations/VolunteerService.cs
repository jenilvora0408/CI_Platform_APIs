using AutoMapper;
using Common.Constants;
using Common.CustomExceptions;
using Common.Enums;
using Common.Utils;
using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Services.Interfaces;
using System.Linq.Expressions;

namespace Services.Implementations;
public class VolunteerService : BaseService<Volunteer>, IVolunteerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHostEnvironment _env;

    public VolunteerService(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment env) : base(unitOfWork.VolunteerRepo, unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _env = env;
    }

    private async Task<string> AddAvatar(IFormFile avatar, long id)
    {
        KeyValuePair<string, string> fileData = await new FileHepler(_env).UploadFileToDestination(avatar, SystemConstant.DIR_AVATAR, id);
        return fileData.Key;
    }

    private async Task<string> UpdateAvatar(Volunteer volunteer, IFormFile avatar, long id)
    {
        bool isAvatarDeleted = new FileHepler(_env).DeleteFileFromDestination(volunteer.User.Avatar);
        if (!isAvatarDeleted) throw new FileNullException("Avatar not updated");
        return await AddAvatar(avatar, id);
    }

    private async Task<Volunteer> Create(VolunteerDTO dto, long sessionUserId)
    {
        Volunteer volunteer = _mapper.Map<Volunteer>(dto);

        volunteer.User.Password = PasswordHelper.HashPassword(dto.Password, out byte[] salt);
        volunteer.User.Salt = Convert.ToHexString(salt);
        volunteer.User.UserType = UserType.Volunteer;
        volunteer.CreatedBy = sessionUserId;
        volunteer.User.Status = UserStatus.ACTIVE;
        volunteer.ModifiedBy = sessionUserId;

        if (dto.Avatar != null)
        {
            volunteer.User.Avatar = await AddAvatar(dto.Avatar, sessionUserId);
        }

        await AddAsync(volunteer);
        return volunteer;
    }

    private async Task<Volunteer> Update(VolunteerDTO dto, long sessionUserId)
    {
        Volunteer volunteer = await Get(dto.Id);
        if (volunteer == null) throw new EntityNullException(ExceptionMessage.VOLUNTEER_NOT_FOUND);

        // Common for user property
        volunteer.User.FirstName = dto.FirstName;
        volunteer.User.LastName = dto.LastName;
        volunteer.User.Status = dto.Status ?? volunteer.User.Status;

        // Volunteer property
        volunteer.PhoneNumber = dto.PhoneNumber;
        volunteer.CityId = dto.CityId;
        volunteer.EmployeeId = !string.IsNullOrEmpty(dto.EmployeeId) ? dto.EmployeeId : volunteer.EmployeeId;
        volunteer.Department = !string.IsNullOrEmpty(dto.Department) ? dto.Department : volunteer.Department;
        volunteer.ProfileText = !string.IsNullOrEmpty(dto.ProfileText) ? dto.ProfileText : volunteer.ProfileText;
        volunteer.ModifiedBy = sessionUserId;

        if (dto.Avatar != null)
        {
            volunteer.User.Avatar = await UpdateAvatar(volunteer, dto.Avatar, dto.Id);
        }
        await UpdateAsync(volunteer);
        return volunteer;
    }


    public async Task UpsertAsync(VolunteerDTO dto, long sessionUserId)
    {
        bool isCityExists = await IsCityExists(dto.CityId);
        if (!isCityExists)
        {
            throw new DbUpdateException(ExceptionMessage.CITY_NOT_FOUND);
        }

        bool isVolunteerExist = await _unitOfWork.VolunteerRepo.IsEntityExist(volunteer => volunteer.User.Email == dto.Email && volunteer.Id != dto.Id);

        if (isVolunteerExist) throw new DataAlreadyExistsException(ExceptionMessage.VOLUNTEER_ALREADY_EXIST);

        if (dto.Id == 0)
        {
            await Create(dto, sessionUserId);
            return;
        }
        await Update(dto, sessionUserId);
    }

    public async Task RemoveAsync(long id, long sessionUserId)
    {
        Volunteer? volunteer = await Get(id) ?? throw new EntityNullException(ExceptionMessage.VOLUNTEER_NOT_FOUND);

        volunteer.User.Status = UserStatus.DELETED;
        volunteer.ModifiedBy = sessionUserId;

        await UpdateAsync(volunteer);
    }

    public async Task<VolunteerInfoDTO> GetByIdAsync(long id)
    {
        Expression<Func<Volunteer, object>>[] includeExpressions = new Expression<Func<Volunteer, object>>[]
        {
            v => v.User,
            v => v.City
        };

        string[] thenIncludeExpressions = new string[]
        {
            "City.Country"
        };

        Volunteer? volunteer = await _unitOfWork.VolunteerRepo.GetAsync(volunteer => volunteer.Id == id, includeExpressions, thenIncludeExpressions);

        if (volunteer == null) throw new EntityNullException(ExceptionMessage.VOLUNTEER_NOT_FOUND);
        VolunteerInfoDTO dto = _mapper.Map<VolunteerInfoDTO>(volunteer);
        return dto;
    }

    public async Task<PageListResponseDTO<VolunteerInfoDTO>> GetAllAsync(VolunteerListRequestDTO dto)
    {
        PageListRequestEntity<Volunteer> pageListRequestEntity = _mapper.Map<PageListRequestEntity<Volunteer>>(dto);

        pageListRequestEntity.IncludeExpressions = new Expression<Func<Volunteer, object>>[] { x => x.User, x => x.City };

        pageListRequestEntity.ThenIncludeExpressions = new string[] { "City.Country" };

        if (!string.IsNullOrEmpty(dto.SearchQuery))
        {
            string searchQuery = dto.SearchQuery.Trim().ToLower();

            pageListRequestEntity.Predicate =
                volunteer => volunteer.User.FirstName.Trim().ToLower().Contains(searchQuery)
                      || volunteer.User.LastName.Trim().ToLower().Contains(searchQuery)
                      || (volunteer.User.FirstName + " " + volunteer.User.LastName).Trim().ToLower().Contains(searchQuery)
                      || (volunteer.User.LastName + " " + volunteer.User.FirstName).Trim().ToLower().Contains(searchQuery)
                      || volunteer.User.Email.ToLower().Contains(searchQuery);
        }

        PageListResponseDTO<Volunteer> pageListResponse = await _unitOfWork.VolunteerRepo.GetAllAsync(pageListRequestEntity);

        List<VolunteerInfoDTO> list = _mapper.Map<List<VolunteerInfoDTO>>(pageListResponse.Records);

        return new PageListResponseDTO<VolunteerInfoDTO>(pageListResponse.PageIndex, pageListResponse.PageSize, pageListResponse.TotalRecords, list);
    }

    private async Task<Volunteer> Get(long id)
    {

        Expression<Func<Volunteer, object>>[] includeExpressions = new Expression<Func<Volunteer, object>>[]
        {
            v => v.User
        };

        Volunteer? volunteer = await _unitOfWork.VolunteerRepo.GetAsync(volunteer => volunteer.Id == id, includeExpressions);

        if (volunteer == null) throw new EntityNullException(ExceptionMessage.VOLUNTEER_NOT_FOUND);

        return volunteer;
    }


    public async Task<IEnumerable<DropdownDTO>> GetVolunteers()
    {
        PageListRequestEntity<Volunteer> pageListRequest = new()
        {
            Predicate = vol => vol.User.Status == UserStatus.ACTIVE && vol.User.UserType == UserType.Volunteer,
            IncludeExpressions = new Expression<Func<Volunteer, object>>[] { x => x.User },
            Selects = vol => new Volunteer()
            {
                Id = vol.Id,
                User = new User()
                {
                    FirstName = vol.User.FirstName,
                    LastName = vol.User.LastName
                }
            }
        };
        PageListResponseDTO<Volunteer> volunteers = await _unitOfWork.VolunteerRepo.GetAllAsync(pageListRequest);
        if (volunteers.Records.Count <= 0)
            return Enumerable.Empty<DropdownDTO>();

        List<DropdownDTO> volDTOs = _mapper.Map<List<DropdownDTO>>(volunteers.Records);
        return volDTOs;
    }


    //TODO: Generic
    private async Task<bool> IsCityExists(long cityId) => await _unitOfWork.CityRepo.IsEntityExist(city => city.Id == cityId && city.Status == CityStatus.ACTIVE);

    public async Task<Volunteer> GetVolunteerByUserId(long userId)
    {
        return await GetAsync(v => v.User.Id == userId && v.User.Status == UserStatus.ACTIVE);
    }

    #region Volunteer
    public async Task<VolunteerProfileInfoDTO> GetById(long id)
    {
        Volunteer? volunteer = await GetVolunteer(id);

        VolunteerProfileInfoDTO? profileDTO = _mapper.Map<VolunteerProfileInfoDTO>(volunteer);

        return profileDTO;
    }


    public async Task UpdateAsync(VolunteerProfileFormDTO dto)
    {
        Volunteer? volunteer = await _unitOfWork.VolunteerRepo.GetAsync(vol => vol.User.Id == dto.UserId, new Expression<Func<Volunteer, object>>[] { x => x.User, x => x.Skills }) ?? throw new EntityNullException(ExceptionMessage.VOLUNTEER_NOT_FOUND);

        if (!await _unitOfWork.CityRepo.IsEntityExist(city => city.Id == dto.CityId && city.Status == CityStatus.ACTIVE))
            throw new EntityNullException(ExceptionMessage.CITY_NOT_ACTIVE);

        volunteer = _mapper.Map(dto, volunteer);

        List<int> skillForAdd = dto.VolunteerSkills.Except(volunteer.Skills.Select(x => x.SkillId)).ToList();

        foreach (var skill in volunteer.Skills)
        {
            skill.Status = dto.VolunteerSkills.Any(id => id == skill.SkillId) ? SkillStatus.ACTIVE : SkillStatus.DELETED;
        }

        //add skills
        volunteer.Skills.AddRange(
            skillForAdd.Select(
                skillId => new VolunteerSkills
                {
                    SkillId = skillId,
                    Status = SkillStatus.ACTIVE
                }).ToList());

        volunteer.ModifiedBy = dto.UserId;

        _unitOfWork.VolunteerRepo.Update(volunteer);
        await _unitOfWork.SaveAsync();
    }


    public async Task UpdateAvatarAsync(IFormFile avatar, long id)
    {
        if (!avatar.ContentType.Contains("image")) throw new BadImageFormatException(ExceptionMessage.AVATAR_NOT_VALID);

        Volunteer? volunteer = await _unitOfWork.VolunteerRepo.GetAsync(x => x.User.Id == id, new Expression<Func<Volunteer, object>>[] { x => x.User }) ?? throw new EntityNullException(ExceptionMessage.VOLUNTEER_NOT_FOUND);

        new FileHepler(_env).DeleteFileFromDestination(volunteer.User.Avatar);

        KeyValuePair<string, string> fileData = await new FileHepler(_env).UploadFileToDestination(avatar, SystemConstant.DIR_AVATAR, id);

        volunteer.User.Avatar = fileData.Key;

        _unitOfWork.VolunteerRepo.Update(volunteer);
        await _unitOfWork.SaveAsync();
    }


    private async Task<Volunteer> GetVolunteer(long id)
    {
        Volunteer? volunteer = await _unitOfWork.VolunteerRepo.GetAsync(x => x.User.Id == id, new Expression<Func<Volunteer, object>>[] { x => x.User }, new string[] { "Skills.Skill", "City.Country" }) ?? throw new EntityNullException(ExceptionMessage.VOLUNTEER_NOT_FOUND);

        return volunteer;
    }

    #endregion
}
