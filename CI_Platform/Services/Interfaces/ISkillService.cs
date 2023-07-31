using Entities.DataModels;
using Entities.DTOs;

namespace Services.Interfaces;

public interface ISkillService : IBaseService<Skill>
{
    Task UpsertAsync(SkillDTO skillDTO, long sessionUserId);

    Task<PageListResponseDTO<SkillDTO>> GetAllSkills(SkillListRequestDTO skillListRequest);

    Task<IEnumerable<DropdownDTO>> GetSkills();

    Task<SkillDTO> GetById(int id);

    Task RemoveAsync(int id, long sessionUserId);

    //Task<bool> IsDuplicate(string skillName);
}
