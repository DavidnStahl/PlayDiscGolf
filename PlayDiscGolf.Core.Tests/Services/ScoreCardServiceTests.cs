using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using PlayDiscGolf.AutoMapper.Profiles.User;
using PlayDiscGolf.Core.AutoMapper.Profiles.Entities;
using PlayDiscGolf.Core.Business.DtoBuilder.ScoreCard;
using PlayDiscGolf.Core.Business.Session;
using PlayDiscGolf.Core.Dtos.Cards;
using PlayDiscGolf.Core.Enums;
using PlayDiscGolf.Core.Services.Account;
using PlayDiscGolf.Core.Services.Score;
using PlayDiscGolf.Core.Services.User;
using PlayDiscGolf.Core.Tests.MockHelpers;
using PlayDiscGolf.Infrastructure.UnitOfWork;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PlayDiscGolf.Core.Tests.Services
{    
    public class ScoreCardServiceTests
    {
        private readonly IScoreCardService _sut;
        private readonly Mock<ISessionStorage<ScoreCardDto>> _sessionStorageMock = new Mock<ISessionStorage<ScoreCardDto>>();
        private readonly Mock<IScoreCardDtoBuilder> scoreCardDtoBuildMock = new Mock<IScoreCardDtoBuilder>();
        private readonly Mock<IUnitOfwork> _unitOfworkMock = new Mock<IUnitOfwork>();
        private readonly Mock<IHttpContextAccessor> accessorMock = new Mock<IHttpContextAccessor>();
        private readonly IMapper _mapper;
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;

        public ScoreCardServiceTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new ScoreCardProfile());
                    mc.AddProfile(new HoleProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _userManagerMock = IdentityMockHelpers.MockUserManager<IdentityUser>();

            _sut = new ScoreCardService(_sessionStorageMock.Object,_mapper,scoreCardDtoBuildMock.Object,_unitOfworkMock.Object,accessorMock.Object, _userManagerMock.Object);
        }

        [Fact]
        public void OpenScoreCard_Assert_ExpectedScoreCardID_And_ExpectedHoleID_Are_Equal_Result_Values()
        {
            //Arange
            var expectedScoreCardID = Guid.NewGuid();
            var expectedholeID = Guid.NewGuid();

            var scoreCardID = expectedScoreCardID.ToString();
            var scoreCard = new ScoreCard
            {
                CourseID = Guid.NewGuid(),
                ScoreCardID = Guid.Parse(scoreCardID)
            };

            var hole = new Hole
            {
                HoleID = expectedholeID
            };

            _unitOfworkMock.Setup(x => x.ScoreCards.GetSingleScoreCardAndIncludePlayerCardAndHoleCardBy(x => x.ScoreCardID == Guid.Parse(scoreCardID))).Returns(scoreCard);
            _unitOfworkMock.Setup(x => x.Holes.GetCourseHole(scoreCard.CourseID, 1)).Returns(hole);


            //Act
            var result = _sut.OpenScoreCard(scoreCardID);

            //Assert
            Assert.Equal(expectedScoreCardID, result.ScoreCard.ScoreCardID);
            Assert.Equal(expectedholeID, result.Hole.HoleID);
        }

        [Fact]
        public void UpdateScore_When_addOrRemove_IsNull_unitOfWork_ScoreCards_GetSingleScoreCardAndIncludePlayerCardAndHoleCardBy_And_unitOfWork_Holes_GetCourseHole_Calls_Once()
        {
            //Arange
            var expectedScoreCardID = Guid.NewGuid();
            var expectedholeID = Guid.NewGuid();

            var username = "david";
            var holeNumber = "5";
            var scoreCardID = expectedScoreCardID.ToString();
            var scoreCard = new ScoreCard
            {
                CourseID = Guid.NewGuid(),
                ScoreCardID = Guid.Parse(scoreCardID)
            };

            var hole = new Hole
            {
                HoleID = expectedholeID
            };

            _unitOfworkMock.Setup(x => x.ScoreCards.GetSingleScoreCardAndIncludePlayerCardAndHoleCardBy(x => x.ScoreCardID == Guid.Parse(scoreCardID))).Returns(scoreCard);
            _unitOfworkMock.Setup(x => x.Holes.GetCourseHole(scoreCard.CourseID, Convert.ToInt32(holeNumber))).Returns(hole);

            //Act
            var result = _sut.UpdateScore(scoreCardID, holeNumber, null, username);

            //Assert
            _unitOfworkMock.Verify(x => x.ScoreCards.GetSingleScoreCardAndIncludePlayerCardAndHoleCardBy(x => x.ScoreCardID == Guid.Parse(scoreCardID)), Times.Once());
            _unitOfworkMock.Verify(x => x.Holes.GetCourseHole(scoreCard.CourseID, Convert.ToInt32(holeNumber)), Times.Once());
        }

        [Fact]

        public void RemovePlayerFromSessionAndReturnUpdatedPlayers_Should_RemovePlayerFromPlayerCard()
        {
            //Arrange
            var removePlayer = "david";
            var removedPlayerCardDto = new PlayerCardDto { UserName = removePlayer };
            var _sessionKey = EnumHelper.ScoreCardViewModelSessionKey.ScoreCardViewModel.ToString();

            var sessionModel = new ScoreCardDto
            {
                PlayerCards = new List<PlayerCardDto> 
                { 
                    new PlayerCardDto{UserName = removePlayer},
                }
            };

            _sessionStorageMock.Setup(x => x.Get(_sessionKey)).Returns(sessionModel);
            _sessionStorageMock.Setup(x => x.Save(_sessionKey, sessionModel));


            //Act
            var result = _sut.RemovePlayerFromSessionAndReturnUpdatedPlayers(removePlayer);

            //Assert
            Assert.DoesNotContain(removedPlayerCardDto, result);
        }
    }
}
