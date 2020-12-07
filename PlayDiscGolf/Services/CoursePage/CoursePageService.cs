using AutoMapper;
using PlayDiscGolf.Business.Calculations.ScoreCard;
using PlayDiscGolf.Data.Cards.Scores;
using PlayDiscGolf.Data.Courses;
using PlayDiscGolf.Data.Holes;
using PlayDiscGolf.Dtos;
using PlayDiscGolf.Enums;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.CoursePage
{
    public class CoursePageService : ICoursePageService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IHoleRepository _holeRepository;
        private readonly IScoreCardRepository _scoreCardRepository;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IScoreCardCalculation _scoreCardCalculation;

        public CoursePageService(ICourseRepository courseRepository, IHoleRepository holeRepository,
            IScoreCardRepository scoreCardRepository, IAccountService accountService, IMapper mapper,
            IScoreCardCalculation scoreCardCalculation)
        {
            _courseRepository = courseRepository;
            _holeRepository = holeRepository;
            _scoreCardRepository = scoreCardRepository;
            _accountService = accountService;
            _mapper = mapper;
            _scoreCardCalculation = scoreCardCalculation;
        }
        public async Task<CourseInfoDto> GetCoursePageInformationAsync(Guid courseID)
        {
            var course = await _courseRepository.GetCourseByIDAsync(courseID);

            var userID = await _accountService.GetInloggedUserID();   
            
            var scoreCards = _mapper.Map<List<ScoreCardDto>>(await _scoreCardRepository.GetScoreCardIncludePlayerCardIncludeHoleCardByIDAsync(userID, courseID));

            return new CourseInfoDto
            {
                CourseID = courseID,
                TotalDistance = course.TotalDistance,
                ScoreCards = scoreCards,
                FullName = course.FullName,
                Holes = await _holeRepository.GetHolesByCourseIDAsync(courseID),
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
