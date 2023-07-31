using Entities.DataModels;
using Entities.DTOs;

namespace Services.Interfaces
{
    public interface IVolunteerMissionService : IBaseService<Mission>
    {
        Task<PageListResponseDTO<VolunteerMissionDTO>> GetAllAsync(VolunteerMissionListRequestDTO missionListRequest, long sessionUserId);

        Task UpsertRatingsAsync(MissionRatingDTO dto, long sessionUserId);

        Task<bool> SetFavoriteStatusAsync(long missionId, long sessionUserId);

        Task<VolunteerMissionInfoDTO> GetById(long missionId, long sessionUserId);
        
        Task<List<RelatedMissionDTO>> GetRelatedMissionsAsync(long missionId, long sessionUserId);
    }
}