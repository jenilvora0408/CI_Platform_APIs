using Entities.DTOs;

namespace Services.Interfaces;
public interface IStoryService
{
    Task<string> UpsertAsync(VolunteerStoryDTO dto, long sessionUserModel); 
}
