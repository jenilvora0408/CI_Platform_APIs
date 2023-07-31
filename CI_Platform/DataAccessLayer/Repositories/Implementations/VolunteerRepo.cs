using Common.Enums;
using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementations;

public class VolunteerRepo : BaseRepo<Volunteer>, IVolunteerRepo
{
    private readonly ApplicationDbContext _context;
    public VolunteerRepo(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
