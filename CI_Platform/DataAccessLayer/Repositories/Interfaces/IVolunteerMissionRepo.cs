using Entities.DataModels;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IVolunteerMissionRepo : IBaseRepo<Mission>
    {
        Task<List<Mission>> GetRelatedMissionsAsync(string theme, string? cityName, string? countryName);
    }
}
