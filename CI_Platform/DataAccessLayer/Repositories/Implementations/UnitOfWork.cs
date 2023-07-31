using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IAdminRepo _adminRepo;
        private ICountryRepo _countryRepo;
        private IUserRepo _userRepo;
        private ICityRepo _cityRepo;
        private ISkillRepo _skillRepo;
        private IMissionRepo _missionRepo;
        private IVolunteerRepo _volunteerRepo;
        private IMissionThemeRepo _missionThemeRepo;
        private IBannerRepo _bannerRepo;
        private ICMSPageRepo _cmspageRepo;
        private IVolunteerMissionRepo _volunteerMissionRepo;
        private IMissionRatingRepo _missionRatingRepo;
        private IFavouriteMissionRepo _favouriteMissionRepo;
        private IStoryRepo _storyRepo;
        private IMissionCommentRepo _missionCommentRepo;
        private IMissionApplicationRepo _missionApplicationRepo;
        private ITimesheetRepo _timesheetRepo;


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IBaseRepo<T> GetRepository<T>() where T : class
        {
            return new BaseRepo<T>(_context);
        }
        public void Save()
            => _context.SaveChanges();

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();

        public void Rollback()
            => _context.Dispose();

        public async Task RollbackAsync()
            => await _context.DisposeAsync();

        public IAdminRepo AdminRepo
        {
            get
            {
                return _adminRepo ??= new AdminRepo(_context);
            }
        }

        public ICountryRepo CountryRepo
        {
            get
            {
                return _countryRepo ??= new CountryRepo(_context);
            }
        }

        public IUserRepo UserRepo
        {
            get
            {
                return _userRepo ??= new UserRepo(_context);
            }
        }

        public ICityRepo CityRepo
        {
            get
            {
                return _cityRepo ??= new CityRepo(_context);
            }
        }

        public ISkillRepo SkillRepo
        {
            get
            {
                return _skillRepo ??= new SkillRepo(_context);
            }
        }

        public IMissionRepo MissionRepo
        {
            get
            {
                return _missionRepo ??= new MissionRepo(_context);
            }
        }

        public IVolunteerRepo VolunteerRepo
        {
            get
            {
                return _volunteerRepo ??= new VolunteerRepo(_context);
            }
        }

        public IMissionThemeRepo MissionThemeRepo
        {
            get
            {
                return _missionThemeRepo ??= new MissionThemeRepo(_context);
            }
        }

        public IBannerRepo BannerRepo
        {
            get
            {
                return _bannerRepo ??= new BannerRepo(_context);
            }
        }
        public ICMSPageRepo CMSPageRepo
        {
            get
            {
                return _cmspageRepo ??= new CMSPageRepo(_context);
            }
        }

        public IVolunteerMissionRepo VolunteerMissionRepo
        {
            get
            {
                return _volunteerMissionRepo ??= new VolunteerMissionRepo(_context);
            }
        }

        public IMissionRatingRepo MissionRatingRepo
        {
            get
            {
                return _missionRatingRepo ??= new MissionRatingRepo(_context);
            }
        }

        public IFavouriteMissionRepo FavouriteMissionRepo
        {
            get
            {
                return _favouriteMissionRepo ??= new FavouriteMissionRepo(_context);
            }
        }

        public IStoryRepo StoryRepo
        {
            get
            {
                return _storyRepo ??= new StoryRepo(_context);
            }
        }


        public IMissionCommentRepo MissionCommentRepo
        {
            get
            {
                return _missionCommentRepo ??= new MissionCommentRepo(_context);
            }
        }

        public IMissionApplicationRepo MissionApplicationRepo
        {
            get
            {
                return _missionApplicationRepo ??= new MissionApplicationRepo(_context);
            }
        }
        public ITimesheetRepo TimesheetRepo
        {
            get
            {
                return _timesheetRepo ??= new TimesheetRepo(_context);
            }
        }
    }
}
