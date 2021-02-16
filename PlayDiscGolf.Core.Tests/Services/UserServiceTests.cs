using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using PlayDiscGolf.AutoMapper.Profiles.User;
using PlayDiscGolf.Core.AutoMapper.Profiles.Entities;
using PlayDiscGolf.Core.Services.Account;
using PlayDiscGolf.Core.Services.User;
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
    public class UserServiceTests
    {
        private readonly IUserService _sut;
        private readonly Mock<IUnitOfwork> _unitOfWorkMock = new Mock<IUnitOfwork>();
        private static IMapper _mapper;
        private readonly Mock<IAccountService> _accountServiceMock;
        private readonly Mock<IUserService> _userServiceMock;
        public UserServiceTests()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _userServiceMock = new Mock<IUserService>();

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new FriendProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _sut = new UserService(_accountServiceMock.Object, _unitOfWorkMock.Object, _mapper);            
        }

        [Fact]

        public async Task GetFriends_InloggedUserID_Should_Equal_Friends_Property_UserID()
        {
            //Arange
            var expected = Guid.NewGuid();

            var friends = new List<Friend>
            {
                new Friend { UserID = expected, FriendID = Guid.NewGuid(), FriendRequestAccepted = false, FriendUserID = Guid.NewGuid(), UserName = "david"},
                new Friend { UserID = expected, FriendID = Guid.NewGuid(), FriendRequestAccepted = false, FriendUserID = Guid.NewGuid(), UserName = "anders"}
            };

            var inloggedID = expected.ToString();
            _accountServiceMock.Setup(x => x.GetInloggedUserIDAsync()).ReturnsAsync(inloggedID);
            _unitOfWorkMock.Setup(x => x.Friends.FindAllBy(x => x.UserID == Guid.Parse(inloggedID))).Returns(friends);
            //Act
            var result = await _sut.GetFriendsAsync();

            //Assert
            var ectual = result.Select(x => x.UserID).Distinct().SingleOrDefault();

            Assert.Equal(ectual, expected);
        }

        [Fact]

        public async Task RemoveFriendAsync_Should_Call_UnitOfWorkComplete_Once()
        {
            //Arange
            var inloggedUsername = "david";
            var addedfriendID = Guid.NewGuid();
            var friendID = addedfriendID.ToString();
            var friend = new Friend { UserID = addedfriendID, FriendID = Guid.NewGuid(), FriendRequestAccepted = false, FriendUserID = Guid.NewGuid(), UserName = "david" };

            _accountServiceMock.Setup(x => x.GetUserName()).Returns(inloggedUsername);
            _unitOfWorkMock.Setup(x => x.Friends.FindSingleBy(x => x.FriendID == Guid.Parse(friendID))).Returns(friend);
            _unitOfWorkMock.Setup(x => x.Friends.Delete(friend));
            _unitOfWorkMock.Setup(x => x.Complete()).Returns(It.IsAny<int>);

            //Act
            await _sut.RemoveFriendAsync(friendID);

            //Assert
            _unitOfWorkMock.Verify(x => x.Complete(), Times.Once);
        }
    }
}
