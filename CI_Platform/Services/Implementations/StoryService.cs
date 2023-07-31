using System.Runtime.InteropServices;
using AutoMapper;
using Common.Constants;
using Common.CustomExceptions;
using Common.Enums;
using Common.Utils;
using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Services.Interfaces;

namespace Services.Implementations;
public class StoryService : BaseService<Story>, IStoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHostEnvironment _env;
    private readonly IVolunteerService _volunteerService;
    private readonly IMissionApplicationService _missionApplicationService;

    public StoryService(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment env, IVolunteerService volunteerService, IMissionApplicationService missionApplicationService) : base(unitOfWork.StoryRepo, unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _env = env;
        _volunteerService = volunteerService;
        _missionApplicationService = missionApplicationService;
    }

    private async Task<List<StoryMedia>> AddMedia(IFormFileCollection? medias, List<string>? urls, long sessionUserId)
    {
        List<StoryMedia> storyMedias = new();
        if (medias != null)
        {
            foreach (IFormFile media in medias)
            {
                KeyValuePair<string, string> fileData = await new FileHepler(_env).UploadFileToDestination(media, SystemConstant.DIR_STORY_MEDIA, sessionUserId);
                StoryMedia storyMedia = new()
                {
                    Path = fileData.Key,
                    Type = fileData.Value,
                    Status = MediaStatus.ACTIVE
                };
                storyMedias.Add(storyMedia);
            }
        }
        if (urls != null)
        {
            foreach (string url in urls)
            {
                StoryMedia storyMedia = new()
                {
                    Path = url,
                    Type = SystemConstant.YOUTUBE_LINK_TYPE,
                    Status = MediaStatus.ACTIVE
                };
                storyMedias.Add(storyMedia);
            }
        }
        return storyMedias;
    }

    public async Task<string> UpsertAsync(VolunteerStoryDTO dto, long sessionUserId)
    {
        Volunteer volunteer = await _volunteerService.GetVolunteerByUserId(sessionUserId);
        if (volunteer == null || volunteer.Id == 0)
            throw new EntityNullException(ExceptionMessage.VOLUNTEER_NOT_FOUND);

        long volunteerId = volunteer.Id;

        await CheckExistency(dto.MissionId, volunteerId);

        string response = dto.Id == 0 ? await Create(dto, volunteerId, sessionUserId) : await Update(dto, volunteerId, sessionUserId);

        return response;
    }

    private Task<string> Update(VolunteerStoryDTO dto, long volunteerId, long sessionUserId)
    {
        throw new System.NotImplementedException();
    }

    private async Task<string> Create(VolunteerStoryDTO dto, long volunteerId, long sessionUserId)
    {
        Story entity = _mapper.Map<Story>(dto);
        entity.VolunteerId = volunteerId;
        entity.Medias = await AddMedia(dto.Medias, dto.MediaUrls, sessionUserId);
        await AddAsync(entity);
        return entity.Status == StoryStatus.DRAFT ? SuccessMessage.ADD_STORY_DRAFT : SuccessMessage.ADD_STORY_PENDING;
    }

    private async Task CheckExistency(long missionId, long volunteerId)
    {
        bool isApplied = await _missionApplicationService.CheckApprovalStatus(missionId, volunteerId);
        if (!isApplied) throw new ValidationException(ExceptionMessage.MISSION_APPLICATION_NOT_APPLIED);
    }
}