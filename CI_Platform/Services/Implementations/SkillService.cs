using AutoMapper;
using Common.Constants;
using Common.CustomExceptions;
using Common.Enums;
using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Entities.DTOs;
using Services.Interfaces;
using System.Net;

namespace Services.Implementations;

public class SkillService : BaseService<Skill>, ISkillService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SkillService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.SkillRepo, unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PageListResponseDTO<SkillDTO>> GetAllSkills(SkillListRequestDTO skillListRequest)
    {
        PageListRequestEntity<Skill> pageListRequestEntity = _mapper.Map<PageListRequestEntity<Skill>>(skillListRequest);

        if (!string.IsNullOrEmpty(skillListRequest.SearchQuery))
        {
            string searchQuery = skillListRequest.SearchQuery.Trim().ToLower();

            pageListRequestEntity.Predicate =
                skill => skill.Name.Trim().ToLower().Contains(searchQuery);
        }

        PageListResponseDTO<Skill> pageListResponse = await _unitOfWork.SkillRepo.GetAllAsync(pageListRequestEntity);

        List<SkillDTO> skillDTOs = _mapper.Map<List<SkillDTO>>(pageListResponse.Records);

        return new PageListResponseDTO<SkillDTO>(pageListResponse.PageIndex, pageListResponse.PageSize, pageListResponse.TotalRecords, skillDTOs);
    }


    public async Task<IEnumerable<DropdownDTO>> GetSkills()
    {
        List<Skill> skills = await _unitOfWork.SkillRepo.GetAllAsync(skill => skill.Status == SkillStatus.ACTIVE);
        if (skills.Count <= 0)
            return Enumerable.Empty<DropdownDTO>();

        List<DropdownDTO> skillDTOs = _mapper.Map<List<DropdownDTO>>(skills);
        return skillDTOs;
    }


    public async Task<SkillDTO> GetById(int id)
    {
        Skill? skill = await GetSkill(id);

        SkillDTO skillDTO = _mapper.Map<SkillDTO>(skill);

        return skillDTO;
    }

    public async Task RemoveAsync(int id, long sessionUserId)
    {
        Skill? skill = await GetSkill(id);

        skill.Status = SkillStatus.DELETED;
        skill.ModifiedBy = sessionUserId;

        await UpdateAsync(skill);
    }

    public async Task UpsertAsync(SkillDTO skillDTO, long sessionUserId)
    {
        Skill? skill = skillDTO.Id != 0 ? await GetSkill(skillDTO.Id) : new Skill();

        bool isExist = await _unitOfWork.SkillRepo.IsEntityExist(skill => skill.Name == skillDTO.Name && skill.Id != skillDTO.Id);

        if (isExist) throw new DataAlreadyExistsException(ExceptionMessage.SKILL_ALREADY_EXIST);

        if (skill == null) throw new EntityNullException(ExceptionMessage.SKILL_NOT_FOUND);

        if (skillDTO.Id == 0)
        {
            skill = _mapper.Map<Skill>(skillDTO);
            skill.CreatedBy = sessionUserId;
            skill.Status = SkillStatus.ACTIVE;
        }
        else
        {
            skill.Name = skillDTO.Name;
            skill.Status = skillDTO.Status;
        }
        skill.ModifiedBy = sessionUserId;

        if (skillDTO.Id == 0) await AddAsync(skill);
        else await UpdateAsync(skill);

    }

    //public async Task<bool> IsDuplicate(string skillName)
    //{
    //    Skill? skill = await _unitOfWork.SkillRepo.GetAsync(skill => skill.Name.Trim().ToLower().Equals(skillName.Trim().ToLower()), null);

    //    return skill != null ? true : false;
    //}

    private async Task<Skill> GetSkill(int id)
    {
        Skill? skill = await _unitOfWork.SkillRepo.GetAsync(skill => skill.Id == id, null);

        if (skill == null) throw new EntityNullException(ExceptionMessage.SKILL_NOT_FOUND);

        return skill;
    }
}
