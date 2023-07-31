using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;

namespace DataAccessLayer.Repositories.Implementations;
public class UserRepo : BaseRepo<User>, IUserRepo
{
    private readonly ApplicationDbContext _context;

	public UserRepo(ApplicationDbContext context) : base(context)
	{
		_context= context;
	}

}