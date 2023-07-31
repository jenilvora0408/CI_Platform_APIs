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
    public class SkillsController : ControllerBase, IBaseController<SkillDTO, int>
    {
        private readonly ISkillService _service;

        public SkillsController(ISkillService service)
        {
            _service = service;
        }


        [HttpPost("list")]
        public async Task<IActionResult> GetList([FromBody] SkillListRequestDTO dto)
        {
            if (dto == null) dto = new SkillListRequestDTO();

            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

            PageListResponseDTO<SkillDTO> skillsPage = await _service.GetAllSkills(dto);

            return new SuccessResponseHelper<PageListResponseDTO<SkillDTO>>()
                .GetSuccessResponse((int)HttpStatusCode.OK, skillsPage);
        }


        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetList()
        {
            IEnumerable<DropdownDTO> dto = await _service.GetSkills();

            return new SuccessResponseHelper<IEnumerable<DropdownDTO>>()
                .GetSuccessResponse((int)HttpStatusCode.OK, dto);
        }


        [HttpPost]
        public async Task<IActionResult> Create(SkillDTO dto)
        {
            if (dto == null) dto = new SkillDTO();

            ModelState.Remove("Status");
            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.UpsertAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.Created, new List<string> { SuccessMessage.ADD_SKILL });
        }


        [HttpDelete("{id:int}")]
        [ValidateId]
        public async Task<IActionResult> Delete(int id)
        {
            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.RemoveAsync(id, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)(HttpStatusCode.OK), new List<string> { SuccessMessage.DELETE_SKILL });
        }


        [HttpGet("{id:int}")]
        [ValidateId]
        public async Task<IActionResult> GetById(int id)
        {
            SkillDTO? skillDTO = await _service.GetById(id);

            return new SuccessResponseHelper<SkillDTO>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { }
                , skillDTO);
        }


        [HttpPut("{id:int}")]
        [ValidateId]
        public async Task<IActionResult> Update(int id, SkillDTO dto)
        {
            if (dto == null) dto = new SkillDTO();
            dto.Id = id;
            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.UpsertAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)(HttpStatusCode.OK), new List<string> { SuccessMessage.UPDATE_SKILL });
        }


        //[HttpPost("CheckDuplicate")]
        //public async Task<IActionResult> IsDuplicate(string skillName)
        //{
        //    bool isDuplicate = await _service.IsDuplicate(skillName);

        //    return new SuccessResponseHelper<object>()
        //        .GetSuccessResponse((int)(isDuplicate? HttpStatusCode.OK : HttpStatusCode.Conflict), new List<string> { SuccessMessage.UPDATE_SKILL });
        //}
    }
}
