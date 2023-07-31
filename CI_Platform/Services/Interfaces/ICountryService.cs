using Entities.DataModels;
using Entities.DTOs;

namespace Services.Interfaces
{
    public interface ICountryService : IBaseService<Country>
    {
        Task<PageListResponseDTO<CountryDTO>> GetAllCountriesAsync(CountryListRequestDTO countryListRequest);

        Task AddAsync(CountryDTO countryDTO, long sessionUserId);

        Task UpdateAsync(CountryDTO countryDTO, long sessionUserID);

        Task RemoveAsync(long id, long sessionUserId);

        Task<CountryDTO> GetByIdAsync(long id);

        Task<IEnumerable<DropdownDTO>> GetCountries();
    }
}