using Entities.DataModels;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Services.Interfaces;
public interface IVolunteerService : IBaseService<Volunteer>
{
    Task UpsertAsync(VolunteerDTO dto, long sessionUserId);

    Task RemoveAsync(long id, long sessionUserId);

    Task<VolunteerInfoDTO> GetByIdAsync(long id);

    Task<PageListResponseDTO<VolunteerInfoDTO>> GetAllAsync(VolunteerListRequestDTO dto);

    Task<IEnumerable<DropdownDTO>> GetVolunteers();

    Task<Volunteer> GetVolunteerByUserId(long userId);
    
    Task<VolunteerProfileInfoDTO> GetById(long id);

    Task UpdateAsync(VolunteerProfileFormDTO dto);

    Task UpdateAvatarAsync(IFormFile avatar, long id);
}
