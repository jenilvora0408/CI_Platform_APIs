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
    public class AdminsController : ControllerBase, IBaseController<AdminDTO, long>
    {
        private readonly IAdminService _service;

        public AdminsController(IAdminService service)
        {
            _service = service;
        }


        [HttpPost("list")]
        public async Task<IActionResult> GetList(AdminListRequestDTO dto)
        {
            dto ??= new AdminListRequestDTO();

            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

            PageListResponseDTO<AdminInfoDTO> adminsPage = await _service.GetAllAsync(dto);

            return new SuccessResponseHelper<PageListResponseDTO<AdminInfoDTO>>()
                .GetSuccessResponse((int)HttpStatusCode.OK, adminsPage);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AdminDTO dto)
        {
            ModelState.Remove("Status");
            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.UpsertAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.Created, new List<string> { SuccessMessage.ADMIN_REGISTERATION });
        }


        [HttpDelete("{id:long}")]
        [ValidateId]
        public async Task<IActionResult> Delete(long id)
        {
            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.RemoveAsync(id, user.Id);
            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { SuccessMessage.ADMIN_DELETED });
        }


        [HttpPut("{id:long}")]
        [ValidateId]
        public async Task<IActionResult> Update(long id, [FromForm] AdminDTO dto)
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
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { SuccessMessage.ADMIN_UPDATED });
        }


        [HttpGet("{id:long}")]
        [ValidateId]
        public async Task<IActionResult> GetById(long id)
        {
            AdminInfoDTO? adminInfoDTO = await _service.GetById(id);

            return new SuccessResponseHelper<AdminInfoDTO>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { }, adminInfoDTO);
        }

        [HttpPost("avatar")]
        public async Task<IActionResult> ChangeProfileImage(IFormFile avatar)
        {
            await _service.UpdateAvatarAsync(avatar, HttpContext.GetSessionUser().Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { SuccessMessage.UPDATE_AVATAR });
        }
    }
}
