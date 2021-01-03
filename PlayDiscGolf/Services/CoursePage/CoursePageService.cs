using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlayDiscGolf.Business.Calculations.ScoreCard;
using PlayDiscGolf.Data;
using PlayDiscGolf.Dtos;
using PlayDiscGolf.Enums;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.ViewModels.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.CoursePage
{
    public class CoursePageService : ICoursePageService
    {
        private readonly IEntityRepository<Course> _courseRepository;
        private readonly IEntityRepository<Hole> _holeRepository;
        private readonly IEntityRepository<ScoreCard> _scoreCardRepository;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IScoreCardCalculation _scoreCardCalculation;

        public CoursePageService(
            IEntityRepository<Course> courseRepository,
            IEntityRepository<Hole> holeRepository,
            IEntityRepository<ScoreCard> scoreCardRepository,
            IAccountService accountService,
            IMapper mapper,
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
            var course = _courseRepository.FindById(courseID);
            var userID = await _accountService.GetInloggedUserIDAsync();

            var scoreCards = _mapper.Map<List<ScoreCardDto>>(_scoreCardRepository
                .GetAll()
                .Include(x => x.PlayerCards)
                .ThenInclude(x => x.HoleCards)
                .Where(x => x.UserID == userID && x.CourseID == courseID)
                .OrderByDescending(x => x.StartDate)
                .ToList());

            return new CourseInfoDto
            {
                CourseID = courseID,
                TotalDistance = course.TotalDistance,
                ScoreCards = _mapper.Map<List<ScoreCardDto>>(scoreCards),
                FullName = course.FullName,
                Holes = _holeRepository.FindBy(x => x.CourseID == courseID),
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
