using API.Utils;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Net;

namespace API.Areas.Volunteer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class BannersController : ControllerBase
    {
        private readonly IBannerService _service;

        public BannersController(IBannerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<LoginBannerDTO> dto = await _service.GetAll();

            return new SuccessResponseHelper<IEnumerable<LoginBannerDTO>>()
                .GetSuccessResponse((int)HttpStatusCode.OK, dto);
        }
    }
}
