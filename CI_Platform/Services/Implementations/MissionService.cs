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
using System.Linq.Expressions;

namespace Services.Implementations;

public class MissionService : BaseService<Mission>, IMissionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHostEnvironment _env;

    public MissionService(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment env) : base(unitOfWork.MissionRepo, unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _env = env;
    }

    public async Task UpsertAsync(MissionFormDTO dto, long sessionUserId)
    {
        Mission mission = dto.Id != 0 ? await GetAsync(dto.Id) : new Mission();

        await CheckExistency(dto.CityId, dto.MissionThemeId, dto.MissionSkills);

        mission = _mapper.Map(dto, mission);

        // for skills add or update
        if (dto.Id == 0)
        {
            mission.Skills.AddRange(AddSkills(dto.MissionSkills));
        }
        else
        {
            List<int> skillForAdd = dto.MissionSkills.Except(mission.Skills.Select(x => x.SkillId)).ToList();

            foreach (var skill in mission.Skills)
            {
                skill.Status = dto.MissionSkills.Any(id => id == skill.SkillId) ? SkillStatus.ACTIVE : SkillStatus.DELETED;
            }

            //add skills
            mission.Skills.AddRange(AddSkills(skillForAdd));

            if (dto.DeletedMedia != null)
            {
                //delete media
                foreach (var media in mission.Medias)
                {
                    if (dto.DeletedMedia.Contains(media.Id))
                        media.Status = MediaStatus.DELETED;
                }
            }
        }

        // for add thumbnail
        if (dto.ThumbnailUrl != null)
        {
            KeyValuePair<string, string> fileData = await new FileHepler(_env).UploadFileToDestination(dto.ThumbnailUrl, SystemConstant.DIR_MISSION_IMAGE, sessionUserId);
            mission.ThumbnailUrl = fileData.Key;
        }

        if (dto.Images != null || dto.Documents != null)
        {
            mission.Medias.AddRange(await AddMediaAsync(dto.Images, dto.Documents, sessionUserId));
        }

        mission.ModifiedBy = sessionUserId;

        if (dto.Id == 0)
        {
            mission.CreatedBy = sessionUserId;
            await AddAsync(mission);
        }
        else
            await UpdateAsync(mission);
    }

    public async Task<PageListResponseDTO<MissionInfoDTO>> GetAllAsync(MissionListRequestDTO missionListRequest)
    {
        PageListRequestEntity<Mission> pageListRequestEntity = _mapper.Map<PageListRequestEntity<Mission>>(missionListRequest);

        pageListRequestEntity.IncludeExpressions = new Expression<Func<Mission, object>>[] { x => x.MissionTheme };

        pageListRequestEntity.Selects = mission => new Mission()
        {
            Id = mission.Id,
            Title = mission.Title,
            MissionTheme = new MissionTheme()
            {
                Title = mission.MissionTheme.Title
            },
            StartDate = mission.StartDate,
            MissionType = mission.MissionType,
            Status = mission.Status,
        };

        if (!string.IsNullOrEmpty(missionListRequest.SearchQuery))
        {
            string searchQuery = missionListRequest.SearchQuery.Trim().ToLower();

            pageListRequestEntity.Predicate =
                mission => mission.Title.Trim().ToLower().Contains(searchQuery)
                      || mission.MissionTheme.Title.Trim().ToLower().Contains(searchQuery);
        }

        PageListResponseDTO<Mission> pageListResponse = await _unitOfWork.MissionRepo.GetAllAsync(pageListRequestEntity);

        List<MissionInfoDTO> missionInfoDTOs = _mapper.Map<List<MissionInfoDTO>>(pageListResponse.Records);

        return new PageListResponseDTO<MissionInfoDTO>(pageListResponse.PageIndex, pageListResponse.PageSize, pageListResponse.TotalRecords, missionInfoDTOs);
    }


    public async Task<MissionDTO> GetById(long id)
    {
        Mission? mission = await _unitOfWork.MissionRepo.GetAsync(mission => mission.Id == id, new Expression<Func<Mission, object>>[] { x => x.City, x => x.MissionTheme, x => x.Skills.Where(x => x.Status == SkillStatus.ACTIVE), x => x.Medias }, new string[] { "City.Country", "Skills.Skill" }) ?? throw new EntityNullException(ExceptionMessage.MISSION_NOT_FOUND);

        MissionDTO? missionDTO = _mapper.Map<MissionDTO>(mission);

        return missionDTO;
    }


    public async Task RemoveAsync(long id, long sessionUserId)
    {
        Mission? mission = await GetAsync(id);

        mission.Status = MissionStatus.DELETED;
        mission.ModifiedBy = sessionUserId;

        foreach (var skill in mission.Skills)
        {
            skill.Status = SkillStatus.DELETED;
        }

        foreach (var media in mission.Medias)
        {
            media.Status = MediaStatus.DELETED;
        }

        await UpdateAsync(mission);
    }


    private async Task CheckExistency(long cityId, long missionThemeId, List<int> missionSkillIds)
    {
        if (!await _unitOfWork.CityRepo.IsEntityExist(city => city.Id == cityId && city.Status == CityStatus.ACTIVE))
            throw new EntityNullException(ExceptionMessage.CITY_NOT_ACTIVE);

        if (!await _unitOfWork.MissionThemeRepo.IsEntityExist(theme => theme.Id == missionThemeId && theme.Status == MissionThemeStatus.ACTIVE))
            throw new EntityNullException(ExceptionMessage.MISSION_THEME_NOT_ACTIVE);

        if (await _unitOfWork.SkillRepo.IsSkillActive(missionSkillIds))
            throw new EntityNullException(ExceptionMessage.MISSION_SKILLS_NOT_ACTIVE);
    }


    private async Task<Mission> GetAsync(long id)
    {
        Mission? mission = await _unitOfWork.MissionRepo.GetAsync(mission => mission.Id == id, new Expression<Func<Mission, object>>[] { x => x.Skills, x => x.Medias }) ?? throw new EntityNullException(ExceptionMessage.MISSION_NOT_FOUND);

        return mission;
    }

    private async Task<ICollection<MissionMedia>> AddMediaAsync(IFormFileCollection? images, IFormFileCollection? documents, long sessionUserId)
    {
        List<MissionMedia> medias = new List<MissionMedia>();
        if (images != null)
        {
            foreach (var image in images)
            {
                KeyValuePair<string, string> fileData = await new FileHepler(_env).UploadFileToDestination(image, SystemConstant.DIR_MISSION_IMAGE, sessionUserId);
                MissionMedia media = new()
                {
                    Path = fileData.Key,
                    Type = fileData.Value,
                    Name = image.FileName,
                    Status = MediaStatus.ACTIVE
                };
                medias.Add(media);
            }
        }

        if (documents != null)
        {
            foreach (var doc in documents)
            {
                KeyValuePair<string, string> fileData = await new FileHepler(_env).UploadFileToDestination(doc, SystemConstant.DIR_MISSION_DOC, sessionUserId);
                MissionMedia media = new()
                {
                    Path = fileData.Key,
                    Type = fileData.Value,
                    Name = doc.FileName,
                    Status = MediaStatus.ACTIVE
                };
                medias.Add(media);
            }
        }
        return medias;
    }

    private ICollection<MissionSkills> AddSkills(List<int> skillIds)
    {
        return skillIds.Select(skillId => new MissionSkills
        {
            SkillId = skillId,
            Status = SkillStatus.ACTIVE
        }).ToList();
    }

    public async Task<bool> CheckForMission(long missionId)
    {
        return await _unitOfWork.MissionRepo.IsEntityExist(mission => mission.Id == missionId && mission.Status == MissionStatus.ACTIVE &&
            mission.EndDate >= DateTime.Now && (mission.GoalObjectiveAchieved == null || mission.GoalObjective == null ||mission.GoalObjectiveAchieved >= mission.GoalObjective));
    }
}
