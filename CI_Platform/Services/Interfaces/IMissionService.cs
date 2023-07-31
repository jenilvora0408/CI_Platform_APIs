using Entities.DataModels;
using Entities.DTOs;

namespace Services.Interfaces;

public interface IMissionService : IBaseService<Mission>
{
    Task UpsertAsync(MissionFormDTO dto, long sessionUserId);

    Task<PageListResponseDTO<MissionInfoDTO>> GetAllAsync(MissionListRequestDTO missionListRequest);

    Task<MissionDTO> GetById(long id);

    Task RemoveAsync(long id, long sessionUserId);

    Task<bool> CheckForMission(long missionId);
}
