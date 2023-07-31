using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;

namespace DataAccessLayer.Repositories.Implementations;

public class TimesheetRepo : BaseRepo<Timesheet>, ITimesheetRepo
{
    private readonly ApplicationDbContext _context;
    public TimesheetRepo(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
