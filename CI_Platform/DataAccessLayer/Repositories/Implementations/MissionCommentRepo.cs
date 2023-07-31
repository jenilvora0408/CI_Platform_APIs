using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;

namespace DataAccessLayer.Repositories.Implementations
{
    public class MissionCommentRepo : BaseRepo<MissionComment>, IMissionCommentRepo
    {
        private readonly ApplicationDbContext _context;
        public MissionCommentRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
