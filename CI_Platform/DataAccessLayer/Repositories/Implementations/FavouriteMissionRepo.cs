using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;

namespace DataAccessLayer.Repositories.Implementations
{
    public class FavouriteMissionRepo : BaseRepo<FavouriteMission>, IFavouriteMissionRepo
    {
        private readonly ApplicationDbContext _context;
        public FavouriteMissionRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
