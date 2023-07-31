using Entities.DataModels;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories.Interfaces;
public interface IMissionApplicationRepo : IBaseRepo<MissionApplication>
{
    Task<int> GetAppliedVolunteerCountForMission(Expression<Func<MissionApplication, bool>> filter);
}
