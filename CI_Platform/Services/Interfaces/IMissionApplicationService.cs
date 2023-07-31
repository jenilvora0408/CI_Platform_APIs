using Entities.DataModels;
using Entities.DTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Interfaces;
public interface IMissionApplicationService
{
    Task CreateAsync(VolunteerMissionApplicationDTO dto, long sessionUserId);
    Task<IEnumerable<DropdownDTO>> GetAppliedMissionsListAsync(long sessionUserId);

    #region Admin side
    Task<PageListResponseDTO<MissionApplicationInfoDTO>> GetAllAsync(MissionApplicationListRequestDTO requestDTO);
    Task<string> UpdateStatusAsync(long id, JsonPatchDocument<MissionApplicationPatchDTO> patchDocument, long sessionUserId);
    Task<bool> CheckApprovalStatus(long missionId, long volunteerId);
    #endregion

    #region Recent_Volunteer

    Task<PageListResponseDTO<RecentVolunteerDTO>> GetApprovedMissionApplicationsByMissionId(long missionId, int pageIndex);

    #endregion
}
