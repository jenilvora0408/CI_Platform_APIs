using AutoMapper;
using Common.Utils.Models;
using Common.Constants;
using Entities.DataModels;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Common.Enums;
using System.Linq.Expressions;

namespace Services.Profiles
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();

            HttpRequest request = httpContextAccessor.HttpContext!.Request;
            string hostAddress = $"{request.Scheme}://{request.Host}";

            CreateMap<Country, CountryDTO>().ReverseMap();

            CreateMap<CountryListRequestDTO, PageListRequestEntity<Country>>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex >= 1 ? src.PageIndex : 1))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize >= 1 ? src.PageSize : SystemConstant.DEFAULT_PAGE_SIZE))
                .ForMember(dest => dest.SortColumn, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.SortColumn) ? SystemConstant.DEFAULT_SORTCOLUMN : src.SortColumn))
                .ReverseMap();


            CreateMap<AdminListRequestDTO, PageListRequestEntity<Admin>>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex >= 1 ? src.PageIndex : 1))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize >= 1 ? src.PageSize : SystemConstant.DEFAULT_PAGE_SIZE))
                .ForMember(dest => dest.SortColumn, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.SortColumn) ? SystemConstant.DEFAULT_SORTCOLUMN : src.SortColumn))
                .ReverseMap();

            CreateMap<Admin, AdminInfoDTO>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => $"{hostAddress}{src.User.Avatar}"))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.User.Status))
            .ReverseMap();


            CreateMap<Admin, AdminDTO>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.User.Status))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ReverseMap()
               .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.Email.ToLower()));

            CreateMap<User, SessionUserModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.UserType))
                .ReverseMap();


            CreateMap<City, CityDTO>().ReverseMap();

            CreateMap<City, CityInfoDTO>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => new DropdownDTO() { Value = (int)src.CountryId, Text = src.Country.Name }))
                .ReverseMap();

            CreateMap<CityListRequestDTO, PageListRequestEntity<City>>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex >= 1 ? src.PageIndex : 1))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize >= 1 ? src.PageSize : SystemConstant.DEFAULT_PAGE_SIZE))
                .ForMember(dest => dest.SortColumn, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.SortColumn) ? SystemConstant.DEFAULT_SORTCOLUMN : src.SortColumn))
                .ReverseMap();


            CreateMap<Skill, SkillDTO>().ReverseMap();

            CreateMap<SkillListRequestDTO, PageListRequestEntity<Skill>>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex >= 1 ? src.PageIndex : 1))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize >= 1 ? src.PageSize : SystemConstant.DEFAULT_PAGE_SIZE));

            CreateMap<Volunteer, VolunteerDTO>()
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.User.Status))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email.ToString().ToLower()))
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.Password, opt => opt.Ignore())
               .ReverseMap()
               .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.Email.ToLower()));

            CreateMap<Volunteer, VolunteerInfoDTO>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => $"{hostAddress}{src.User.Avatar}"))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.User.Status))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.City.Id))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.City.Country.Id))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.City.Country.Name))
            .ReverseMap();

            CreateMap<Volunteer, VolunteerProfileInfoDTO>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => $"{hostAddress}{src.User.Avatar}"))
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.City.CountryId))
            .ForMember(dest => dest.VolunteerSkills, opt => opt.MapFrom(src => src.Skills
                                                        .Select(s => s.SkillId)))
            .ForMember(dest => dest.SkillsList, opt => opt.MapFrom(src => src.Skills
                                                        .Select(skill => new DropdownDTO()
                                                        {
                                                            Value = skill.SkillId,
                                                            Text = skill.Skill.Name
                                                        })));

            CreateMap<VolunteerProfileFormDTO, Volunteer>()
            .ForPath(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForPath(dest => dest.User.LastName, opt => opt.MapFrom(src => src.LastName));


            CreateMap<VolunteerListRequestDTO, PageListRequestEntity<Volunteer>>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex >= 1 ? src.PageIndex : 1))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize >= 1 ? src.PageSize : SystemConstant.DEFAULT_PAGE_SIZE))
                .ForMember(dest => dest.SortColumn, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.SortColumn) ? SystemConstant.DEFAULT_SORTCOLUMN : src.SortColumn))
                .ReverseMap();

            CreateMap<MissionTheme, MissionThemeDTO>().ReverseMap();

            CreateMap<MissionThemeListRequestDTO, PageListRequestEntity<MissionTheme>>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex >= 1 ? src.PageIndex : 1))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize >= 1 ? src.PageSize : SystemConstant.DEFAULT_PAGE_SIZE))
                .ForMember(dest => dest.SortColumn, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.SortColumn) ? SystemConstant.DEFAULT_SORTCOLUMN : src.SortColumn))
                .ReverseMap();


            CreateMap<Mission, MissionFormDTO>().ReverseMap();

            CreateMap<Mission, MissionDTO>()
                .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => $"{hostAddress}{src.ThumbnailUrl}"))
                .ForPath(dest => dest.City.Value, opt => opt.MapFrom(src => src.CityId))
                .ForPath(dest => dest.City.Text, opt => opt.MapFrom(src => src.City.Name))
                .ForPath(dest => dest.Country.Value, opt => opt.MapFrom(src => src.City.CountryId))
                .ForPath(dest => dest.Country.Text, opt => opt.MapFrom(src => src.City.Country.Name))
                .ForPath(dest => dest.MissionTheme.Value, opt => opt.MapFrom(src => src.MissionThemeId))
                .ForPath(dest => dest.MissionTheme.Text, opt => opt.MapFrom(src => src.MissionTheme.Title))
                .ForMember(dest => dest.MissionSkills, opt => opt.MapFrom(src => src.Skills
                    .Select(x => new DropdownDTO()
                    {
                        Value = x.SkillId,
                        Text = x.Skill.Name
                    })))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Medias
                    .Where(media => media.Type.Contains("image"))
                    .Select(media => new MediaDTO
                    {
                        Id = media.Id,
                        Url = $"{hostAddress}{media.Path}"
                    })
                    .ToList()))
                .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.Medias
                    .Where(media => !media.Type.Contains("image"))
                    .Select(media => new MediaDTO
                    {
                        Id = media.Id,
                        Url = $"{hostAddress}{media.Path}"
                    })
                    .ToList()))
                .ReverseMap();


            CreateMap<Country, DropdownDTO>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<City, DropdownDTO>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Skill, DropdownDTO>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<MissionTheme, DropdownDTO>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Volunteer, DropdownDTO>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Mission, DropdownDTO>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();


            CreateMap<Mission, MissionInfoDTO>()
            .ForMember(dest => dest.MissionType, opt => opt.MapFrom(src => src.MissionType))
            .ForMember(dest => dest.MissionTheme, opt => opt.MapFrom(src => src.MissionTheme.Title))
            .ReverseMap();

            CreateMap<MissionListRequestDTO, PageListRequestEntity<Mission>>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex >= 1 ? src.PageIndex : 1))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize >= 1 ? src.PageSize : SystemConstant.DEFAULT_PAGE_SIZE))
                .ForMember(dest => dest.SortColumn, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.SortColumn) ? SystemConstant.DEFAULT_SORTCOLUMN : src.SortColumn))
                .ReverseMap();

            CreateMap<VolunteerMissionListRequestDTO, PageListRequestEntity<Mission>>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex >= 1 ? src.PageIndex : 1))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize >= 1 ? src.PageSize : SystemConstant.DEFAULT_PAGE_SIZE))
                .ForMember(dest => dest.SortColumn, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.SortColumn) ? SystemConstant.DEFAULT_SORTCOLUMN : src.SortColumn))
                .ReverseMap();

            CreateMap<CMSPage, CMSPageDTO>().ReverseMap();

            CreateMap<CMSPageListRequestDTO, PageListRequestEntity<CMSPage>>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex >= 1 ? src.PageIndex : 1))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize >= 1 ? src.PageSize : SystemConstant.DEFAULT_PAGE_SIZE))
                .ForMember(dest => dest.SortColumn, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.SortColumn) ? SystemConstant.DEFAULT_SORTCOLUMN : src.SortColumn))
                .ReverseMap();

            #region Banner
            CreateMap<Banner, BannerInfoDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{hostAddress}{src.Image}"))
                .ReverseMap();

            CreateMap<Banner, LoginBannerDTO>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => $"{hostAddress}{src.Image}"))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();

            CreateMap<BannerListRequestDTO, PageListRequestEntity<Banner>>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex >= 1 ? src.PageIndex : 1))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize >= 1 ? src.PageSize : SystemConstant.DEFAULT_PAGE_SIZE))
                .ForMember(dest => dest.SortColumn, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.SortColumn) ? SystemConstant.NAMEOF_SORTCOLUMN_SORTORDER : src.SortColumn))
                .ForMember(dest => dest.SortOrder, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.SortOrder) ? SystemConstant.ASCENDING : src.SortOrder))
                .ReverseMap();
            #endregion

            #region MissionApplication
            CreateMap<VolunteerMissionApplicationDTO, MissionApplication>()
                .ForMember(dest => dest.MissionId, opt => opt.MapFrom(src => src.MissionId))
                .ForMember(dest => dest.AppliedOn, opt => opt.MapFrom(src => DateTimeOffset.Now))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => MissionApplicationStatus.PENDING));

            CreateMap<MissionApplicationListRequestDTO, PageListRequestEntity<MissionApplication>>()
               .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex >= 1 ? src.PageIndex : 1))
               .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize >= 1 ? src.PageSize : SystemConstant.DEFAULT_PAGE_SIZE))
               .ForMember(dest => dest.SortColumn, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.SortColumn) ? SystemConstant.DEFAULT_SORTCOLUMN : src.SortColumn))
               .ReverseMap();

            CreateMap<MissionApplication, MissionApplicationInfoDTO>()
                .ForMember(dest => dest.MissionTitle, opt => opt.MapFrom(src => src.Mission.Title))
                .ForMember(dest => dest.VolunteerName, opt => opt.MapFrom(src => src.Volunteer.User.FirstName + " " + src.Volunteer.User.LastName));

            CreateMap<MissionApplication, DropdownDTO>()
               .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Mission.Title))
               .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Mission.Id))
               .ReverseMap();

            CreateMap<MissionApplication, MissionApplicationPatchDTO>().ReverseMap();

            #endregion
            CreateMap<MissionSkills, Skill>().ReverseMap();

            CreateMap<Mission, VolunteerMissionDTO>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.ThemeTitle, opt => opt.MapFrom(src => src.MissionTheme.Title))
                .ForPath(dest => dest.SkillList, opt => opt.MapFrom(src => src.Skills.Select(x => new DropdownDTO() { Value = x.SkillId })))
                .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => $"{hostAddress}{src.ThumbnailUrl}"))
                .ForMember(dest => dest.AvgRating, opt =>
                {
                    opt.PreCondition(src => src.MissionRating != null && src.MissionRating.Any());
                    opt.MapFrom(src => src.MissionRating.Average(x => x.Rating));
                })
                .ForMember(dest => dest.RemainingSeat, opt => opt.MapFrom(src =>
                            src.TotalSeat.HasValue ? src.TotalSeat - src.MissionApplications.Count() : null))
                .ForMember(dest => dest.IsFavourite, opt => opt.MapFrom(src => src.FavouriteMissions.Any()))
                .ReverseMap();

            CreateMap<MissionRating, MissionRatingInfoDTO>().ReverseMap();

            #region Timesheet
            CreateMap<TimesheetListRequestDTO, PageListRequestEntity<Timesheet>>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex >= 1 ? src.PageIndex : 1))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize >= 1 ? src.PageSize : SystemConstant.DEFAULT_PAGE_SIZE))
                .ForMember(dest => dest.SortColumn, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.SortColumn) ? SystemConstant.DEFAULT_SORTCOLUMN : src.SortColumn))
                .ReverseMap();

            CreateMap<TimeBaseTimesheetFormDTO, Timesheet>()
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => TimeSpan.FromHours(src.Hours) + TimeSpan.FromMinutes(src.Minute)))
                .ReverseMap();
            CreateMap<GoalBaseTimesheetFormDTO, Timesheet>()
                .ForMember(dest => dest.GoalAchieved, opt => opt.MapFrom(src => src.Contribution))
                .ReverseMap();

            CreateMap<Timesheet, TimeBaseTimesheetDTO>()
                .ForMember(dest => dest.Hours, opt => opt.MapFrom(src => src.Time.Value.Hours))
                .ForMember(dest => dest.Minute, opt => opt.MapFrom(src => src.Time.Value.Minutes))
                .ForMember(dest => dest.Mission, opt => opt.MapFrom(src => new DropdownDTO { Text = src.Mission.Title, Value = (int)src.MissionId }))
                .ReverseMap();
            CreateMap<Timesheet, GoalBaseTimesheetDTO>()
                .ForMember(dest => dest.Mission, opt => opt.MapFrom(src => new DropdownDTO { Text = src.Mission.Title, Value = (int)src.MissionId }))
                .ForMember(dest => dest.Contribution, opt => opt.MapFrom(src => src.GoalAchieved))
                .ReverseMap();

            CreateMap<Timesheet, TimesheetDTO>()
                .ForMember(dest => dest.Contribution, opt => opt.MapFrom(src => src.GoalAchieved))
                .ForMember(dest => dest.Hours, opt => opt.MapFrom(src => src.Time.HasValue ? src.Time.Value.Hours : (int?)null))
                .ForMember(dest => dest.Minutes, opt => opt.MapFrom(src => src.Time.HasValue ? src.Time.Value.Minutes : (int?)null))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Mission.MissionType))
                .ForMember(dest => dest.MissionTitle, opt => opt.MapFrom(src => src.Mission.Title))
                .ForMember(dest => dest.VolunteerName, opt => opt.MapFrom(src => $"{src.Volunteer.User.FirstName} {src.Volunteer.User.LastName}"))
                .ReverseMap();

            CreateMap<Timesheet, TimesheetPatchDTO>().ReverseMap();
            #endregion

            CreateMap<MissionCommentDTO, MissionComment>();

            CreateMap<MissionComment, CommentInfoDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Volunteer.User.FirstName} {src.Volunteer.User.LastName}"))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => $"{hostAddress}{src.Volunteer.User.Avatar}"))
                .ForMember(dest => dest.CommentPostedOn, opt => opt.MapFrom(src => src.CreatedOn.ToString("yyyy-MM-dd").Substring(0, 10)))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text));

            CreateMap<MissionApplication, RecentVolunteerDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Volunteer.User.FirstName))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => $"{hostAddress}{src.Volunteer.User.Avatar}"));

            #region Story

            CreateMap<VolunteerStoryDTO, Story>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status ?? StoryStatus.PENDING))
                .ForMember(dest => dest.Medias, opt => opt.Ignore())
                .ReverseMap();

            #endregion

            #region Get_Mission_By_Mission_MissionID

            CreateMap<Mission, VolunteerMissionInfoDTO>()
                .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => $"{hostAddress}{src.ThumbnailUrl}"))
                .ForPath(dest => dest.City.Value, opt => opt.MapFrom(src => src.CityId))
                .ForPath(dest => dest.City.Text, opt => opt.MapFrom(src => src.City.Name))
                .ForPath(dest => dest.Country.Value, opt => opt.MapFrom(src => src.City.CountryId))
                .ForPath(dest => dest.Country.Text, opt => opt.MapFrom(src => src.City.Country.Name))
                .ForPath(dest => dest.MissionTheme.Value, opt => opt.MapFrom(src => src.MissionThemeId))
                .ForPath(dest => dest.MissionTheme.Text, opt => opt.MapFrom(src => src.MissionTheme.Title))
                .ForMember(dest => dest.MissionSkills, opt => opt.MapFrom(src => src.Skills
                    .Select(x => new DropdownDTO()
                    {
                        Value = x.SkillId,
                        Text = x.Skill.Name
                    })))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Medias
                    .Where(media => media.Type.Contains("image"))
                    .Select(media => new MediaDTO
                    {
                        Id = media.Id,
                        Url = $"{hostAddress}{media.Path}"
                    })
                    .ToList()))
                .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.Medias
                    .Where(media => !media.Type.Contains("image"))
                    .Select(media => new MediaDTO
                    {
                        Id = media.Id,
                        Url = $"{hostAddress}{media.Path}"
                    })
                    .ToList()))
                    .ForMember(dest => dest.AvgRating, opt =>
                {
                    opt.PreCondition(src => src.MissionRating != null && src.MissionRating.Any());
                    opt.MapFrom(src => src.MissionRating.Average(x => x.Rating));
                })
                .ForMember(dest => dest.IsFavourite, opt => opt.MapFrom(src => src.FavouriteMissions.Any()))
                .ForMember(dest => dest.HasApplied, opt => opt.MapFrom(src => src.MissionApplications.Any()))
                .ReverseMap();

            #endregion

            #region Related_Missions

            CreateMap<Mission, RelatedMissionDTO>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.City.Country.Name))
                .ForMember(dest => dest.ThemeTitle, opt => opt.MapFrom(src => src.MissionTheme.Title))
                .ForMember(dest => dest.AvgRating, opt =>
                {
                    opt.PreCondition(src => src.MissionRating != null && src.MissionRating.Any());
                    opt.MapFrom(src => src.MissionRating.Average(x => x.Rating));
                })
                .ForMember(dest => dest.IsFavourite, opt => opt.MapFrom(src => src.FavouriteMissions.Any()))
                .ForMember(dest => dest.HasApplied, opt => opt.MapFrom(src => src.MissionApplications.Any()));

            #endregion
        }
    }
}