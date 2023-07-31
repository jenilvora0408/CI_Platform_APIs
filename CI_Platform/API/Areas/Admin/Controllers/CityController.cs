using API.CustomExceptions;
using API.ExtAuthorization;
using API.Extension;
using API.Utils;
using Common.Constants;
using Common.CustomValidationAttributes;
using Common.Utils.Models;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Net;

namespace API.Areas.Admin.Controllers
{
    [Route("admin/[controller]")]
    [ApiController]
    [AdminPolicy]
    public class CityController : ControllerBase, IBaseController<CityDTO, long>
    {
        private readonly ICityService _service;
        private readonly ICountryService _countryService;

        public CityController(ICityService service, ICountryService countryService)
        {
            _service = service;
            _countryService = countryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CityDTO dto)
        {
            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.AddAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
            .GetSuccessResponse((int)HttpStatusCode.Created, new List<string> { SuccessMessage.ADD_CITY });
        }

        [HttpDelete]
        [ValidateId]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.RemoveAsync((long)id, user.Id);

            return new SuccessResponseHelper<object>()
            .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { SuccessMessage.DELETE_CITY });
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetList([FromBody] CityListRequestDTO cityListRequest)
        {
            if (cityListRequest == null)
            {
                cityListRequest = new CityListRequestDTO();
            }

            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

            PageListResponseDTO<CityInfoDTO> citiesPage = await _service.GetAllCitiesAsync(cityListRequest);

            return new SuccessResponseHelper<PageListResponseDTO<CityInfoDTO>>()
            .GetSuccessResponse((int)HttpStatusCode.OK, citiesPage);
        }

        [HttpGet]
        [ValidateId]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            CityDTO city = await _service.GetByIdAsync((long)id);

            return new SuccessResponseHelper<CityDTO>()
            .GetSuccessResponse((int)HttpStatusCode.OK, city);
        }

        [HttpPut]
        [ValidateId]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] CityDTO dto)
        {
            dto.Id = id;

            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.UpdateAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
            .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { SuccessMessage.UPDATE_CITY });
        }

        [HttpGet]
        [Route("list/{id}")]
        [ValidateId]
        [AllowAnonymous]
        public async Task<IActionResult> GetCityList(long id)
        {
            IEnumerable<DropdownDTO> citiesPage = await _service.GetCities(id);

            return new SuccessResponseHelper<IEnumerable<DropdownDTO>>().GetSuccessResponse((int)HttpStatusCode.OK, citiesPage);
        }
    }
}