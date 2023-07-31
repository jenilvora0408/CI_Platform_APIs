using AutoMapper;
using Common.Constants;
using Common.CustomExceptions;
using Common.Enums;
using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Entities.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Services.Interfaces;
using System.Linq.Expressions;

namespace Services.Implementations;

public class MissionApplicationService : BaseService<MissionApplication>, IMissionApplicationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMissionService _missionService;

    public MissionApplicationService(IUnitOfWork unitOfWork, IMapper mapper, IMissionService missionService) : base(unitOfWork.MissionApplicationRepo, unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _missionService = missionService;
    }
    #region Volunteer side

    public async Task CreateAsync(VolunteerMissionApplicationDTO dto, long sessionUserId)
    {
        long volunteerId = await GetVolunteerId(sessionUserId);
        await CheckExistancy(dto.MissionId, volunteerId);

        MissionApplication entity = _mapper.Map<MissionApplication>(dto);
        entity.VolunteerId = volunteerId;
        await AddAsync(entity);
    }

    public async Task<IEnumerable<DropdownDTO>> GetAppliedMissionsListAsync(long sessionUserId)
    {
        PageListRequestEntity<MissionApplication> pageListRequest = new()
        {
            Predicate = ma => ma.CreatedBy == sessionUserId && ma.Status == MissionApplicationStatus.APPROVED,
            IncludeExpressions = new Expression<Func<MissionApplication, object>>[] { x => x.Mission },
            Selects = ma => new MissionApplication()
            {
                Mission = new Mission()
                {
                    Id = ma.Mission.Id,
                    Title = ma.Mission.Title
                }
            }
        };
        PageListResponseDTO<MissionApplication> list = await _unitOfWork.MissionApplicationRepo.GetAllAsync(pageListRequest);
        if (!list.Records.Any())
            return Enumerable.Empty<DropdownDTO>();

        List<DropdownDTO> dropdownList = _mapper.Map<List<DropdownDTO>>(list.Records);
        return dropdownList;
    }

    #endregion

    #region Admin side

    public async Task<PageListResponseDTO<MissionApplicationInfoDTO>> GetAllAsync(MissionApplicationListRequestDTO requestDTO)
    {
        PageListRequestEntity<MissionApplication> pageListRequest = _mapper.Map<PageListRequestEntity<MissionApplication>>(requestDTO);
        pageListRequest.IncludeExpressions = new Expression<Func<MissionApplication, object>>[] { ma => ma.Mission, ma => ma.Volunteer };

        pageListRequest.Selects = missionApplication => new MissionApplication()
        {
            Id = missionApplication.Id,
            AppliedOn = missionApplication.AppliedOn,
            Status = missionApplication.Status,
            Mission = new Mission()
            {
                Title = missionApplication.Mission.Title
            },
            Volunteer = new Volunteer()
            {
                User = new User()
                {
                    FirstName = missionApplication.Volunteer.User.FirstName,
                    LastName = missionApplication.Volunteer.User.LastName
                }
            }
        };

        if (!string.IsNullOrEmpty(requestDTO.SearchQuery))
        {
            string searchQuery = requestDTO.SearchQuery.Trim().ToLower();
            pageListRequest.Predicate = ma => ma.Mission.Title.Trim().ToLower().Contains(searchQuery)
                || (ma.Volunteer.User.FirstName + " " + ma.Volunteer.User.LastName).Trim().ToLower().Contains(searchQuery)
                || (ma.Volunteer.User.LastName + " " + ma.Volunteer.User.FirstName).Trim().ToLower().Contains(searchQuery);
        }

        PageListResponseDTO<MissionApplication> pageListResponse = await _unitOfWork.MissionApplicationRepo.GetAllAsync(pageListRequest);
        List<MissionApplicationInfoDTO> responseList = _mapper.Map<List<MissionApplicationInfoDTO>>(pageListResponse.Records);
        return new PageListResponseDTO<MissionApplicationInfoDTO>(pageListResponse.PageIndex, pageListResponse.PageSize, pageListResponse.TotalRecords, responseList);
    }

    public async Task<string> UpdateStatusAsync(long id, JsonPatchDocument<MissionApplicationPatchDTO> patchDocument, long sessionUserId)
    {
        MissionApplication? missionApplication = await _unitOfWork.MissionApplicationRepo.GetByIdAsync(id);

        if (missionApplication == null)
        {
            throw new EntityNullException(ExceptionMessage.MISSION_APPLICATION_NOT_FOUND);
        }

        await CheckAppliable(missionApplication.MissionId);

        MissionApplicationPatchDTO patchDTO = new MissionApplicationPatchDTO()
        {
            Status = missionApplication.Status,
        };

        patchDocument.ApplyTo(patchDTO);

        _mapper.Map(patchDTO, missionApplication);

        missionApplication.ModifiedBy = sessionUserId;

        await UpdateAsync(missionApplication);

        return $"Application {missionApplication.Status}!";
    }

    #endregion

    #region Helper Methods

    private async Task CheckExistancy(long missionId, long volunteerId)
    {
        bool isAlreadyApplied = await _unitOfWork.MissionApplicationRepo.IsEntityExist(ma => ma.MissionId == missionId && ma.VolunteerId == volunteerId);
        if (isAlreadyApplied)
        {
            throw new DataAlreadyExistsException(ExceptionMessage.ALREADY_APPLIED_FOR_MISSION);
        }

        await CheckAppliable(missionId);
    }

    private async Task CheckAppliable(long missionId)
    {
        bool isExist = await _unitOfWork.MissionRepo.IsEntityExist(m => m.Id == missionId && m.Status == MissionStatus.ACTIVE);
        if (!isExist)
        {
            throw new EntityNullException(ExceptionMessage.MISSION_NOT_FOUND);
        }

        Mission? missionInfo = await _unitOfWork.MissionRepo
                    .GetAsync(m => m.Id == missionId,
                    selects: mission => new Mission
                    {
                        MissionType = mission.MissionType,
                        StartDate = mission.StartDate,
                        RegistrationDeadline = mission.RegistrationDeadline,
                        TotalSeat = mission.TotalSeat,
                        Availability = mission.Availability,
                        GoalObjective = mission.GoalObjective,
                        GoalObjectiveAchieved = mission.GoalObjectiveAchieved,
                    }) ?? throw new EntityNullException(ExceptionMessage.MISSION_NOT_FOUND);

        switch (missionInfo.MissionType)
        {
            case Common.Enums.MissionType.Goal:
                if (missionInfo.GoalObjectiveAchieved >= missionInfo.GoalObjective)
                {
                    throw new InvalidOperationException(ExceptionMessage.ALREADY_GOAL_VALUE_ACHIEVED);
                }
                break;

            case MissionType.Time:

                if (missionInfo.StartDate >= DateTimeOffset.UtcNow)
                    throw new InvalidOperationException(ExceptionMessage.EARLY_APPLY_MISSION_ERROR);

                if (missionInfo.RegistrationDeadline.HasValue && missionInfo.RegistrationDeadline <= DateTimeOffset.Now)
                    throw new InvalidOperationException(ExceptionMessage.LATE_APPLY_MISSION_ERROR);

                if (missionInfo.EndDate.HasValue && missionInfo.EndDate <= DateTimeOffset.Now)
                    throw new InvalidOperationException(ExceptionMessage.LATE_APPLY_MISSION_ERROR);

                int numberOfFullSeat = await _unitOfWork.MissionApplicationRepo.GetAppliedVolunteerCountForMission(ma => ma.MissionId == missionId && ma.Status == MissionApplicationStatus.APPROVED);

                if (missionInfo?.TotalSeat <= numberOfFullSeat)
                    throw new InvalidOperationException(ExceptionMessage.SEAT_FULL_MISSION_ERROR);

                break;
            default:
                break;
        }
    }

    private async Task<long> GetVolunteerId(long userId)
    {
        var vol = await _unitOfWork.VolunteerRepo.GetAsync(v => v.UserId == userId,
            selects: volunteer => new Volunteer
            {
                Id = volunteer.Id,
            });
        return vol!.Id;
    }

    public async Task<bool> CheckApprovalStatus(long missionId, long volunteerId)
    {
        MissionApplication entity = await GetAsync(ma => ma.MissionId == missionId && ma.VolunteerId == volunteerId) ?? throw new EntityNullException(ExceptionMessage.MISSION_APPLICATION_NOT_FOUND);
        return entity.Status == MissionApplicationStatus.APPROVED;
    }

    #endregion

    #region Recent_Volunteer

    public async Task<PageListResponseDTO<RecentVolunteerDTO>> GetApprovedMissionApplicationsByMissionId(long missionId, int pageIndex)
    {
        bool missionFound = await _missionService.CheckForMission(missionId);
        if (!missionFound)
            throw new EntityNullException(ExceptionMessage.MISSION_NOT_FOUND);

        if (pageIndex < 1)
            pageIndex = 1;

        Expression<Func<MissionApplication, bool>> expression = a =>
            a.MissionId == missionId && a.Status == MissionApplicationStatus.APPROVED;

        PageListRequestEntity<MissionApplication> pageListRequest = new PageListRequestEntity<MissionApplication>
        {
            PageIndex = pageIndex,
            PageSize = SystemConstant.RECENT_VOLUNTEER_PAGE_SIZE,
            Predicate = expression,
            SortColumn = SystemConstant.RECENT_VOLUNTEER_SORT_COLUMN,
            SortOrder = SystemConstant.DESCENDING,
            IncludeExpressions = new Expression<Func<MissionApplication, object>>[]
            {
                    a => a.Volunteer,
                    a => a.Volunteer.User
            }
        };

        PageListResponseDTO<MissionApplication> missionApplications = await _unitOfWork.MissionApplicationRepo.GetAllAsync(pageListRequest);

        return new PageListResponseDTO<RecentVolunteerDTO>(
            missionApplications.PageIndex,
            missionApplications.PageSize,
            missionApplications.TotalRecords,
            _mapper.Map<List<RecentVolunteerDTO>>(missionApplications.Records)
        );
    }

    #endregion
}