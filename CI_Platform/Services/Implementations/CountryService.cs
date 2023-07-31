using AutoMapper;
using Common.Constants;
using Common.CustomExceptions;
using Common.Enums;
using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services.Implementations
{
    public class CountryService : BaseService<Country>, ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.CountryRepo, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PageListResponseDTO<CountryDTO>> GetAllCountriesAsync(CountryListRequestDTO countryListRequest)
        {

            PageListRequestEntity<Country> pageListRequestEntity = _mapper.Map<CountryListRequestDTO, PageListRequestEntity<Country>>(countryListRequest);

            if (!string.IsNullOrEmpty(countryListRequest.SearchQuery))
            {
                pageListRequestEntity.Predicate = country => country.Name.Trim().ToLower().Contains(countryListRequest.SearchQuery.ToLower().Trim());
            }

            PageListResponseDTO<Country> pageListResponse = await _unitOfWork.CountryRepo.GetAllAsync(pageListRequestEntity);

            List<CountryDTO> countryDTOs = _mapper.Map<List<Country>, List<CountryDTO>>(pageListResponse.Records);

            return new PageListResponseDTO<CountryDTO>(pageListResponse.PageIndex, pageListResponse.PageSize, pageListResponse.TotalRecords, countryDTOs);
        }

        public async Task AddAsync(CountryDTO countryDTO, long sessionUserId)
        {
            bool isExist = await _unitOfWork.CountryRepo.IsEntityExist(country => country.Name == countryDTO.Name && country.Id != countryDTO.Id);

            if (isExist) throw new DataAlreadyExistsException(ExceptionMessage.COUNTRY_ALREADY_EXIST);

            Country country = _mapper.Map<Country>(countryDTO);

            country.Status = CountryStatus.ACTIVE;
            country.CreatedBy = sessionUserId;
            country.ModifiedBy = sessionUserId;

            await AddAsync(country);
        }

        public async Task UpdateAsync(CountryDTO countryDTO, long sessionUserId)
        {
            Country country = await _unitOfWork.CountryRepo.GetByIdAsync(countryDTO.Id);
            if (country == null)
                throw new EntityNullException(ExceptionMessage.COUNTRY_NOT_FOUND);

            bool isExist = await _unitOfWork.CountryRepo.IsEntityExist(country => country.Name == countryDTO.Name && country.Id != countryDTO.Id);

            if (isExist) throw new DataAlreadyExistsException(ExceptionMessage.COUNTRY_ALREADY_EXIST);

            if (countryDTO.Status == CountryStatus.INACTIVE || countryDTO.Status == CountryStatus.DELETED)
            {
                if (!await IsCityActiveForCountryAsync(countryDTO.Id))
                    throw new ForbiddenException(ExceptionMessage.COUNTRY_IN_USE);
            }

            country.Name = countryDTO.Name;

            if (countryDTO.Status != null)
            {
                country.Status = (CountryStatus)countryDTO.Status;
            }

            country.ModifiedBy = sessionUserId;

            await UpdateAsync(country);
        }


        public async Task RemoveAsync(long id, long sessionUserId)
        {
            Country country = await _unitOfWork.CountryRepo.GetByIdAsync(id);
            if (country == null)
                throw new EntityNullException(ExceptionMessage.COUNTRY_NOT_FOUND);

            if (!await IsCityActiveForCountryAsync(id))
                throw new ForbiddenException(ExceptionMessage.COUNTRY_IN_USE);

            country.Status = CountryStatus.DELETED;
            country.ModifiedBy = sessionUserId;
            await UpdateAsync(country);

        }

        public async Task<CountryDTO> GetByIdAsync(long id)
        {
            Country country = await _unitOfWork.CountryRepo.GetByIdAsync(id);

            if (country == null)
                throw new EntityNullException(ExceptionMessage.COUNTRY_NOT_FOUND);

            if (country == null)
            {
                return null;
            }

            CountryDTO countryDTO = _mapper.Map<CountryDTO>(country);
            return countryDTO;
        }

        public async Task<IEnumerable<DropdownDTO>> GetCountries()
        {
            List<Country> countries = await _unitOfWork.CountryRepo.GetAllAsync(country => country.Status == CountryStatus.ACTIVE);
            if (countries.Count == 0)
            {
                return Enumerable.Empty<DropdownDTO>();
            }

            List<DropdownDTO> DropdownDTOs = _mapper.Map<List<DropdownDTO>>(countries);
            return DropdownDTOs.AsEnumerable();
        }

        public async Task<bool> IsCityActiveForCountryAsync(long countryId)
        {
            return !await _unitOfWork.CityRepo.IsEntityExist(c => c.CountryId == countryId && c.Status == CityStatus.ACTIVE);
        }
    }
}
