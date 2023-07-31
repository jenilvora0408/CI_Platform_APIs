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
    public class MissionThemeController : ControllerBase, IBaseController<MissionThemeDTO, long>
    {
        private readonly IMissionThemeService _service;

        public MissionThemeController(IMissionThemeService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(MissionThemeDTO dto)
        {
            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.AddAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.Created, new List<string> { SuccessMessage.ADD_MISSION_THEME });
        }

        [HttpDelete]
        [ValidateId]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.RemoveAsync(id, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { SuccessMessage.DELETE_MISSION_THEME });
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetList(MissionThemeListRequestDTO missionThemeListRequest)
        {
            if (missionThemeListRequest == null)
            {
                missionThemeListRequest = new MissionThemeListRequestDTO();
            }

            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

            PageListResponseDTO<MissionThemeDTO> missionThemePage = await _service.GetAllThemesAsync(missionThemeListRequest);

            return new SuccessResponseHelper<PageListResponseDTO<MissionThemeDTO>>()
                .GetSuccessResponse((int)HttpStatusCode.OK, missionThemePage);
        }

        [HttpGet]
        [ValidateId]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            MissionThemeDTO missionTheme = await _service.GetByIdAsync((long)id);

            return new SuccessResponseHelper<MissionThemeDTO>()
                .GetSuccessResponse((int)HttpStatusCode.OK, missionTheme);
        }

        [HttpPut]
        [ValidateId]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] MissionThemeDTO dto)
        {
            dto.Id = id;

            SessionUserModel user = HttpContext.GetSessionUser();

            if (!ModelState.IsValid)
            {
                throw new InvalidModelStateException(ModelState);
            }

            await _service.UpdateAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { SuccessMessage.UPDATE_MISSION_THEME });
        }

        [HttpGet]
        [Route("list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMissionThemeList()
        {
            IEnumerable<DropdownDTO> themePage = await _service.GetThemes();

            return new SuccessResponseHelper<IEnumerable<DropdownDTO>>().GetSuccessResponse((int)HttpStatusCode.OK, themePage);
        }
    }
}
