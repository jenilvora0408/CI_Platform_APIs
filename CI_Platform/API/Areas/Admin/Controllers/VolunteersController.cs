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
    public class VolunteersController : ControllerBase, IBaseController<VolunteerDTO, long>
    {
        private readonly IVolunteerService _service;

        public VolunteersController(IVolunteerService service)
        {
            _service = service;
        }


        [HttpPost("list")]
        public async Task<IActionResult> GetList(VolunteerListRequestDTO dto)
        {
            if (dto == null) dto = new VolunteerListRequestDTO();

            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

            PageListResponseDTO<VolunteerInfoDTO> list = await _service.GetAllAsync(dto);

            return new SuccessResponseHelper<PageListResponseDTO<VolunteerInfoDTO>>()
                .GetSuccessResponse((int)HttpStatusCode.OK, list);
        }


        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetList()
        {
            IEnumerable<DropdownDTO> dto = await _service.GetVolunteers();

            return new SuccessResponseHelper<IEnumerable<DropdownDTO>>()
                .GetSuccessResponse((int)HttpStatusCode.OK, dto);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] VolunteerDTO dto)
        {
            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.UpsertAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.Created, new List<string> { SuccessMessage.ADD_VOLUNTEER });
        }

        [HttpDelete("{id:long}")]
        [ValidateId]
        public async Task<IActionResult> Delete(long id)
        {
            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.RemoveAsync(id, user.Id);
            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { SuccessMessage.DELETE_VOLUNTEER });
        }

        [HttpPut("{id:long}")]
        [ValidateId]
        public async Task<IActionResult> Update(long id, [FromForm] VolunteerDTO dto)
        {
            dto.Id = id;

            ModelState.Remove("Email");
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.UpsertAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { SuccessMessage.UPDATE_VOLUNTEER });
        }


        [HttpGet("{id:long}")]
        [ValidateId]
        public async Task<IActionResult> GetById(long id)
        {
            VolunteerInfoDTO dto = await _service.GetByIdAsync(id);

            return new SuccessResponseHelper<VolunteerInfoDTO>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { }, dto);
        }
    }
}