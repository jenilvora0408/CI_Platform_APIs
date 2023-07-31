using AutoMapper;
using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Microsoft.Extensions.Hosting;
using Services.Interfaces;


namespace Services.Implementations
{
    public class MissionRatingService : BaseService<MissionRating>, IMissionRatingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHostEnvironment _env;

        public MissionRatingService(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment env) : base(unitOfWork.MissionRatingRepo, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }
    
    }
}
