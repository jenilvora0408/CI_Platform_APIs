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
    public class CountriesController : ControllerBase, IBaseController<CountryDTO, long>
    {
        private readonly ICountryService _service;

        public CountriesController(ICountryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CountryDTO dto)
        {
            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.AddAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.Created, new List<string> { SuccessMessage.ADD_COUNTRY });
        }

        [HttpDelete]
        [ValidateId]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.RemoveAsync((long)id, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { SuccessMessage.DELETE_COUNTRY });
        }


        [HttpPost("list")]
        public async Task<IActionResult> GetList(CountryListRequestDTO countryListRequest)
        {
            if (countryListRequest == null)
            {
                countryListRequest = new CountryListRequestDTO();
            }

            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

            PageListResponseDTO<CountryDTO> countriesPage = await _service.GetAllCountriesAsync(countryListRequest);

            return new SuccessResponseHelper<PageListResponseDTO<CountryDTO>>()
                .GetSuccessResponse((int)HttpStatusCode.OK, countriesPage);
        }

        [HttpPut]
        [ValidateId]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] CountryDTO dto)
        {
            dto.Id = id;

            SessionUserModel user = HttpContext.GetSessionUser();

            if (!ModelState.IsValid)
            {
                throw new InvalidModelStateException(ModelState);
            }

            await _service.UpdateAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { SuccessMessage.UPDATE_COUNTRY });
        }

        [HttpGet]
        [ValidateId]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            CountryDTO country = await _service.GetByIdAsync((long)id);

            return new SuccessResponseHelper<CountryDTO>()
                .GetSuccessResponse((int)HttpStatusCode.OK, country);
        }

        [HttpGet]
        [Route("list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCountryList()
        {
            IEnumerable<DropdownDTO> countriesPage = await _service.GetCountries();

            return new SuccessResponseHelper<IEnumerable<DropdownDTO>>().GetSuccessResponse((int)HttpStatusCode.OK, countriesPage);
        }
    }
}
