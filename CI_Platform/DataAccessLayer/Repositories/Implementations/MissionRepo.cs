using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;

namespace DataAccessLayer.Repositories.Implementations;

public class MissionRepo : BaseRepo<Mission>, IMissionRepo
{
    private readonly ApplicationDbContext _context;
    public MissionRepo(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
