using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;

namespace DataAccessLayer.Repositories.Implementations
{
    public class MissionThemeRepo : BaseRepo<MissionTheme>, IMissionThemeRepo
    {
        private readonly ApplicationDbContext _context;
        public MissionThemeRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
