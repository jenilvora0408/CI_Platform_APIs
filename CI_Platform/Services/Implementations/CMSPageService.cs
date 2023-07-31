using AutoMapper;
using Common.Constants;
using Common.CustomExceptions;
using Common.Enums;
using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Entities.DTOs;
using Services.Interfaces;

namespace Services.Implementations
{
    public class CMSPageService : BaseService<CMSPage>, ICMSPageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CMSPageService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.CMSPageRepo, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PageListResponseDTO<CMSPageDTO>> GetAllCMSPagesAsync(CMSPageListRequestDTO cmsPageListRequest)
        {

            PageListRequestEntity<CMSPage> pageListRequestEntity = _mapper.Map<CMSPageListRequestDTO, PageListRequestEntity<CMSPage>>(cmsPageListRequest);

            if (!string.IsNullOrEmpty(cmsPageListRequest.SearchQuery))
            {
                pageListRequestEntity.Predicate = cmsPage => cmsPage.Title.Trim().ToLower().Contains(cmsPageListRequest.SearchQuery.ToLower().Trim());
            }

            PageListResponseDTO<CMSPage> pageListResponse = await _unitOfWork.CMSPageRepo.GetAllAsync(pageListRequestEntity);

            List<CMSPageDTO> cmsPageDTOs = _mapper.Map<List<CMSPage>, List<CMSPageDTO>>(pageListResponse.Records);

            return new PageListResponseDTO<CMSPageDTO>(pageListResponse.PageIndex, pageListResponse.PageSize, pageListResponse.TotalRecords, cmsPageDTOs);
        }

        public async Task AddAsync(CMSPageDTO cmsPageDTO, long sessionUserId)
        {
            bool isExist = await _unitOfWork.CMSPageRepo.IsEntityExist(cmsPage => cmsPage.Slug == cmsPageDTO.Slug && cmsPage.Id != cmsPageDTO.Id);

            if (isExist) throw new DataAlreadyExistsException(ExceptionMessage.CMS_PAGE_ALREADY_EXIST);

            CMSPage cmsPage = _mapper.Map<CMSPage>(cmsPageDTO);
            cmsPage.Status = CMSPageStatus.ACTIVE;
            await AddAsync(cmsPage);
        }

        public async Task UpdateAsync(CMSPageDTO cmsPageDTO, long sessionUserId)
        {
            CMSPage cmsPage = await _unitOfWork.CMSPageRepo.GetByIdAsync(cmsPageDTO.Id);
            if (cmsPage == null)
                throw new EntityNullException(ExceptionMessage.CMS_PAGE_NOT_FOUND);

            bool isExist = await _unitOfWork.CMSPageRepo.IsEntityExist(cmsPage => cmsPage.Slug == cmsPageDTO.Slug && cmsPage.Id != cmsPageDTO.Id);

            if (isExist) throw new DataAlreadyExistsException(ExceptionMessage.CMS_PAGE_ALREADY_EXIST);

            cmsPage.Title = cmsPageDTO.Title;
            cmsPage.Description = cmsPageDTO.Description;
            cmsPage.Slug = cmsPageDTO.Slug;

            if (cmsPageDTO.Status != null)
            {
                cmsPage.Status = (CMSPageStatus)cmsPageDTO.Status;
            }

            cmsPage.ModifiedBy = sessionUserId;

            await UpdateAsync(cmsPage);
        }

        public async Task RemoveAsync(long id, long sessionUserId)
        {
            CMSPage cmsPage = await _unitOfWork.CMSPageRepo.GetByIdAsync(id);
            if (cmsPage == null)
                throw new EntityNullException(ExceptionMessage.CMS_PAGE_NOT_FOUND);

            cmsPage.Status = CMSPageStatus.DELETED;
            cmsPage.ModifiedBy = sessionUserId;

            await UpdateAsync(cmsPage);
        }

        public async Task<CMSPageDTO> GetByIdAsync(long id)
        {
            CMSPage cmsPage = await _unitOfWork.CMSPageRepo.GetByIdAsync(id);

            if (cmsPage == null)
                throw new EntityNullException(ExceptionMessage.CMS_PAGE_NOT_FOUND);

            if (cmsPage == null)
            {
                return null;
            }

            CMSPageDTO cmsPageDTO = _mapper.Map<CMSPageDTO>(cmsPage);
            return cmsPageDTO;
        }
    }
}
