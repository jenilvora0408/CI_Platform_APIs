using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;

namespace DataAccessLayer.Repositories.Implementations;
public class StoryRepo : BaseRepo<Story>, IStoryRepo
{
    private readonly ApplicationDbContext _context;

    public StoryRepo(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
