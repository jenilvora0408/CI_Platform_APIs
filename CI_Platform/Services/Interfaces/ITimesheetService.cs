using Entities.DataModels;
using Entities.DTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Interfaces;

public interface ITimesheetService : IBaseService<Timesheet>
{
    Task GoalUpsertAsync(GoalBaseTimesheetFormDTO dto, long sessionUserId);
    Task TimeUpsertAsync(TimeBaseTimesheetFormDTO dto, long sessionUserId);

    Task<PageListResponseDTO<TimesheetDTO>> GetAllAsync(TimesheetListRequestDTO requestDTO);
    Task<PageListResponseDTO<GoalBaseTimesheetDTO>> GetAllGoalAsync(TimesheetListRequestDTO requestDTO);
    Task<PageListResponseDTO<TimeBaseTimesheetDTO>> GetAllTimeAsync(TimesheetListRequestDTO requestDTO);

    Task<GoalBaseTimesheetDTO> GetByIdGoal(long id);
    Task<TimeBaseTimesheetDTO> GetByIdTime(long id);
    Task<TimesheetValidationDTO> GetValidationByMissionId(long id, long sessionUserId);

    Task RemoveAsync(long id, long sessionUserId);

    Task<string> UpdateStatusAsync(long id, JsonPatchDocument<TimesheetPatchDTO> patchDocument, long sessionUserId);
}
