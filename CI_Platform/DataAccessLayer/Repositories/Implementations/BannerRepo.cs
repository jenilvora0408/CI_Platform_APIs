using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories.Implementations;

public class BannerRepo : BaseRepo<Banner>, IBannerRepo
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Banner> _dbSet;
    public BannerRepo(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = _context.Set<Banner>();
    }

    public async Task<int> GetMaxSortOrderAsync(Expression<Func<Banner, bool>>? whereExpression = null)
    {
        IQueryable<Banner> query = _dbSet.AsQueryable();

        if (whereExpression != null)
        {
            query = query.Where(whereExpression);
        }

        int maxSortOrder = await query.MaxAsync(b => b.SortOrder);
        return maxSortOrder;
    }
}
