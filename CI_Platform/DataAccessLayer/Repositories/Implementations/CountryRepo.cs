using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;

namespace DataAccessLayer.Repositories.Implementations
{
    public class CountryRepo : BaseRepo<Country>, ICountryRepo
    {
        private readonly ApplicationDbContext _context;

        public CountryRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


    }



}
