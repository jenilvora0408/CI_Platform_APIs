using Entities.DataModels;
using Entities.DTOs;

namespace Services.Interfaces
{
    public interface IMissionThemeService : IBaseService<MissionTheme>
    {
        Task<PageListResponseDTO<MissionThemeDTO>> GetAllThemesAsync(MissionThemeListRequestDTO missionThemeListRequest);

        Task AddAsync(MissionThemeDTO missionThemeDTO, long sessionUserId);

        Task UpdateAsync(MissionThemeDTO missionThemeDTO, long sessionUserId);

        Task RemoveAsync(long id, long sessionUserId);

        Task<MissionThemeDTO> GetByIdAsync(long id);

        Task<IEnumerable<DropdownDTO>> GetThemes();
    }
}
