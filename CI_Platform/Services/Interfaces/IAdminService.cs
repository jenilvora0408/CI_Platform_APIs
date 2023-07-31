using Entities.DataModels;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Services.Interfaces
{
    public interface IAdminService : IBaseService<Admin>
    {
        Task UpsertAsync(AdminDTO adminDTO, long sessionUserId);

        Task<PageListResponseDTO<AdminInfoDTO>> GetAllAsync(AdminListRequestDTO adminListRequest);

        Task<AdminInfoDTO> GetById(long id);

        Task RemoveAsync(long id, long sessionUserId);

        Task UpdateAvatarAsync(IFormFile avatar, long id);
    }
}