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

namespace API.Areas.Volunteer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [VolunteerPolicy]
    public class MissionsController : ControllerBase
    {
        private readonly IVolunteerMissionService _service;
        private readonly IMissionCommentService _missionCommentService;
        private readonly IMissionApplicationService _missionApplicationService;

        public MissionsController(IVolunteerMissionService service, IMissionCommentService missionCommentService, IMissionApplicationService missionApplicationService)
        {
            _service = service;
            _missionCommentService = missionCommentService;
            _missionApplicationService = missionApplicationService;
        }

        [HttpPost("list")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetList(VolunteerMissionListRequestDTO dto)
        {
            dto ??= new VolunteerMissionListRequestDTO();

            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

            long sessionUserId = HttpContext.IsSessionUserExist() ? HttpContext.GetSessionUser().Id : 0;

            PageListResponseDTO<VolunteerMissionDTO> missionsPage = await _service.GetAllAsync(dto, sessionUserId);

            return new SuccessResponseHelper<PageListResponseDTO<VolunteerMissionDTO>>()
            .GetSuccessResponse((int)HttpStatusCode.OK, missionsPage);
        }

        [HttpPost("ratings")]
        public async Task<IActionResult> PostRating(MissionRatingDTO dto)
        {
            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

            SessionUserModel user = HttpContext.GetSessionUser();

            await _service.UpsertRatingsAsync(dto, user.Id);

            return new SuccessResponseHelper<object>()
            .GetSuccessResponse((int)HttpStatusCode.Created, new List<string> { SuccessMessage.POST_RATING });
        }

        [HttpPost]
        [Route("favourites/{id}")]
        [ValidateId]
        public async Task<IActionResult> SetFavorite([FromRoute] long id)
        {
            SessionUserModel user = HttpContext.GetSessionUser();

            bool isLiked = await _service.SetFavoriteStatusAsync(id, user.Id);

            string successMessage = isLiked ? SuccessMessage.LIKED_MISSION : SuccessMessage.UNLIKED_MISSION;

            return new SuccessResponseHelper<object>()
                .GetSuccessResponse((int)HttpStatusCode.Created, new List<string> { successMessage });
        }

        [HttpPost]
        [Route("comments")]
        public async Task<IActionResult> PostComments(MissionCommentDTO dto)
        {
            SessionUserModel user = HttpContext.GetSessionUser();

            await _missionCommentService.PostComments(dto, user.Id);

            return new SuccessResponseHelper<object>()
            .GetSuccessResponse((int)HttpStatusCode.Created, new List<string> { SuccessMessage.POST_COMMENT });
        }


        [HttpGet]
        [Route("comments/{id}")]
        [ValidateId]
        public async Task<IActionResult> GetCommentsForMission([FromRoute] long id)
        {
            List<CommentInfoDTO> dto = await _missionCommentService.GetCommentsForMissionAsync(id);

            return new SuccessResponseHelper<List<CommentInfoDTO>>()
            .GetSuccessResponse((int)HttpStatusCode.OK, dto);
        }

        [HttpGet]
        [Route("related-missions/{id}")]
        [ValidateId]
        public async Task<IActionResult> GetRelatedMissions(long id)
        {
            SessionUserModel user = HttpContext.GetSessionUser();

            List<RelatedMissionDTO> relatedMissions = await _service.GetRelatedMissionsAsync(id, user.Id);

            return new SuccessResponseHelper<List<RelatedMissionDTO>>()
            .GetSuccessResponse((int)HttpStatusCode.OK, relatedMissions);
        }
        
        [HttpGet]
        [Route("recent-volunteers/{id}/{pageIndex}")]
        [ValidateId]
        public async Task<IActionResult> GetApprovedMissionApplications([FromRoute] long id,[FromRoute] int pageIndex)
        {
            PageListResponseDTO<RecentVolunteerDTO> missionApplications = await _missionApplicationService.GetApprovedMissionApplicationsByMissionId(id, pageIndex);

            return new SuccessResponseHelper<PageListResponseDTO<RecentVolunteerDTO>>()
            .GetSuccessResponse((int)HttpStatusCode.OK, missionApplications);
        }

        [HttpGet]
        [Route("{id}")]
        [ValidateId]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            SessionUserModel user = HttpContext.GetSessionUser();

            VolunteerMissionInfoDTO? mission = await _service.GetById(id, user.Id);

            return new SuccessResponseHelper<VolunteerMissionInfoDTO>()
                .GetSuccessResponse((int)HttpStatusCode.OK, new List<string> { }, mission);
        }
    }
}