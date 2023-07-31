using Entities.DataModels;
using Entities.DTOs;

namespace Services.Interfaces
{
    public interface ICityService : IBaseService<City>
    {
        Task<PageListResponseDTO<CityInfoDTO>> GetAllCitiesAsync(CityListRequestDTO cityListRequest);

        Task AddAsync(CityDTO cityDTO, long sessionUserId);

        Task UpdateAsync(CityDTO cityDTO, long sessionUserID);

        Task RemoveAsync(long id, long sessionUserId);

        Task<CityDTO> GetByIdAsync(long id);

        Task<IEnumerable<DropdownDTO>> GetCities(long id);
    }
}
