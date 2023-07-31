using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories.Implementations;
public class MissionApplicationRepo : BaseRepo<MissionApplication>, IMissionApplicationRepo
{
    private readonly ApplicationDbContext _context;
    private DbSet<MissionApplication> _dbSet;

    public MissionApplicationRepo(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = _context.Set<MissionApplication>();
    }

    public async Task<int> GetAppliedVolunteerCountForMission(Expression<Func<MissionApplication, bool>> filter)
    {
        int numberOfVolunteers = await _dbSet.CountAsync(filter);
        return numberOfVolunteers;
    }
}