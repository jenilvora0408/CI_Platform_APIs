using Common.Enums;
using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementations;

public class SkillRepo : BaseRepo<Skill>, ISkillRepo
{
    private readonly ApplicationDbContext _context;
    public SkillRepo(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> IsSkillActive(List<int> skillIds)
    {
        return await _context.Skills
            .CountAsync(skill => skill.Status == SkillStatus.ACTIVE && skillIds.Contains(skill.Id)) != skillIds.Count();
    }
}
