using API.CustomExceptions;
using API.ExtAuthorization;
using API.Extension;
using API.Utils;
using Common.Constants;
using Common.Utils.Models;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Net;

namespace API.Areas.Volunteer.Controllers;

[Route("[controller]")]
[ApiController]
[VolunteerPolicy]
public class MissionApplicationsController : ControllerBase
{
    private readonly IMissionApplicationService _service;

    public MissionApplicationsController(IMissionApplicationService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VolunteerMissionApplicationDTO dto)
    {
        if (!ModelState.IsValid)
        {
            throw new InvalidModelStateException(ModelState);
        }

        SessionUserModel user = HttpContext.GetSessionUser();
        await _service.CreateAsync(dto, user.Id);

        return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.Created, new List<string> { SuccessMessage.MISSION_APPLY_SUCCESS });
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetList()
    {
        SessionUserModel user = HttpContext.GetSessionUser();

        IEnumerable<DropdownDTO> dropdownList = await _service.GetAppliedMissionsListAsync(user.Id);

        return new SuccessResponseHelper<IEnumerable<DropdownDTO>>()
            .GetSuccessResponse((int)HttpStatusCode.OK, dropdownList);
    }
}