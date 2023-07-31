using API.CustomExceptions;
using API.ExtAuthorization;
using API.Extension;
using API.Utils;
using Common.CustomValidationAttributes;
using Common.Utils.Models;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Net;

namespace API.Areas.Volunteer.Controllers;
[Route("[controller]")]
[ApiController]
[VolunteerPolicy]
public class StoriesController : ControllerBase, IBaseController<VolunteerStoryDTO, long>
{
    private readonly IStoryService _service;

    public StoriesController(IStoryService service)
    {
        _service = service;
    }

    [HttpPost]
    [ValidateIdOnCreate]
    public async Task<IActionResult> Create([FromForm] VolunteerStoryDTO dto)
    {
        if (!ModelState.IsValid)
        {
            throw new InvalidModelStateException(ModelState);
        }

        SessionUserModel user = HttpContext.GetSessionUser();

        string message = await _service.UpsertAsync(dto, user.Id);

        return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.Created, new List<string> { message }); ;
    }

    [HttpDelete]
    public Task<IActionResult> Delete(long id)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public Task<IActionResult> GetById(long id)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public Task<IActionResult> Update(long id, VolunteerStoryDTO dto)
    {
        throw new NotImplementedException();
    }
}
