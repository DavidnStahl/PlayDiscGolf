using AutoMapper;
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

        public CoursePageService(ICourseRepository courseRepository,
            IHoleRepository holeRepository,
            IScoreCardRepository scoreCardRepository,
            IAccountService accountService,
            IMapper mapper)
        {
            _courseRepository = courseRepository;
            _holeRepository = holeRepository;
            _scoreCardRepository = scoreCardRepository;
            _accountService = accountService;
            _mapper = mapper;
        }
        public async Task<CourseInfoDto> GetCoursePageInformationAsync(Guid courseID)
        {
            var course = await _courseRepository.GetCourseByIDAsync(courseID);
            var userID = await _accountService.GetInloggedUserID();
            var scoreCard = _mapper.Map<List<ScoreCardDto>>(await _scoreCardRepository.GetScoreCardIncludePlayerCardIncludeHoleCardByIDAsync(userID));

            var courseInfoDto = new CourseInfoDto
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
                BestRound = BestScoreCardRound(scoreCard, userID),
                AverageRound = AverageScoreCardRound(scoreCard, userID)
            };

            return courseInfoDto;
        }

        public string AverageScoreCardRound(List<ScoreCardDto> scoreCards, string userID)
        {
            if (scoreCards.Count == 0) return null;

            var sumTotal = 0;
            
            foreach (var scoreCard in scoreCards)
            {
                
                foreach (var player in scoreCard.PlayerCards)
                {
                    if (player.UserID == userID)
                    {
                        var sumHoles = 0;
                        foreach (var holecard in player.HoleCards)
                        {
                            sumHoles += holecard.Score;
                        }

                        sumTotal += sumHoles;
                    }
                    
                }
            }

            return (sumTotal / scoreCards.Count).ToString();
        }
        public string BestScoreCardRound(List<ScoreCardDto> scoreCards, string userID)
        {
            if (scoreCards.Count == 0) return null;

            var bestScore = 0;
            foreach (var scoreCard in scoreCards)
            {
                var sumHoles = 0;
                foreach (var player in scoreCard.PlayerCards)
                {
                    if (player.UserID == userID)
                    {
                        
                        foreach (var holecard in player.HoleCards)
                        {
                            sumHoles += holecard.Score;
                        }
                    }
                }

                if (bestScore > sumHoles || bestScore < sumHoles)
                    bestScore = sumHoles;
            }

            return bestScore.ToString();
        }
    }
}
