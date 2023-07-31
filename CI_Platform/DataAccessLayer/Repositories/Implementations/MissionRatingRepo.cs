using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;

namespace DataAccessLayer.Repositories.Implementations
{
    public class MissionRatingRepo : BaseRepo<MissionRating>, IMissionRatingRepo
    {
        private readonly ApplicationDbContext _context;
        public MissionRatingRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
