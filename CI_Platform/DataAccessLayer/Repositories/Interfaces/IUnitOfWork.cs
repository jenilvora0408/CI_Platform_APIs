namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        void Save();
        void Rollback();
        IBaseRepo<T> GetRepository<T>() where T : class;
        Task SaveAsync();
        Task RollbackAsync();

        IAdminRepo AdminRepo { get; }
        ICountryRepo CountryRepo { get; }
        IUserRepo UserRepo { get; }
        ICityRepo CityRepo { get; }
        ISkillRepo SkillRepo { get; }
        IMissionRepo MissionRepo { get; }
        IVolunteerRepo VolunteerRepo { get; }
        IMissionThemeRepo MissionThemeRepo { get; }
        IBannerRepo BannerRepo { get; }
        ICMSPageRepo CMSPageRepo { get; }
        IVolunteerMissionRepo VolunteerMissionRepo { get;}
        IMissionRatingRepo MissionRatingRepo { get;}
        IFavouriteMissionRepo FavouriteMissionRepo { get; }
        IStoryRepo StoryRepo { get; }
        IMissionCommentRepo MissionCommentRepo { get; }
        IMissionApplicationRepo MissionApplicationRepo { get; }
        ITimesheetRepo TimesheetRepo  { get; }
    }
}
