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

public class TimesheetService : BaseService<Timesheet>, ITimesheetService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    IVolunteerService _volunteerService;
    public TimesheetService(IUnitOfWork unitOfWork, IMapper mapper, IVolunteerService volunteerService) : base(unitOfWork.TimesheetRepo, unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _volunteerService = volunteerService;
    }


    public async Task<PageListResponseDTO<TimesheetDTO>> GetAllAsync(TimesheetListRequestDTO requestDTO)
    {
        PageListRequestEntity<Timesheet> pageListRequestEntity = _mapper.Map<PageListRequestEntity<Timesheet>>(requestDTO);

        if (!string.IsNullOrEmpty(requestDTO.SearchQuery))
        {
            string searchQuery = requestDTO.SearchQuery.Trim().ToLower();

            pageListRequestEntity.Predicate =
                t => t.Status == TimesheetStatus.PENDING && (t.Mission.Title.Trim().ToLower().Contains(searchQuery) || (t.Volunteer.User.FirstName + " " + t.Volunteer.User.LastName).Trim().ToLower().Contains(searchQuery));
        }
        else pageListRequestEntity.Predicate = t => t.Status == TimesheetStatus.PENDING;

        pageListRequestEntity.IncludeExpressions = new Expression<Func<Timesheet, object>>[] { x => x.Mission, x => x.Volunteer };

        pageListRequestEntity.ThenIncludeExpressions = new string[] { "Volunteer.User" };

        pageListRequestEntity.Selects = t =>
                                        new Timesheet
                                        {
                                            Id = t.Id,
                                            GoalAchieved = t.GoalAchieved,
                                            DateVolunteered = t.DateVolunteered,
                                            Time = t.Time,
                                            Mission = new Mission
                                            {
                                                Title = t.Mission.Title,
                                                MissionType = t.Mission.MissionType,
                                            },
                                            Volunteer = new Volunteer
                                            {
                                                User = new User
                                                {
                                                    FirstName = t.Volunteer.User.FirstName,
                                                    LastName = t.Volunteer.User.LastName,
                                                },
                                            },
                                        };

        PageListResponseDTO<Timesheet> pageListResponse = await _unitOfWork.TimesheetRepo.GetAllAsync(pageListRequestEntity);

        List<TimesheetDTO> dto = _mapper.Map<List<TimesheetDTO>>(pageListResponse.Records);

        return new PageListResponseDTO<TimesheetDTO>(pageListResponse.PageIndex, pageListResponse.PageSize, pageListResponse.TotalRecords, dto);
    }


    public async Task<PageListResponseDTO<GoalBaseTimesheetDTO>> GetAllGoalAsync(TimesheetListRequestDTO requestDTO)
    {
        PageListRequestEntity<Timesheet> pageListRequestEntity = _mapper.Map<PageListRequestEntity<Timesheet>>(requestDTO);

        if (!string.IsNullOrEmpty(requestDTO.SearchQuery))
        {
            string searchQuery = requestDTO.SearchQuery.Trim().ToLower();

            pageListRequestEntity.Predicate =
                t => t.Status != TimesheetStatus.DELETED && t.Mission.MissionType == MissionType.Goal && t.Mission.Title.Trim().ToLower().Contains(searchQuery);
        }
        else pageListRequestEntity.Predicate = t => t.Status != TimesheetStatus.DELETED && t.Mission.MissionType == MissionType.Goal;

        pageListRequestEntity.IncludeExpressions = new Expression<Func<Timesheet, object>>[] { x => x.Mission };

        pageListRequestEntity.Selects = t =>
                                        new Timesheet
                                        {
                                            GoalAchieved = t.GoalAchieved,
                                            DateVolunteered = t.DateVolunteered,
                                            Status = t.Status,
                                            Notes = t.Notes,
                                            MissionId = t.MissionId,
                                            Mission = new Mission
                                            {
                                                Title = t.Mission.Title,
                                                Id = t.Mission.Id,
                                                MissionType = t.Mission.MissionType,
                                            }
                                        };

        PageListResponseDTO<Timesheet> pageListResponse = await _unitOfWork.TimesheetRepo.GetAllAsync(pageListRequestEntity);

        List<GoalBaseTimesheetDTO> dto = _mapper.Map<List<GoalBaseTimesheetDTO>>(pageListResponse.Records);

        return new PageListResponseDTO<GoalBaseTimesheetDTO>(pageListResponse.PageIndex, pageListResponse.PageSize, pageListResponse.TotalRecords, dto);
    }


    public async Task<PageListResponseDTO<TimeBaseTimesheetDTO>> GetAllTimeAsync(TimesheetListRequestDTO requestDTO)
    {
        PageListRequestEntity<Timesheet> pageListRequestEntity = _mapper.Map<PageListRequestEntity<Timesheet>>(requestDTO);

        if (!string.IsNullOrEmpty(requestDTO.SearchQuery))
        {
            string searchQuery = requestDTO.SearchQuery.Trim().ToLower();

            pageListRequestEntity.Predicate =
                t => t.Status != TimesheetStatus.DELETED && t.Mission.MissionType == MissionType.Time && t.Mission.Title.Trim().ToLower().Contains(searchQuery);
        }
        else pageListRequestEntity.Predicate = t => t.Status != TimesheetStatus.DELETED && t.Mission.MissionType == MissionType.Time;

        pageListRequestEntity.IncludeExpressions = new Expression<Func<Timesheet, object>>[] { x => x.Mission };

        pageListRequestEntity.Selects = t =>
                                        new Timesheet
                                        {
                                            GoalAchieved = t.GoalAchieved,
                                            DateVolunteered = t.DateVolunteered,
                                            Status = t.Status,
                                            Notes = t.Notes,
                                            MissionId = t.MissionId,
                                            Mission = new Mission
                                            {
                                                Title = t.Mission.Title,
                                                Id = t.Mission.Id,
                                                MissionType = t.Mission.MissionType,
                                            }
                                        };

        PageListResponseDTO<Timesheet> pageListResponse = await _unitOfWork.TimesheetRepo.GetAllAsync(pageListRequestEntity);

        List<TimeBaseTimesheetDTO> dto = _mapper.Map<List<TimeBaseTimesheetDTO>>(pageListResponse.Records);

        return new PageListResponseDTO<TimeBaseTimesheetDTO>(pageListResponse.PageIndex, pageListResponse.PageSize, pageListResponse.TotalRecords, dto);
    }


    public async Task<GoalBaseTimesheetDTO> GetByIdGoal(long id)
    {
        Timesheet? timesheet = await _unitOfWork.TimesheetRepo.GetAsync(timesheet => timesheet.Id == id && timesheet.Status != TimesheetStatus.DELETED && timesheet.Mission.MissionType == MissionType.Goal, includes: new Expression<Func<Timesheet, object>>[] { x => x.Mission },
            selects: t => new Timesheet
            {
                GoalAchieved = t.GoalAchieved,
                DateVolunteered = t.DateVolunteered,
                Status = t.Status,
                Notes = t.Notes,
                MissionId = t.MissionId,
                Mission = new Mission
                {
                    Title = t.Mission.Title,
                    Id = t.Mission.Id,
                    MissionType = t.Mission.MissionType,
                }
            }) ?? throw new EntityNullException(ExceptionMessage.TIMESHEET_NOT_FOUND);

        GoalBaseTimesheetDTO dto = _mapper.Map<GoalBaseTimesheetDTO>(timesheet);

        return dto;
    }


    public async Task<TimeBaseTimesheetDTO> GetByIdTime(long id)
    {
        Timesheet? timesheet = await _unitOfWork.TimesheetRepo.GetAsync(timesheet => timesheet.Id == id && timesheet.Status != TimesheetStatus.DELETED && timesheet.Mission.MissionType == MissionType.Time, includes: new Expression<Func<Timesheet, object>>[] { x => x.Mission },
            selects: t => new Timesheet
            {
                Time = t.Time,
                DateVolunteered = t.DateVolunteered,
                Status = t.Status,
                Notes = t.Notes,
                MissionId = t.MissionId,
                Mission = new Mission
                {
                    Title = t.Mission.Title,
                    Id = t.Mission.Id,
                    MissionType = t.Mission.MissionType,
                }
            }) ?? throw new EntityNullException(ExceptionMessage.TIMESHEET_NOT_FOUND);

        TimeBaseTimesheetDTO dto = _mapper.Map<TimeBaseTimesheetDTO>(timesheet);

        return dto;
    }

    public async Task<TimesheetValidationDTO> GetValidationByMissionId(long id, long sessionUserId)
    {
        Volunteer? volunteer = await _volunteerService.GetVolunteerByUserId(sessionUserId);
        if (volunteer == null || volunteer.Id == 0)
            throw new EntityNullException(ExceptionMessage.VOLUNTEER_NOT_FOUND);

        return await CheckApplieble(id, volunteer.Id);
    }


    public async Task GoalUpsertAsync(GoalBaseTimesheetFormDTO dto, long sessionUserId)
    {
        Volunteer? volunteer = await _volunteerService.GetVolunteerByUserId(sessionUserId);
        if (volunteer == null || volunteer.Id == 0)
            throw new EntityNullException(ExceptionMessage.VOLUNTEER_NOT_FOUND);

        TimesheetValidationDTO validationDTO = await CheckApplieble(dto.MissionId, volunteer.Id);

        CheckGoalBaseTimesheetValidations(dto, validationDTO);

        Timesheet? timesheet = dto.Id != 0 ? await GetTimesheet(dto.Id) : new Timesheet();
        dto.MissionId = dto.Id != 0 ? timesheet.MissionId : dto.MissionId;
        if (timesheet.Status != TimesheetStatus.PENDING) throw new ValidationException(ExceptionMessage.INVALID_OPRATION_TIMESHEET);

        timesheet = _mapper.Map(dto, timesheet);
        timesheet.VolunteerId = volunteer.Id;

        if (dto.Id == 0) await AddAsync(timesheet);
        else await UpdateAsync(timesheet);
    }

    private static void CheckGoalBaseTimesheetValidations(GoalBaseTimesheetFormDTO dto, TimesheetValidationDTO validationDTO)
    {
        if (validationDTO != null && validationDTO.MissionType == MissionType.Goal)
        {
            if (dto.DateVolunteered < validationDTO.StartDate || dto.DateVolunteered > DateTimeOffset.UtcNow)
                throw new ValidationException(ExceptionMessage.TIMESHEET_STARTDATE);
            if (dto.Contribution > validationDTO.RemainingGoal)
                throw new ValidationException(ExceptionMessage.TIMESHEET_GOALVALUE.Replace("#", (dto.Contribution - validationDTO.RemainingGoal).ToString()));
        }
        else throw new ValidationException(ExceptionMessage.INVALID_MISSION_TIMESHEET);
    }

    public async Task TimeUpsertAsync(TimeBaseTimesheetFormDTO dto, long sessionUserId)
    {
        Volunteer? volunteer = await _volunteerService.GetVolunteerByUserId(sessionUserId);
        if (volunteer == null || volunteer.Id == 0)
            throw new EntityNullException(ExceptionMessage.VOLUNTEER_NOT_FOUND);

        TimesheetValidationDTO validationDTO = await CheckApplieble(dto.MissionId, volunteer.Id);

        CheckTimeBaseTimesheetValidations(dto, validationDTO);

        Timesheet? timesheet = dto.Id != 0 ? await GetTimesheet(dto.Id) : new Timesheet();
        dto.MissionId = dto.Id != 0 ? timesheet.MissionId : dto.MissionId;
        if (timesheet.Status != TimesheetStatus.PENDING) throw new ValidationException(ExceptionMessage.INVALID_OPRATION_TIMESHEET);

        timesheet = _mapper.Map(dto, timesheet);
        timesheet.VolunteerId = volunteer.Id;

        if (dto.Id == 0) await AddAsync(timesheet);
        else await UpdateAsync(timesheet);
    }

    public async Task<string> UpdateStatusAsync(long id, JsonPatchDocument<TimesheetPatchDTO> patchDocument, long sessionUserId)
    {
        Timesheet? timesheet = await _unitOfWork.TimesheetRepo.GetByIdAsync(id);

        if (timesheet == null)
        {
            throw new EntityNullException(ExceptionMessage.TIMESHEET_NOT_FOUND);
        }

        TimesheetValidationDTO validationDTO = await CheckApplieble(timesheet.MissionId, timesheet.VolunteerId);

        if (validationDTO != null && validationDTO.MissionType == MissionType.Goal && timesheet.GoalAchieved > validationDTO.RemainingGoal) throw new ValidationException(ExceptionMessage.TIMESHEET_GOALVALUE.Replace("#", (timesheet.GoalAchieved - validationDTO.RemainingGoal).ToString()));

        TimesheetPatchDTO patchDTO = new TimesheetPatchDTO()
        {
            Status = timesheet.Status,
        };

        patchDocument.ApplyTo(patchDTO);

        _mapper.Map(patchDTO, timesheet);

        timesheet.ModifiedBy = sessionUserId;

        await UpdateAsync(timesheet);

        return $"Timesheet {timesheet.Status}!";
    }


    private static void CheckTimeBaseTimesheetValidations(TimeBaseTimesheetFormDTO dto, TimesheetValidationDTO validationDTO)
    {
        if (validationDTO != null && validationDTO.MissionType == MissionType.Time)
        {
            if (dto.DateVolunteered < validationDTO.StartDate || dto.DateVolunteered > DateTimeOffset.UtcNow)
                throw new ValidationException(ExceptionMessage.TIMESHEET_STARTDATE);
            else if (dto.DateVolunteered > validationDTO.EndDate)
                throw new ValidationException(ExceptionMessage.TIMESHEET_ENDDATE);
        }
        else throw new ValidationException(ExceptionMessage.INVALID_MISSION_TIMESHEET);
    }

    public async Task RemoveAsync(long id, long sessionUserId)
    {
        Timesheet? timesheet = await GetTimesheet(id);
        if (timesheet.Status != TimesheetStatus.PENDING) throw new ValidationException(ExceptionMessage.INVALID_OPRATION_TIMESHEET);
        timesheet.Status = TimesheetStatus.DELETED;

        await UpdateAsync(timesheet);
    }

    private async Task<Timesheet> GetTimesheet(long id)
    {
        Timesheet? timesheet = await _unitOfWork.TimesheetRepo.GetAsync(timesheet => timesheet.Id == id && timesheet.Status != TimesheetStatus.DELETED);

        if (timesheet == null) throw new EntityNullException(ExceptionMessage.TIMESHEET_NOT_FOUND);

        return timesheet;
    }

    private async Task<TimesheetValidationDTO> CheckApplieble(long missionId, long volunteerId)
    {
        Mission mission = await _unitOfWork.MissionRepo.GetAsync(m => m.Id == missionId && m.Status == MissionStatus.ACTIVE && m.MissionApplications.Any(x => x.VolunteerId == volunteerId && x.Status == MissionApplicationStatus.APPROVED),
            includes: new Expression<Func<Mission, object>>[] { x => x.MissionApplications.Where(x => x.VolunteerId == volunteerId && x.Status == MissionApplicationStatus.APPROVED) },
            selects: m => new Mission
            {
                ModifiedOn = m.MissionApplications.Select(x => x.ModifiedOn).FirstOrDefault(),
                EndDate = m.EndDate,
                GoalObjectiveAchieved = m.GoalObjectiveAchieved,
                MissionType = m.MissionType,
                GoalObjective = m.GoalObjective,
            }) ?? throw new EntityNullException(ExceptionMessage.INVALID_MISSION_TIMESHEET);

        TimesheetValidationDTO dto = new TimesheetValidationDTO
        {
            StartDate = mission.ModifiedOn,
            EndDate = (mission.EndDate < DateTimeOffset.UtcNow ? mission.EndDate : DateTimeOffset.UtcNow) ?? DateTimeOffset.UtcNow,
            MissionType = mission.MissionType,
            RemainingGoal = 0,
        };

        if (dto.MissionType == MissionType.Goal)
        {
            long total = mission.GoalObjective ?? 0;
            long achieved = mission.GoalObjectiveAchieved ?? 0;
            dto.RemainingGoal = total;
        }

        return dto;
    }
}
