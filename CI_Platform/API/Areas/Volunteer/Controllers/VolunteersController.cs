using API.CustomExceptions;
using API.ExtAuthorization;
using API.Extension;
using API.Utils;
using Common.Constants;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Net;

namespace API.Areas.Volunteer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [VolunteerPolicy]
    public class VolunteersController : ControllerBase
    {
        private readonly IVolunteerService _service;

        public VolunteersController(IVolunteerService service)
        {
            _service = service;
        }


        [HttpGet("profile-details")]
        public async Task<IActionResult> GetDetails()
        {
            long id = HttpContext.GetSessionUser().Id;
            if (id <= 0) throw new UnauthorizedAccessException(ExceptionMessage.UNAUTHORIZED);

            VolunteerProfileInfoDTO dto = await _service.GetById(id);

            return new SuccessResponseHelper<VolunteerProfileInfoDTO>()
                .GetSuccessResponse((int)HttpStatusCode.OK, dto);
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> Update(VolunteerProfileFormDTO dto)
        {
            dto.UserId = HttpContext.GetSessionUser().Id;

            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            await _service.UpdateAsync(dto);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { SuccessMessage.UPDATE_VOLUNTEER_PROFILE });
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
