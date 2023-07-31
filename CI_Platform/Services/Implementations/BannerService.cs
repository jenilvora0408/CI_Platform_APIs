using AutoMapper;
using Common.Constants;
using Common.CustomExceptions;
using Common.Enums;
using Common.Utils;
using DataAccessLayer.Migrations;
using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Services.Interfaces;

namespace Services.Implementations;

public class BannerService : BaseService<Banner>, IBannerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHostEnvironment _env;

    public BannerService(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment env) : base(unitOfWork.BannerRepo, unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _env = env;
    }

    #region Admin side

    public async Task UpdateSortOrder(BannerSortOrderDTO dto, long sessionUserId)
    {

        if (dto.FromId == dto.ToId)
            throw new ValidationException(ExceptionMessage.SAME_BANNER_ORDER);


        Banner? fromEntity = await Get(dto.FromId);
        Banner? toEntity = await Get(dto.ToId);

        if (fromEntity == null || toEntity == null || fromEntity.Status != BannerStatus.ACTIVE || toEntity.Status != BannerStatus.ACTIVE)
            throw new Exception(ExceptionMessage.BANNER_NOT_FOUND);

        int minSortOrder = Math.Min(fromEntity.SortOrder, toEntity.SortOrder);
        int maxSortOrder = Math.Max(fromEntity.SortOrder, toEntity.SortOrder);

        List<Banner> entities = await _unitOfWork.BannerRepo.GetAllAsync(e => e.SortOrder < maxSortOrder && e.SortOrder > minSortOrder);

        if (fromEntity.SortOrder < toEntity.SortOrder)
        {
            entities.ForEach(e => e.SortOrder -= 1);
            fromEntity.SortOrder = maxSortOrder;
            toEntity.SortOrder = maxSortOrder - 1;
        }
        else if (fromEntity.SortOrder > toEntity.SortOrder)
        {
            entities.ForEach(e => e.SortOrder += 1);
            fromEntity.SortOrder = minSortOrder;
            toEntity.SortOrder = minSortOrder + 1;
        }
        entities.Add(fromEntity);
        entities.Add(toEntity);
        await UpdateRangeAsync(entities);
    }

    public async Task UpsertAsync(BannerDTO dto, long sessionUserId)
    {
        if (dto.Id == 0)
        {
            await Create(dto, sessionUserId);
            return;
        }
        await Update(dto, sessionUserId);
    }

    public async Task<PageListResponseDTO<BannerInfoDTO>> GetAllAsync(BannerListRequestDTO dto)
    {
        PageListRequestEntity<Banner> pageListRequestEntity = _mapper.Map<PageListRequestEntity<Banner>>(dto);

        if (!string.IsNullOrEmpty(dto.SearchQuery))
        {
            string searchQuery = dto.SearchQuery.Trim().ToLower();

            pageListRequestEntity.Predicate =
                banner => banner.Description.Trim().ToLower().Contains(searchQuery);
        }

        pageListRequestEntity.SortColumn = dto.SortColumn ?? SystemConstant.NAMEOF_SORTCOLUMN_SORTORDER;
        pageListRequestEntity.SortOrder = dto.SortOrder ?? SystemConstant.ASCENDING;

        PageListResponseDTO<Banner> pageListResponse = await _unitOfWork.BannerRepo.GetAllAsync(pageListRequestEntity);

        List<BannerInfoDTO> list = _mapper.Map<List<BannerInfoDTO>>(pageListResponse.Records);

        return new PageListResponseDTO<BannerInfoDTO>(pageListResponse.PageIndex, pageListResponse.PageSize, pageListResponse.TotalRecords, list);
    }

    public async Task<BannerInfoDTO> GetByIdAsync(int id)
    {
        Banner? banner = await Get(id);

        BannerInfoDTO infoDTO = _mapper.Map<BannerInfoDTO>(banner);

        return infoDTO;
    }

    public async Task RemoveAsync(int id, long sessionUserId)
    {
        Banner entity = await Get(id);
        if (entity.Status == BannerStatus.DELETED) throw new ValidationException($"Banner is already {BannerStatus.DELETED}");

        List<Banner> entities = await _unitOfWork.BannerRepo.GetAllAsync(ent => ent.Id >= id && ent.Status != BannerStatus.DELETED);

        Banner temp = entities.First();

        entities.Where(e => e.SortOrder > temp.SortOrder)
                .ToList()
                .ForEach(e => e.SortOrder--);

        entities.First().SortOrder = entities.Last().SortOrder + 1;
        entities.First().Status = BannerStatus.DELETED;

        await _unitOfWork.SaveAsync();

    }

    #endregion

    #region Volunteer side

    public async Task<IEnumerable<LoginBannerDTO>> GetAll()
    {
        PageListRequestEntity<Banner> pageListRequestEntity = new PageListRequestEntity<Banner>()
        {
            PageIndex = 1,
            PageSize = 5,
            SortColumn = "SortOrder",
            SortOrder = SystemConstant.ASCENDING,
            Predicate = x => x.Status == BannerStatus.ACTIVE,
            Selects = x => new Banner() { Image = x.Image, Description = x.Description }
        };

        var entity = await _unitOfWork.BannerRepo.GetAllAsync(pageListRequestEntity);
        return entity.Records.Select(banner => new LoginBannerDTO
        {
            Description = banner.Description,
            Url = banner.Image
        });
    }
    #endregion

    #region Helper methods

    private async Task<string> AddImage(IFormFile avatar, long id)
    {
        KeyValuePair<string, string> fileData = await new FileHepler(_env).UploadFileToDestination(avatar, SystemConstant.DIR_BANNER, id);
        return fileData.Key;
    }

    private async Task<string> UpdateImage(Banner banner, IFormFile avatar, long userId)
    {
        bool isAvatarDeleted = new FileHepler(_env).DeleteFileFromDestination(banner.Image);
        if (!isAvatarDeleted) throw new FileNullException("Image not updated");
        return await AddImage(avatar, userId);
    }

    private async Task<Banner> Create(BannerDTO dto, long sessionUserId)
    {
        List<Banner> deletedBanners = await _unitOfWork.BannerRepo.GetAllAsync(e => e.Status == BannerStatus.DELETED);
        deletedBanners.ForEach(db => db.SortOrder += 1);

        Banner banner = new();

        if (deletedBanners.Any())
        {
            await UpdateRangeAsync(deletedBanners);
            banner.SortOrder = deletedBanners.First().SortOrder;
        }
        else
        {
            banner.SortOrder = await _unitOfWork.BannerRepo.GetMaxSortOrderAsync() + 1;
        }
        banner.Description = dto.Description;
        banner.Status = BannerStatus.ACTIVE;
        banner.Image = await AddImage(dto.Image, sessionUserId);
        await AddAsync(banner);
        return banner;
    }

    private async Task<Banner> Update(BannerDTO dto, long sessionUserId)
    {
        Banner? banner = await _unitOfWork.BannerRepo.GetAsync(banner => banner.Id == dto.Id) ?? throw new EntityNullException(ExceptionMessage.BANNER_NOT_FOUND);

        banner.Description = dto.Description;
        banner.Status = dto.Status ?? banner.Status;

        if (dto.Image != null)
        {
            banner.Image = await UpdateImage(banner, dto.Image, sessionUserId);
        }
        await UpdateAsync(banner);
        return banner;
    }

    private async Task<Banner> Get(int id)
    {
        Banner? Banner = await _unitOfWork.BannerRepo.GetAsync(Banner => Banner.Id == id) ?? throw new EntityNullException(ExceptionMessage.BANNER_NOT_FOUND);
        return Banner;
    }

    #endregion

}