using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlayDiscGolf.Core.Business.Calculations.ScoreCard;
using PlayDiscGolf.Core.Dtos.Cards;
using PlayDiscGolf.Core.Dtos.Course;
using PlayDiscGolf.Core.Dtos.Entities;
using PlayDiscGolf.Core.Enums;
using PlayDiscGolf.Core.Services.Account;
using PlayDiscGolf.Infrastructure.UnitOfWork;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Services.CoursePage
{
    public class CoursePageService : ICoursePageService
    {
        private readonly IMapper _mapper;
        private readonly IScoreCardCalculation _scoreCardCalculation;
        private readonly IAccountService _accountService;
        private readonly IUnitOfwork _unitOfWork;

        public CoursePageService(
            IUnitOfwork unitOfWork,
            IMapper mapper,
            IScoreCardCalculation scoreCardCalculation,
            IAccountService accountService)
        {
            _mapper = mapper;
            _scoreCardCalculation = scoreCardCalculation;
            _accountService = accountService;
            _unitOfWork = unitOfWork;
        }
        public async Task<CourseInfoDto> GetCoursePageInformation(Guid courseID)
        {
            var course = _unitOfWork.Courses.FindById(courseID);
            var userID = await _accountService.GetInloggedUserIDAsync();

            var scoreCardsEntity = _unitOfWork.ScoreCards.GetScoreCardAndIncludePlayerCardAndHoleCard(x => x.UserID == userID && x.CourseID == courseID);
            var scoreCards = _mapper.Map<List<ScoreCardDto>>(scoreCardsEntity);
            var holesEntity = _unitOfWork.Holes.FindBy(x => x.CourseID == courseID);
            var holes = _mapper.Map<List<HoleDto>>(holesEntity);

            return new CourseInfoDto
            {
                CourseID = courseID,
                TotalDistance = course.TotalDistance,
                ScoreCards = scoreCards,
                FullName = course.FullName,
                Holes = holes,
                TotalHoles = course.HolesTotal,
                Name = course.Name,
                TotalParValue = course.TotalParValue,
                NumberOfRounds = scoreCards.Count,
                BestRound = scoreCards.Count > 0 ? _scoreCardCalculation.BestRound(scoreCards, userID).ToString() : EnumHelper.BestRound.None.ToString(),
                AverageRound = scoreCards.Count > 0 ? _scoreCardCalculation.AverageRound(scoreCards, userID).ToString() : EnumHelper.AverageRound.None.ToString()
            };
        }        
    }
}
