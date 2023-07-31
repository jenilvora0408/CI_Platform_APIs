using Entities.DataModels;
using Entities.DTOs;

namespace Services.Interfaces
{
    public interface IMissionCommentService : IBaseService<MissionComment>
    {
        Task PostComments(MissionCommentDTO dto, long sessionUserId);

        Task<List<CommentInfoDTO>> GetCommentsForMissionAsync(long missionId);
    }
}
