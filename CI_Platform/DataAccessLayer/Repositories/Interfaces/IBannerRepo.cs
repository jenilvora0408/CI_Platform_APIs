using Entities.DataModels;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IBannerRepo : IBaseRepo<Banner>
{
    Task<int> GetMaxSortOrderAsync(Expression<Func<Banner, bool>>? whereExpression = null);
}
