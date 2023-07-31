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
    public class MissionThemeService : BaseService<MissionTheme>, IMissionThemeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MissionThemeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.MissionThemeRepo, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PageListResponseDTO<MissionThemeDTO>> GetAllThemesAsync(MissionThemeListRequestDTO missionThemeListRequest)
        {

            PageListRequestEntity<MissionTheme> pageListRequestEntity = _mapper.Map<MissionThemeListRequestDTO, PageListRequestEntity<MissionTheme>>(missionThemeListRequest);

            if (!string.IsNullOrEmpty(missionThemeListRequest.SearchQuery))
            {
                pageListRequestEntity.Predicate = missionTheme => missionTheme.Title.Trim().ToLower().Contains(missionThemeListRequest.SearchQuery.ToLower().Trim());
            }

            PageListResponseDTO<MissionTheme> pageListResponse = await _unitOfWork.MissionThemeRepo.GetAllAsync(pageListRequestEntity);

            List<MissionThemeDTO> missionThemeDTOs = _mapper.Map<List<MissionTheme>, List<MissionThemeDTO>>(pageListResponse.Records);

            return new PageListResponseDTO<MissionThemeDTO>(pageListResponse.PageIndex, pageListResponse.PageSize, pageListResponse.TotalRecords, missionThemeDTOs);
        }

        public async Task AddAsync(MissionThemeDTO missionThemeDTO, long sessionUserId)
        {
            MissionTheme missionTheme = await _unitOfWork.MissionThemeRepo.GetByIdAsync(missionThemeDTO.Id);

            bool isExist = await _unitOfWork.MissionThemeRepo.IsEntityExist(missionTheme => missionTheme.Title == missionThemeDTO.Title && missionTheme.Id != missionThemeDTO.Id);

            if (isExist) throw new DataAlreadyExistsException(ExceptionMessage.MISSION_THEME_ALREADY_EXIST);

            missionTheme = _mapper.Map<MissionTheme>(missionThemeDTO);

            missionTheme.Status = MissionThemeStatus.ACTIVE;
            missionTheme.CreatedBy = sessionUserId;
            missionTheme.ModifiedBy = sessionUserId;

            await AddAsync(missionTheme);
        }

        public async Task UpdateAsync(MissionThemeDTO missionThemeDTO, long sessionUserId)
        {
            MissionTheme missionTheme = await _unitOfWork.MissionThemeRepo.GetByIdAsync(missionThemeDTO.Id);
            if (missionTheme == null)
                throw new EntityNullException(ExceptionMessage.MISSION_THEME_NOT_FOUND);

            bool isExist = await _unitOfWork.MissionThemeRepo.IsEntityExist(missionTheme => missionTheme.Title == missionThemeDTO.Title && missionTheme.Id != missionThemeDTO.Id);

            if (isExist) throw new DataAlreadyExistsException(ExceptionMessage.MISSION_THEME_ALREADY_EXIST);

            if(missionThemeDTO.Status != MissionThemeStatus.ACTIVE)
            {
                bool hasActiveMissions = await HasActiveMissions(missionTheme);
                if (hasActiveMissions)
                    throw new ForbiddenException(ExceptionMessage.MISSION_THEME_IN_USE_FOR_MISSION);
            }

            missionTheme.Title = missionThemeDTO.Title;

            if (missionThemeDTO.Status != null)
            {
                missionTheme.Status = (MissionThemeStatus)missionThemeDTO.Status;
            }

            missionTheme.ModifiedBy = sessionUserId;

            await UpdateAsync(missionTheme);
        }

        public async Task RemoveAsync(long id, long sessionUserId)
        {
            MissionTheme missionTheme = await _unitOfWork.MissionThemeRepo.GetByIdAsync(id);
            if (missionTheme == null)
                throw new EntityNullException(ExceptionMessage.MISSION_THEME_NOT_FOUND);

            bool hasActiveMissions = await HasActiveMissions(missionTheme);
            if (hasActiveMissions)
                throw new ForbiddenException(ExceptionMessage.MISSION_THEME_IN_USE_FOR_MISSION);

            missionTheme.Status = MissionThemeStatus.DELETED;
            missionTheme.ModifiedBy = sessionUserId;

            await UpdateAsync(missionTheme);
        }

        public async Task<MissionThemeDTO> GetByIdAsync(long id)
        {
            MissionTheme missionTheme = await _unitOfWork.MissionThemeRepo.GetByIdAsync(id);

            if (missionTheme == null)
                throw new EntityNullException(ExceptionMessage.MISSION_THEME_NOT_FOUND);

            if (missionTheme == null)
            {
                return null;
            }

            MissionThemeDTO missionThemeDTO = _mapper.Map<MissionThemeDTO>(missionTheme);
            return missionThemeDTO;
        }

        public async Task<IEnumerable<DropdownDTO>> GetThemes()
        {
            List<MissionTheme> themes = await _unitOfWork.MissionThemeRepo.GetAllAsync(missionTheme => missionTheme.Status == MissionThemeStatus.ACTIVE);
            if (themes.Count == 0)
            {
                return Enumerable.Empty<DropdownDTO>();
            }

            List<DropdownDTO> themeInfoDTOs = _mapper.Map<List<DropdownDTO>>(themes);
            return themeInfoDTOs.AsEnumerable();
        }

        private async Task<bool> HasActiveMissions(MissionTheme missionTheme)
        {
            bool isMissionExist = await _unitOfWork.MissionRepo.IsEntityExist(m => m.MissionThemeId == missionTheme.Id && m.Status == MissionStatus.ACTIVE);
            return isMissionExist;
        }
    }
}
