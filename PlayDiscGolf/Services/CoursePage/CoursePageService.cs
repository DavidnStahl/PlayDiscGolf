using AutoMapper;
using PlayDiscGolf.Business.Calculations.ScoreCard;
using PlayDiscGolf.Data.Cards.Scores;
using PlayDiscGolf.Data.Courses;
using PlayDiscGolf.Data.Holes;
using PlayDiscGolf.Dtos;
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
            Course course = await _courseRepository.GetCourseByIDAsync(courseID);
            string userID = await _accountService.GetInloggedUserID();
            
            List<ScoreCardDto> scoreCard = _mapper.Map<List<ScoreCardDto>>(await _scoreCardRepository.GetScoreCardIncludePlayerCardIncludeHoleCardByIDAsync(userID));

            var x = new CourseInfoDto
            {
                CourseID = courseID,
                TotalDistance = course.TotalDistance,
                ScoreCards = scoreCard,
                FullName = course.FullName,
                Holes = await _holeRepository.GetHolesByCourseID(courseID),
                TotalHoles = course.HolesTotal,
                Name = course.Name,
                TotalParValue = course.TotalParValue,
                NumberOfRounds = scoreCard.Count,
                BestRound = scoreCard.Count > 0 ? _scoreCardCalculation.BestRound(scoreCard, userID).ToString() : "None",
                AverageRound = scoreCard.Count > 0 ? _scoreCardCalculation.AverageRound(scoreCard, userID).ToString() : "None"
            };

            return x;
        }        
    }
}
