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
    public class CMSPageController : ControllerBase, IBaseController<CMSPageDTO, long>
    {
        private readonly ICMSPageService _service;

        public CMSPageController(ICMSPageService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CMSPageDTO dto)
        {
            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.AddAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.Created, new List<string> { SuccessMessage.ADD_CMS_PAGE });
        }

        [HttpDelete]
        [ValidateId]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.RemoveAsync(id, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { SuccessMessage.DELETE_CMS_PAGE });
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetList(CMSPageListRequestDTO cmsPageListRequest)
        {
            if (cmsPageListRequest == null)
            {
                cmsPageListRequest = new CMSPageListRequestDTO();
            }

            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

            PageListResponseDTO<CMSPageDTO> cmsPage = await _service.GetAllCMSPagesAsync(cmsPageListRequest);

            return new SuccessResponseHelper<PageListResponseDTO<CMSPageDTO>>()
                .GetSuccessResponse((int)HttpStatusCode.OK, cmsPage);
        }

        [HttpGet]
        [ValidateId]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            CMSPageDTO cmsPageDTO = await _service.GetByIdAsync((long)id);

            return new SuccessResponseHelper<CMSPageDTO>()
                .GetSuccessResponse((int)HttpStatusCode.OK, cmsPageDTO);
        }

        [HttpPut]
        [ValidateId]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] CMSPageDTO dto)
        {
            dto.Id = id;

            SessionUserModel user = HttpContext.GetSessionUser();

            if (!ModelState.IsValid)
            {
                throw new InvalidModelStateException(ModelState);
            }

            await _service.UpdateAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { SuccessMessage.UPDATE_CMS_PAGE });
        }
    }
}
