using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;

namespace DataAccessLayer.Repositories.Implementations
{
    public class AdminRepo : BaseRepo<Admin>, IAdminRepo
    {
        private readonly ApplicationDbContext _context;
        public AdminRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
