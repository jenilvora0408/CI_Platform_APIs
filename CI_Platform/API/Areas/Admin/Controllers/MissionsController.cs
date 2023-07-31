using API.CustomExceptions;
using API.ExtAuthorization;
using API.Extension;
using API.Utils;
using Common.Constants;
using Common.CustomValidationAttributes;
using Common.Utils.Models;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Net;

namespace API.Areas.Admin.Controllers
{
    [Route("admin/[controller]")]
    [ApiController]
    [AdminPolicy]
    public class MissionsController : ControllerBase, IBaseController<MissionFormDTO, long>
    {
        private readonly IMissionService _service;

        public MissionsController(IMissionService service)
        {
            _service = service;
        }


        [HttpPost("list")]
        public async Task<IActionResult> GetList(MissionListRequestDTO dto)
        {
            dto ??= new MissionListRequestDTO();

            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

            PageListResponseDTO<MissionInfoDTO> missionsPage = await _service.GetAllAsync(dto);

            return new SuccessResponseHelper<PageListResponseDTO<MissionInfoDTO>>()
                .GetSuccessResponse((int)HttpStatusCode.OK, missionsPage);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MissionFormDTO dto)
        {
            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.UpsertAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.Created, new List<string> { SuccessMessage.ADD_MISSION });
        }


        [HttpDelete("{id:long}")]
        [ValidateId]
        public async Task<IActionResult> Delete(long id)
        {
            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.RemoveAsync(id, user.Id);
            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { SuccessMessage.DELETE_MISSION });
        }


        [HttpGet("{id:long}")]
        [ValidateId]
        public async Task<IActionResult> GetById(long id)
        {
            MissionDTO? mission = await _service.GetById(id);

            return new SuccessResponseHelper<MissionDTO>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { }, mission);
        }


        [HttpPut("{id:long}")]
        [ValidateId]
        public async Task<IActionResult> Update(long id, [FromForm] MissionFormDTO dto)
        {
            dto.Id = id;
            ModelState.Remove("ThumbnailUrl");
            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.UpsertAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { SuccessMessage.UPDATE_MISSION });
        }
    }
}
