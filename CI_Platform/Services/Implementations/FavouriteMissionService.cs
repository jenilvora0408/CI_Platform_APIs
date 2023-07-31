using AutoMapper;
using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Microsoft.Extensions.Hosting;
using Services.Interfaces;

namespace Services.Implementations
{
    public class FavouriteMissionService : BaseService<FavouriteMission>, IFavouriteMissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHostEnvironment _env;

        public FavouriteMissionService(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment env) : base(unitOfWork.FavouriteMissionRepo, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }
    
    }
}
