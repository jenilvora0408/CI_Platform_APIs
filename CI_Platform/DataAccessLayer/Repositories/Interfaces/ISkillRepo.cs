using Entities.DataModels;

namespace DataAccessLayer.Repositories.Interfaces;

public interface ISkillRepo : IBaseRepo<Skill>
{
    Task<bool> IsSkillActive(List<int> skillIds);
}
