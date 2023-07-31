using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementations
{
    public class CityRepo : BaseRepo<City>, ICityRepo
    {
        private readonly ApplicationDbContext _context;

        public CityRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
