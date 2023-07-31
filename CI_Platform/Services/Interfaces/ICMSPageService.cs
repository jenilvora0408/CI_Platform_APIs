using Entities.DataModels;
using Entities.DTOs;

namespace Services.Interfaces
{
    public interface ICMSPageService : IBaseService<CMSPage>
    {
        Task<PageListResponseDTO<CMSPageDTO>> GetAllCMSPagesAsync(CMSPageListRequestDTO cmsPageListRequest);

        Task AddAsync(CMSPageDTO cmsPageDTO, long sessionUserId);

        Task UpdateAsync(CMSPageDTO cmsPageDTO, long sessionUserId);

        Task RemoveAsync(long id, long sessionUserId);

        Task<CMSPageDTO> GetByIdAsync(long id);
    }
}
