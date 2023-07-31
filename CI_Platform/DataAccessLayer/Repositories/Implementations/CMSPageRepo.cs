using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;

namespace DataAccessLayer.Repositories.Implementations
{
    public class CMSPageRepo : BaseRepo<CMSPage>, ICMSPageRepo
    {
        private readonly ApplicationDbContext _context;
        public CMSPageRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
