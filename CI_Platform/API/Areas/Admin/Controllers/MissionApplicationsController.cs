using API.CustomExceptions;
using API.ExtAuthorization;
using API.Extension;
using API.Utils;
using Common.Utils.Models;
using Entities.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Net;

namespace API.Areas.Admin.Controllers;
[Route("admin/[controller]")]
[ApiController]
[AdminPolicy]
public class MissionApplicationsController : ControllerBase
{
    private readonly IMissionApplicationService _service;

    public MissionApplicationsController(IMissionApplicationService service)
    {
        _service = service;
    }


    [HttpPost("list")]
    public async Task<IActionResult> GetList(MissionApplicationListRequestDTO dto)
    {
        if (dto == null) dto = new MissionApplicationListRequestDTO();

        if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

        PageListResponseDTO<MissionApplicationInfoDTO> list = await _service.GetAllAsync(dto);
        return new SuccessResponseHelper<PageListResponseDTO<MissionApplicationInfoDTO>>()
                .GetSuccessResponse((int)HttpStatusCode.OK, list);
    }

    [HttpPatch("update-status/{id:long}")]
    public async Task<IActionResult> UpdateStatus(long id, [FromBody] JsonPatchDocument<MissionApplicationPatchDTO> patchDocument)
    {
        if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

        SessionUserModel user = HttpContext.GetSessionUser();

        string response = await _service.UpdateStatusAsync(id, patchDocument, user.Id);

        return new SuccessResponseHelper<object>()
               .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { response });
    }
}