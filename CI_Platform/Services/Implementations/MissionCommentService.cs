using AutoMapper;
using Common.Constants;
using Common.CustomExceptions;
using Common.Enums;
using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Entities.DTOs;
using Services.Interfaces;
using System.Linq.Expressions;

namespace Services.Implementations
{
    public class MissionCommentService : BaseService<MissionComment>, IMissionCommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IVolunteerService _volunteerService;
        private readonly IMissionService _missionService;

        public MissionCommentService(IUnitOfWork unitOfWork, IMapper mapper, IVolunteerService volunteerService, IMissionService missionService) : base(unitOfWork.MissionCommentRepo, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _volunteerService = volunteerService;
            _missionService = missionService;
        }

        public async Task PostComments(MissionCommentDTO dto, long sessionUserId)
        {
            Volunteer volunteer = await _volunteerService.GetVolunteerByUserId(sessionUserId);
            if(volunteer == null || volunteer.Id == 0)
                throw new EntityNullException(ExceptionMessage.VOLUNTEER_NOT_FOUND);

            if (string.IsNullOrEmpty(dto.Text))
                throw new EntityNullException(ExceptionMessage.CANNOT_POST_EMPTY_COMMENT);

            long volunteerId = volunteer.Id;

            bool missionFound = await _missionService.CheckForMission(dto.MissionId);
            if (!missionFound)
                throw new EntityNullException(ExceptionMessage.MISSION_NOT_FOUND);

            MissionComment missionComment = _mapper.Map<MissionComment>(dto);
            missionComment.VolunteerId = volunteerId;

            await AddAsync(missionComment);
        }

        public async Task<List<CommentInfoDTO>> GetCommentsForMissionAsync(long missionId)
        {
            bool missionFound = await _missionService.CheckForMission(missionId);
            if (!missionFound)
                throw new EntityNullException(ExceptionMessage.MISSION_NOT_FOUND);

            Expression<Func<MissionComment, bool>> expression = c => c.MissionId == missionId;

            List<Expression<Func<MissionComment, object>>>? expressions = new();

            expressions.Add(x => x.Volunteer.User);

            List<MissionComment> comments = await _unitOfWork.MissionCommentRepo.GetAllAsync(expression, includes: expressions.AsEnumerable(), "CreatedOn", SystemConstant.DESCENDING);

            List<CommentInfoDTO> commentDTOs = _mapper.Map<List<CommentInfoDTO>>(comments);

            return commentDTOs;
        }
    }
}
