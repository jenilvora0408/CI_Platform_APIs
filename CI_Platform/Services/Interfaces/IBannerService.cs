using Entities.DTOs;

namespace Services.Interfaces;
public interface IBannerService
{
    Task UpsertAsync(BannerDTO dto, long sessionUserId);

    Task RemoveAsync(int id, long sessionUserId);

    Task<BannerInfoDTO> GetByIdAsync(int id);

    public Task UpdateSortOrder(BannerSortOrderDTO dto, long sessionUserId);

    Task<PageListResponseDTO<BannerInfoDTO>> GetAllAsync(BannerListRequestDTO dto);

    Task<IEnumerable<LoginBannerDTO>> GetAll();
}