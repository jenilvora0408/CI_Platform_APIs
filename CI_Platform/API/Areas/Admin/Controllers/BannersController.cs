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
    public class BannersController : ControllerBase, IBaseController<BannerDTO, int>
    {
        private readonly IBannerService _service;

        public BannersController(IBannerService service)
        {
            _service = service;
        }


        [HttpPost("list")]
        public async Task<IActionResult> GetList(BannerListRequestDTO dto)
        {
            if (dto == null) dto = new BannerListRequestDTO();

            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

            PageListResponseDTO<BannerInfoDTO> list = await _service.GetAllAsync(dto);

            return new SuccessResponseHelper<PageListResponseDTO<BannerInfoDTO>>()
                .GetSuccessResponse((int)HttpStatusCode.OK, list);
        }


        [HttpPost]
        [ValidateIdOnCreate]
        public async Task<IActionResult> Create([FromForm] BannerDTO dto)
        {
            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.UpsertAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.Created, new List<string> { SuccessMessage.ADD_BANNER });
        }

        [HttpDelete("{id:int}")]
        [ValidateId]
        public async Task<IActionResult> Delete(int id)
        {
            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.RemoveAsync(id, user.Id);
            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { SuccessMessage.DELETE_BANNER });
        }

        [HttpPut("{id:int}")]
        [ValidateId]
        public async Task<IActionResult> Update(int id, [FromForm] BannerDTO dto)
        {
            dto.Id = id;
            ModelState.Remove("Image");

            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.UpsertAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { SuccessMessage.UPDATE_BANNER });
        }


        [HttpGet("{id:int}")]
        [ValidateId]
        public async Task<IActionResult> GetById(int id)
        {
            BannerInfoDTO dto = await _service.GetByIdAsync(id);

            return new SuccessResponseHelper<BannerInfoDTO>()
                .GetSuccessResponse((int)HttpStatusCode.OK, dto);
        }

        [HttpPut("update-order")]
        public async Task<IActionResult> UpdateSortOrder([FromBody] BannerSortOrderDTO dto)
        {
            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.UpdateSortOrder(dto, user.Id);

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)(HttpStatusCode.OK), SuccessMessage.UPDATE_BANNER_SORT_ORDER);
        }
    }
}
