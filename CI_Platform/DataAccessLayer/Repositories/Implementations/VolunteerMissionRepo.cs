using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementations
{
    public class VolunteerMissionRepo : BaseRepo<Mission>, IVolunteerMissionRepo
    {
        private readonly ApplicationDbContext _context;
        public VolunteerMissionRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Mission>> GetRelatedMissionsAsync(string theme, string? cityName, string? countryName)
        {
            List<Mission>? relatedMissions = await _context.Missions
                .Include(m => m.City)
                .ThenInclude(c => c.Country)
                .Include(m => m.MissionTheme)
                .Where(m =>
                    m.MissionTheme.Title == theme ||
                    m.City.Name == cityName ||
                    m.City.Country.Name == countryName
                )
                .ToListAsync();

            return relatedMissions;
        }
    }
}
