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

namespace API.Areas.Volunteer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [VolunteerPolicy]
    public class TimesheetsController : ControllerBase
    {
        private readonly ITimesheetService _service;

        public TimesheetsController(ITimesheetService service)
        {
            _service = service;
        }

        [HttpPost("time-base/list")]
        public async Task<IActionResult> GetListTime([FromBody] TimesheetListRequestDTO dto)
        {
            if (dto == null) dto = new TimesheetListRequestDTO();

            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

            PageListResponseDTO<TimeBaseTimesheetDTO> timesheet = await _service.GetAllTimeAsync(dto);

            return new SuccessResponseHelper<PageListResponseDTO<TimeBaseTimesheetDTO>>()
                .GetSuccessResponse((int)HttpStatusCode.OK, timesheet);
        }

        [HttpPost("goal-base/list")]
        public async Task<IActionResult> GetListGoal([FromBody] TimesheetListRequestDTO dto)
        {
            if (dto == null) dto = new TimesheetListRequestDTO();

            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

            PageListResponseDTO<GoalBaseTimesheetDTO> timesheet = await _service.GetAllGoalAsync(dto);

            return new SuccessResponseHelper<PageListResponseDTO<GoalBaseTimesheetDTO>>()
                .GetSuccessResponse((int)HttpStatusCode.OK, timesheet);
        }


        [HttpPost("time-base")]
        public async Task<IActionResult> TimeBaseCreate(TimeBaseTimesheetFormDTO dto)
        {
            if (dto == null) dto = new TimeBaseTimesheetFormDTO();

            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.TimeUpsertAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.Created, new List<string> { SuccessMessage.ADD_TIMESHEET });
        }

        [HttpPost("goal-base")]
        public async Task<IActionResult> GoalBaseCreate(GoalBaseTimesheetFormDTO dto)
        {
            if (dto == null) dto = new GoalBaseTimesheetFormDTO();

            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.GoalUpsertAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.Created, new List<string> { SuccessMessage.ADD_TIMESHEET });
        }


        [HttpPut("time-base/{id:long}")]
        [ValidateId]
        public async Task<IActionResult> UpdateTime(long id, TimeBaseTimesheetFormDTO dto)
        {
            if (dto == null) dto = new TimeBaseTimesheetFormDTO();
            dto.Id = id;
            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.TimeUpsertAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)(HttpStatusCode.OK), new List<string> { SuccessMessage.UPDATE_TIMESHEET });
        }

        [HttpPut("goal-base/{id:long}")]
        [ValidateId]
        public async Task<IActionResult> UpdateGoal(long id, GoalBaseTimesheetFormDTO dto)
        {
            if (dto == null) dto = new GoalBaseTimesheetFormDTO();
            dto.Id = id;
            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.GoalUpsertAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)(HttpStatusCode.OK), new List<string> { SuccessMessage.UPDATE_TIMESHEET });
        }


        [HttpGet("time-base/{id:long}")]
        [ValidateId]
        public async Task<IActionResult> GetByIdTime(long id)
        {
            TimeBaseTimesheetDTO? dto = await _service.GetByIdTime(id);

            return new SuccessResponseHelper<TimeBaseTimesheetDTO>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { }
                , dto);
        }

        [HttpGet("goal-base/{id:long}")]
        [ValidateId]
        public async Task<IActionResult> GetByIdGoal(long id)
        {
            GoalBaseTimesheetDTO? dto = await _service.GetByIdGoal(id);

            return new SuccessResponseHelper<GoalBaseTimesheetDTO>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { }
                , dto);
        }


        [HttpDelete("{id:int}")]
        [ValidateId]
        public async Task<IActionResult> Delete(int id)
        {
            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.RemoveAsync(id, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)(HttpStatusCode.OK), new List<string> { SuccessMessage.DELETE_TIMESHEET });
        }


        [HttpGet("validation/{id:long}")]
        [ValidateId]
        public async Task<IActionResult> ValidationByMissionId(long id)
        {
            SessionUserModel user = HttpContext.GetSessionUser();

            TimesheetValidationDTO? dto = await _service.GetValidationByMissionId(id, user.Id);

            return new SuccessResponseHelper<TimesheetValidationDTO>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { }
                , dto);
        }
    }
}
