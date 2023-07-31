using AutoMapper;
using Common.Constants;
using Common.CustomExceptions;
using Common.Enums;
using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System.Linq.Expressions;

namespace Services.Implementations
{
    public class CityService : BaseService<City>, ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.CityRepo, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PageListResponseDTO<CityInfoDTO>> GetAllCitiesAsync(CityListRequestDTO cityListRequest)
        {

            PageListRequestEntity<City> pageListRequestEntity = _mapper.Map<CityListRequestDTO, PageListRequestEntity<City>>(cityListRequest);

            pageListRequestEntity.IncludeExpressions = new Expression<Func<City, object>>[] { x => x.Country };

            if (!string.IsNullOrEmpty(cityListRequest.SearchQuery))
            {
                pageListRequestEntity.Predicate = country => country.Name.Trim().ToLower().Contains(cityListRequest.SearchQuery.ToLower().Trim());
            }

            PageListResponseDTO<City> pageListResponse = await _unitOfWork.CityRepo.GetAllAsync(pageListRequestEntity);

            List<CityInfoDTO> cityDTOs = _mapper.Map<List<City>, List<CityInfoDTO>>(pageListResponse.Records);

            return new PageListResponseDTO<CityInfoDTO>(pageListResponse.PageIndex, pageListResponse.PageSize, pageListResponse.TotalRecords, cityDTOs);
        }

        public async Task AddAsync(CityDTO cityDTO, long sessionUserId)
        {
            bool isCityExist = await _unitOfWork.CityRepo.IsEntityExist(c => c.Name == cityDTO.Name && c.CountryId == cityDTO.CountryId);
            if (isCityExist)
                throw new EntityNullException(ExceptionMessage.CITY_ALREADY_EXIST);

            bool isCountryExist = await IsCountryExists((long)cityDTO.CountryId);
            if (!isCountryExist)
                throw new EntityNullException(ExceptionMessage.COUNTRY_NOT_FOUND);

            if (!isCityExist && isCountryExist)
            {
                City city = _mapper.Map<City>(cityDTO);

                city.Status = CityStatus.ACTIVE;
                city.CreatedBy = sessionUserId;
                city.ModifiedBy = sessionUserId;

                await AddAsync(city);
            }
            else
            {
                throw new DbUpdateException();
            }
        }

        public async Task UpdateAsync(CityDTO cityDTO, long sessionUserID)
        {
            City city = await _unitOfWork.CityRepo.GetByIdAsync(cityDTO.Id);
            if (city == null)
                throw new EntityNullException(ExceptionMessage.CITY_NOT_FOUND);

            bool isCountryExist = await IsCountryExists((long)cityDTO.CountryId);

            if (!isCountryExist)
                throw new EntityNullException(ExceptionMessage.COUNTRY_NOT_FOUND);

            bool isCityUnique = await _unitOfWork.CityRepo.IsEntityExist(c => c.Name == cityDTO.Name && c.CountryId == cityDTO.CountryId && c.Id != cityDTO.Id);

            if (isCityUnique)
                throw new DataAlreadyExistsException(ExceptionMessage.CITY_ALREADY_EXIST);

            if (cityDTO.Status != CityStatus.ACTIVE)
            {
                bool hasActiveMissions = await HasActiveMissions(city);
                if (hasActiveMissions)
                    throw new ForbiddenException(ExceptionMessage.CITY_IN_USE_FOR_MISSION);

                bool hasActiveVolunteers = await HasActiveVolunteers(city);
                if (hasActiveVolunteers)
                    throw new ForbiddenException(ExceptionMessage.CITY_IN_USE_BY_VOLUNTEER);
            }

            if (isCityUnique && isCountryExist)
            {
                city.Id = cityDTO.Id;
                city.Name = cityDTO.Name;
                city.CountryId = (long)cityDTO.CountryId;

                if (cityDTO.Status != null)
                {
                    city.Status = (CityStatus)cityDTO.Status;
                }

                city.ModifiedBy = sessionUserID;
            }

            await UpdateAsync(city);
        }

        public async Task RemoveAsync(long id, long sessionUserId)
        {
            City city = await _unitOfWork.CityRepo.GetByIdAsync(id);
            if (city == null)
                throw new EntityNullException(ExceptionMessage.CITY_NOT_FOUND);

            bool hasActiveMissions = await HasActiveMissions(city);
            if (hasActiveMissions)
                throw new ForbiddenException(ExceptionMessage.CITY_IN_USE_FOR_MISSION);

            bool hasActiveVolunteers = await HasActiveVolunteers(city);
            if (hasActiveVolunteers)
                throw new ForbiddenException(ExceptionMessage.CITY_IN_USE_BY_VOLUNTEER);

            if (city != null)
            {
                city.Status = CityStatus.DELETED;
                city.ModifiedBy = sessionUserId;
                await UpdateAsync(city);
            }
        }

        public async Task<CityDTO> GetByIdAsync(long id)
        {
            City city = await _unitOfWork.CityRepo.GetByIdAsync(id);

            if (city == null)
                throw new EntityNullException(ExceptionMessage.CITY_NOT_FOUND);

            if (city == null)
            {
                return null;
            }

            CityDTO cityDTO = _mapper.Map<CityDTO>(city);
            return cityDTO;
        }

        public async Task<IEnumerable<DropdownDTO>> GetCities(long id)
        {
            List<City> city = await _unitOfWork.CityRepo.GetAllAsync(city => city.Status == CityStatus.ACTIVE && city.CountryId == id);
            if (city.Count == 0)
            {
                return Enumerable.Empty<DropdownDTO>();
            }

            List<DropdownDTO> DropdownDTOs = _mapper.Map<List<DropdownDTO>>(city);
            return DropdownDTOs.AsEnumerable();
        }

        private async Task<bool> IsCountryExists(long countryId) => await _unitOfWork.CountryRepo.IsEntityExist(country => country.Id == countryId && country.Status == CountryStatus.ACTIVE);

        private async Task<bool> HasActiveMissions(City city)
        {
            bool hasActiveMissions = await _unitOfWork.MissionRepo.IsEntityExist(m => m.CityId == city.Id && m.Status == MissionStatus.ACTIVE);
            return hasActiveMissions;
        }

        private async Task<bool> HasActiveVolunteers(City city)
        {
            bool hasActiveVolunteers = await _unitOfWork.VolunteerRepo.IsEntityExist(v => v.CityId == city.Id && v.User.Status == UserStatus.ACTIVE);
            return hasActiveVolunteers;
        }
    }
}