using AutoMapper;
using Common.Constants;
using Common.CustomExceptions;
using Common.Enums;
using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Entities.DTOs;
using Microsoft.Extensions.Hosting;
using Services.Interfaces;
using System.Linq;
using System.Linq.Expressions;

namespace Services.Implementations
{
    public class VolunteerMissionService : BaseService<Mission>, IVolunteerMissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHostEnvironment _env;
        private readonly IMissionRatingService _missionRatingService;
        private readonly IFavouriteMissionService _favouriteMissionService;
        private readonly IVolunteerService _volunteerService;
        private readonly IMissionService _missionService;

        public VolunteerMissionService(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment env, IMissionRatingService missionRatingService, IFavouriteMissionService favouriteMissionService, IVolunteerService volunteerService, IMissionService missionService) : base(unitOfWork.MissionRepo, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
            _missionRatingService = missionRatingService;
            _favouriteMissionService = favouriteMissionService;
            _volunteerService = volunteerService;
            _missionService = missionService;
        }

        public async Task<PageListResponseDTO<VolunteerMissionDTO>> GetAllAsync(VolunteerMissionListRequestDTO missionListRequest, long sessionUserId)
        {
            PageListRequestEntity<Mission> pageListRequestEntity = _mapper.Map<PageListRequestEntity<Mission>>(missionListRequest);

            Volunteer volunteer = await _volunteerService.GetVolunteerByUserId(sessionUserId);
            if (volunteer == null || volunteer.Id == 0)
                throw new EntityNullException(ExceptionMessage.VOLUNTEER_NOT_FOUND);

            long volunteerId = volunteer.Id;

            ApplyFilters(missionListRequest, pageListRequestEntity, volunteerId);

            PageListResponseDTO<Mission> pageListResponse = await _unitOfWork.VolunteerMissionRepo.GetAllAsync(pageListRequestEntity);

            List<int> listSkillIds = pageListResponse.Records.SelectMany(x => x.Skills.Select(x => x.SkillId).Distinct()).ToList();

            List<Skill> skillNames = pageListResponse.Records.SelectMany(x => x.Skills.Select(x => x.Skill)).Distinct().ToList();

            List<VolunteerMissionDTO> missionInfoDTOs = _mapper.Map<List<VolunteerMissionDTO>>(pageListResponse.Records);

            missionInfoDTOs.ForEach(dto =>
            {
                dto.SkillList = skillNames.Where(x => dto.SkillList.Any(y => y.Value == x.Id)).Select(x => new DropdownDTO()
                {
                    Value = x.Id,
                    Text = x.Name
                }).ToList();
                dto.HasApplied = pageListResponse.Records.Any(x => x.MissionApplications.Any(x => x.MissionId == dto.Id && x.VolunteerId == volunteerId));
            });

            return new PageListResponseDTO<VolunteerMissionDTO>(pageListResponse.PageIndex, pageListResponse.PageSize, pageListResponse.TotalRecords, missionInfoDTOs);
        }

        private static void ApplyFilters(VolunteerMissionListRequestDTO missionListRequest, PageListRequestEntity<Mission> pageListRequestEntity, long volunteerId)
        {
            pageListRequestEntity.IncludeExpressions = new Expression<Func<Mission, object>>[]
                        {
                            x => x.MissionTheme,
                            x => x.City,
                            x => x.Skills.Where(x=>x.Status != SkillStatus.DELETED),
                            x => x.MissionRating,
                            x => x.FavouriteMissions.Where(x => x.VolunteerId== volunteerId && x.IsFavourite),
                            x => x.MissionApplications.Where(x => x.Status == MissionApplicationStatus.APPROVED)
                        };
            pageListRequestEntity.ThenIncludeExpressions = new string[] { "Skills.Skill" };

            string searchQuery = string.IsNullOrEmpty(missionListRequest.SearchQuery) ? "" : missionListRequest.SearchQuery.Trim().ToLower();

            pageListRequestEntity.Predicate = mission =>
            (mission.Title.Trim().ToLower().Contains(searchQuery)
            || mission.MissionTheme.Title.Trim().ToLower().Contains(searchQuery))
            && (missionListRequest.CountryId != 0 ? mission.City.CountryId == missionListRequest.CountryId : true)
            && (missionListRequest.CityIds.Any() ? missionListRequest.CityIds.Contains(mission.CityId) : true)
            && (missionListRequest.MissionThemeIds.Any() ? missionListRequest.MissionThemeIds.Contains(mission.MissionThemeId) : true)
            && (missionListRequest.SkillIds.Any() ? mission.Skills.Any(s => s.Status == SkillStatus.ACTIVE && missionListRequest.SkillIds.Contains(s.SkillId)) : true);
        }

        public async Task UpsertRatingsAsync(MissionRatingDTO dto, long sessionUserId)
        {
            bool missionFound = await _missionService.CheckForMission(dto.MissionId);
            if (!missionFound)
                throw new EntityNullException(ExceptionMessage.MISSION_NOT_FOUND);

            if (dto.Rating <= SystemConstant.MIN_RATING || dto.Rating > SystemConstant.MAX_RATING)
                throw new EntityNullException(ExceptionMessage.INVALID_RATING);

            Volunteer volunteer = await _volunteerService.GetVolunteerByUserId(sessionUserId);
            if (volunteer == null || volunteer.Id == 0)
                throw new EntityNullException(ExceptionMessage.VOLUNTEER_NOT_FOUND);

            long volunteerId = volunteer.Id;
            bool isUserRatingExists = await HasUserRating(dto.MissionId, volunteerId);

            if (isUserRatingExists)
            {
                // Update the rating
                await UpdateRatingAsync(dto.MissionId, dto.Rating, volunteerId);
            }
            else
            {
                // Add a new rating
                await AddRatingAsync(dto.MissionId, volunteerId, dto.Rating);
            }
        }

        public async Task<bool> SetFavoriteStatusAsync(long missionId, long sessionUserId)
        {
            Volunteer volunteer = await _volunteerService.GetVolunteerByUserId(sessionUserId);
            if (volunteer == null || volunteer.Id == 0)
                throw new EntityNullException(ExceptionMessage.VOLUNTEER_NOT_FOUND);

            long volunteerId = volunteer.Id;

            bool isFavoriteExists = await IsFavouriteExists(missionId, volunteerId);

            bool isFavorite = await IsFavourites(missionId, volunteerId);

            bool missionFound = await _missionService.CheckForMission(missionId);
            if (!missionFound)
                throw new EntityNullException(ExceptionMessage.MISSION_NOT_FOUND);

            if (!isFavoriteExists)
            {
                // Add a new favorite record
                await AddFavoriteAsync(missionId, volunteerId, isFavorite);
                return true;
            }
            else
            {
                // Update the favorite status
                bool isLiked = await UpdateFavoriteAsync(missionId, volunteerId, isFavorite);
                return !isLiked;
            }
        }


        private async Task<bool> HasUserRating(long missionId, long volunteerId) => await _unitOfWork.MissionRatingRepo.IsEntityExist(rating => rating.MissionId == missionId && rating.VolunteerId == volunteerId);

        private async Task AddRatingAsync(long missionId, long volunteerId, int rating)
        {
            MissionRating newRating = new MissionRating
            {
                MissionId = missionId,
                VolunteerId = volunteerId,
                Rating = rating
            };

            await _missionRatingService.AddAsync(newRating);
        }

        private async Task UpdateRatingAsync(long missionId, int rating, long volunteerId)
        {

            MissionRating? existingRating = await _unitOfWork.MissionRatingRepo.GetAsync(r => r.MissionId == missionId && r.VolunteerId == volunteerId);

            if (existingRating == null)
                throw new EntityNullException(ExceptionMessage.RATING_NOT_FOUND);

            existingRating.Rating = rating;

            await _missionRatingService.UpdateAsync(existingRating);
        }

        private async Task<bool> IsFavouriteExists(long missionId, long volunteerId) => await _unitOfWork.FavouriteMissionRepo.IsEntityExist(fav => fav.MissionId == missionId && fav.VolunteerId == volunteerId);

        private async Task<bool> IsFavourites(long missionId, long volunteerId) => await _unitOfWork.FavouriteMissionRepo.IsEntityExist(fav => fav.MissionId == missionId && fav.VolunteerId == volunteerId && fav.IsFavourite == true);

        private async Task AddFavoriteAsync(long missionId, long volunteerId, bool isFavorite)
        {

            FavouriteMission newFavorite = new FavouriteMission
            {
                MissionId = missionId,
                VolunteerId = volunteerId,
                IsFavourite = true
            };

            await _favouriteMissionService.AddAsync(newFavorite);
        }

        private async Task<bool> UpdateFavoriteAsync(long missionId, long volunteerId, bool isFavorite)
        {
            FavouriteMission? existingFavorite = await _unitOfWork.FavouriteMissionRepo.GetAsync(fav =>
                fav.MissionId == missionId && fav.VolunteerId == volunteerId);

            if (existingFavorite == null)
                throw new EntityNullException(ExceptionMessage.FAVOURITES_NOT_FOUND);

            existingFavorite.IsFavourite = !existingFavorite.IsFavourite;

            await _favouriteMissionService.UpdateAsync(existingFavorite);

            return !existingFavorite.IsFavourite;
        }

        #region Get_Mission_By_Mission_ID

        public async Task<VolunteerMissionInfoDTO> GetById(long missionId, long sessionUserId)
        {
            Volunteer volunteer = await _volunteerService.GetVolunteerByUserId(sessionUserId);
            if (volunteer == null || volunteer.Id == 0)
                throw new EntityNullException(ExceptionMessage.VOLUNTEER_NOT_FOUND);

            long volunteerId = volunteer.Id;

            Mission? mission = await _unitOfWork.MissionRepo.GetAsync(mission => mission.Id == missionId, new Expression<Func<Mission, object>>[] { x => x.City, x => x.MissionTheme, x => x.MissionRating, x => x.Skills.Where(x => x.Status == SkillStatus.ACTIVE), x => x.Medias }, new string[] { "City.Country", "Skills.Skill" }) ?? throw new EntityNullException(ExceptionMessage.MISSION_NOT_FOUND);

            VolunteerMissionInfoDTO? missionDTO = _mapper.Map<VolunteerMissionInfoDTO>(mission);

            missionDTO.IsFavourite = await checkFavouriteMission(missionId, volunteerId);
            missionDTO.HasApplied = await checkVolunteerApplication(missionId, volunteerId);

            return missionDTO;
        }

        #endregion

        #region Related_Missions

        public async Task<List<RelatedMissionDTO>> GetRelatedMissionsAsync(long missionId, long sessionUserId)
        {
            Volunteer volunteer = await _volunteerService.GetVolunteerByUserId(sessionUserId);
            if (volunteer == null || volunteer.Id == 0)
                throw new EntityNullException(ExceptionMessage.VOLUNTEER_NOT_FOUND);

            long volunteerId = volunteer.Id;

            bool missionFound = await _missionService.CheckForMission(missionId);
            if (!missionFound)
                throw new EntityNullException(ExceptionMessage.MISSION_NOT_FOUND);

            Expression<Func<Mission, object>>[] includes = { m => m.MissionRating };

            Mission? mission = await _unitOfWork.VolunteerMissionRepo.GetAsync(m => m.Id == missionId, includes: includes);

            List<RelatedMissionDTO> relatedMissions = new List<RelatedMissionDTO>();

            long themeId = mission.MissionThemeId;
            MissionTheme theme = await _unitOfWork.MissionThemeRepo.GetByIdAsync(themeId);
            string themeTitle = theme.Title;

            long cityId = mission.CityId;
            City city = await _unitOfWork.CityRepo.GetByIdAsync(cityId);
            string cityName = city.Name;

            long countryId = mission.City.CountryId;
            Country country = await _unitOfWork.CountryRepo.GetByIdAsync(countryId);
            string countryName = country.Name;

            // Fetch missions based on theme
            List<Mission>? missionsByTheme = await _unitOfWork.VolunteerMissionRepo.GetAllAsync(m => m.MissionTheme.Title == themeTitle);
            relatedMissions.AddRange(await MapToRelatedMissionDTOs(missionsByTheme.Take(3), volunteerId));

            // Fetch missions based on city name if not enough missions found by theme
            if (relatedMissions.Count < 3)
            {
                int themeCount = relatedMissions.Count;
                List<Mission>? missionsByCity = await _unitOfWork.VolunteerMissionRepo.GetAllAsync(m => m.MissionTheme.Title == themeTitle && m.City.Name == cityName);
                IEnumerable<Mission> additionalMissions = missionsByCity.Where(m => !relatedMissions.Any(rm => rm.Id == m.Id)).Take(3 - themeCount);
                relatedMissions.AddRange(await MapToRelatedMissionDTOs(additionalMissions, volunteerId));
            }

            // Fetch missions based on country name if not enough missions found by theme and city name
            if (relatedMissions.Count < 3)
            {
                List<Mission> missionsByCountry = await _unitOfWork.VolunteerMissionRepo.GetAllAsync(m => m.MissionTheme.Title == themeTitle && m.City.Name == cityName && m.City.Country.Name == countryName);
                IEnumerable<Mission> additionalMissions = missionsByCountry.Where(m => !relatedMissions.Any(rm => rm.Id == m.Id)).Take(3 - relatedMissions.Count);
                relatedMissions.AddRange(await MapToRelatedMissionDTOs(additionalMissions, volunteerId));
            }

            return relatedMissions;
        }

        private async Task<List<RelatedMissionDTO>> MapToRelatedMissionDTOs(IEnumerable<Mission> missions, long volunteerId)
        {
            List<RelatedMissionDTO> relatedMissionDTOs = new List<RelatedMissionDTO>();
            List<long> missionIds = missions.Select(m => m.Id).ToList();

            Dictionary<long, bool> favouriteMissions = await GetFavouriteMissions(missionIds, volunteerId);
            Dictionary<long, bool> appliedMissions = await GetAppliedMissions(missionIds, volunteerId);

            foreach (Mission mission in missions)
            {
                bool isFavourite = favouriteMissions.TryGetValue(mission.Id, out bool favourite);
                bool hasApplied = appliedMissions.TryGetValue(mission.Id, out bool applied);

                RelatedMissionDTO relatedMissionDTO = _mapper.Map<Mission, RelatedMissionDTO>(mission);
                relatedMissionDTO.IsFavourite = isFavourite ? true : false;
                relatedMissionDTO.HasApplied = hasApplied ? true : false;
                relatedMissionDTOs.Add(relatedMissionDTO);
            }

            return relatedMissionDTOs;
        }

        #endregion

        private async Task<bool> checkFavouriteMission(long missionId, long volunteerId)
        {
            FavouriteMission? favouriteMission = await _unitOfWork.FavouriteMissionRepo.GetAsync(m => m.MissionId == missionId && m.VolunteerId == volunteerId);

            return favouriteMission != null && favouriteMission.IsFavourite ? true : false;
        }
        
        private async Task<bool>  checkVolunteerApplication(long missionId, long volunteerId)
        {
            MissionApplication? missionApplication = await _unitOfWork.MissionApplicationRepo.GetAsync(m => m.MissionId == missionId && m.VolunteerId == volunteerId);

            return missionApplication != null && missionApplication.Status == MissionApplicationStatus.APPROVED ? true : false;
        }

        private async Task<Dictionary<long, bool>> GetFavouriteMissions(List<long> missionIds, long volunteerId)
        {
            List<FavouriteMission> favouriteMissions = await _unitOfWork.FavouriteMissionRepo.GetAllAsync(fm => missionIds.Contains(fm.MissionId) && fm.VolunteerId == volunteerId);
            Dictionary<long, bool> dictionary = favouriteMissions.ToDictionary(fm => fm.MissionId, fm => fm.IsFavourite);
            return dictionary;
        }

        private async Task<Dictionary<long, bool>> GetAppliedMissions(List<long> missionIds, long volunteerId)
        {
            List<MissionApplication> missionApplications = await _unitOfWork.MissionApplicationRepo.GetAllAsync(ma => missionIds.Contains(ma.MissionId) && ma.VolunteerId == volunteerId);
            Dictionary<long, bool> dictionary = missionApplications.ToDictionary(ma => ma.MissionId, ma => ma.Status == MissionApplicationStatus.APPROVED);
            return dictionary;
        }

    }
}