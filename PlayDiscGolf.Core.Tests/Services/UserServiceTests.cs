using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using PlayDiscGolf.AutoMapper.Profiles.User;
using PlayDiscGolf.Core.AutoMapper.Profiles.Entities;
using PlayDiscGolf.Core.Services.Account;
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
    public class UserServiceTests
    {
        private readonly IUserService _sut;
        private readonly Mock<IUnitOfwork> _unitOfWorkMock = new Mock<IUnitOfwork>();
        private static IMapper _mapper;
        private readonly Mock<IAccountService> _accountServiceMock;
        public UserServiceTests()
        {
            _accountServiceMock = new Mock<IAccountService>();

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

        [Fact]

        public async Task GetFriendRequestsAsync_Should_Call_GetUserByIDAsync_Once()
        {
            //Arange
            var inloggedUsername = "david";
            var userID = Guid.NewGuid();
            var friends = new List<Friend>
            {
                new Friend { UserID = Guid.NewGuid(), FriendID = userID, FriendRequestAccepted = false, FriendUserID = Guid.NewGuid(), UserName = "david" }
            };
            
            var user = new IdentityUser { Id = userID.ToString() };

            _accountServiceMock.Setup(x => x.GetUserName()).Returns(inloggedUsername);
            _accountServiceMock.Setup(x => x.GetUserByQueryAsync(inloggedUsername)).ReturnsAsync(user);
            _unitOfWorkMock.Setup(x => x.Friends.FindAllBy(x => x.FriendUserID == Guid.Parse(user.Id) && x.FriendRequestAccepted == false)).Returns(friends);
            _accountServiceMock.Setup(x => x.GetUserByIDAsync(friends[0].UserID.ToString())).ReturnsAsync(new IdentityUser());
            
            //Act
            var result = await _sut.GetFriendRequestsAsync();

            //Assert
            _accountServiceMock.Verify(x => x.GetUserByIDAsync(friends[0].UserID.ToString()), Times.Once());
        }

        [Fact]

        public async Task RemoveExtraFriendAsync_Should_Call_unitOfWorkFriendsDelete_Once()
        {
            //Arange
            var inloggedUsername = "david";            
            var addedfriendID = Guid.NewGuid();
            var userRemovedFriendUserID = addedfriendID;
            var friendID = addedfriendID.ToString();
            var friend = new Friend { UserID = addedfriendID, FriendID = Guid.NewGuid(), FriendRequestAccepted = false, FriendUserID = Guid.NewGuid(), UserName = "david" };

            var user = new IdentityUser { Id = addedfriendID.ToString() };

            _accountServiceMock.Setup(x => x.GetUserName()).Returns(inloggedUsername);
            _accountServiceMock.Setup(x => x.GetUserByQueryAsync(inloggedUsername)).ReturnsAsync(user);
            _unitOfWorkMock.Setup(x => x.Friends.FindSingleBy(x => x.UserID == userRemovedFriendUserID && x.FriendUserID == Guid.Parse(user.Id))).Returns(friend);
            _unitOfWorkMock.Setup(x => x.Friends.Delete(friend));

            //Act
           await _sut.RemoveExtraFriendAsync(userRemovedFriendUserID);

            //Assert
            _unitOfWorkMock.Verify(x => x.Friends.Delete(friend), Times.Once);
        }

        [Fact]

        public void DeclineFriendRequest_Should_Call_UnitOfWorkFriendsFindById_Once()
        {
            //Arange
            var friendID = Guid.NewGuid().ToString();
            var friend = new Friend { UserID = Guid.NewGuid(), FriendID = Guid.NewGuid(), FriendRequestAccepted = false, FriendUserID = Guid.Parse(friendID), UserName = "david" };
            _unitOfWorkMock.Setup(x => x.Friends.FindById(Guid.Parse(friendID))).Returns(friend);
            _unitOfWorkMock.Setup(x => x.Friends.Delete(friend));
            _unitOfWorkMock.Setup(x => x.Complete()).Returns(It.IsAny<int>);

            //Act
            _sut.DeclineFriendRequest(friendID);

            //Assert
            _unitOfWorkMock.Verify(x => x.Friends.FindById(Guid.Parse(friendID)), Times.Once());
        }

        [Fact]

        public void DeclineFriendRequest_Should_Call_unitOfWorkFriendsDelete_Once()
        {
            //Arange
            var friendID = Guid.NewGuid().ToString();
            var friend = new Friend { UserID = Guid.NewGuid(), FriendID = Guid.NewGuid(), FriendRequestAccepted = false, FriendUserID = Guid.Parse(friendID), UserName = "david" };
            _unitOfWorkMock.Setup(x => x.Friends.FindById(Guid.Parse(friendID))).Returns(friend);
            _unitOfWorkMock.Setup(x => x.Friends.Delete(friend));
            _unitOfWorkMock.Setup(x => x.Complete()).Returns(It.IsAny<int>);

            //Act
            _sut.DeclineFriendRequest(friendID);

            //Assert
            _unitOfWorkMock.Verify(x => x.Friends.Delete(friend), Times.Once());
        }

        [Fact]

        public void DeclineFriendRequest_Should_Call_unitOfWorkComplete_Once()
        {
            //Arange
            var friendID = Guid.NewGuid().ToString();
            var friend = new Friend { UserID = Guid.NewGuid(), FriendID = Guid.NewGuid(), FriendRequestAccepted = false, FriendUserID = Guid.Parse(friendID), UserName = "david" };
            _unitOfWorkMock.Setup(x => x.Friends.FindById(Guid.Parse(friendID))).Returns(friend);
            _unitOfWorkMock.Setup(x => x.Friends.Delete(friend));
            _unitOfWorkMock.Setup(x => x.Complete()).Returns(It.IsAny<int>);

            //Act
            _sut.DeclineFriendRequest(friendID);

            //Assert
            _unitOfWorkMock.Verify(x => x.Complete(), Times.Once());
        }

        [Fact]
        public async Task SendFriendRequestAsync_Should_Call_UnitOfWork_Complete_Once()
        {
            //Arange
            var username = "david";
            var inloggedUserID = Guid.NewGuid().ToString();
            var user = new IdentityUser
            {
                UserName = "david",
                Id = Guid.NewGuid().ToString(),
            };
            _accountServiceMock.Setup(x => x.GetUserByQueryAsync(username)).ReturnsAsync(user);
            _accountServiceMock.Setup(x => x.GetInloggedUserIDAsync()).ReturnsAsync(inloggedUserID);
            
            var friend = new Friend
            {
                UserName = username,
                UserID = Guid.Parse(inloggedUserID),
                FriendID = Guid.NewGuid(),
                FriendUserID = Guid.Parse(user.Id),
                FriendRequestAccepted = false
            };

            _unitOfWorkMock.Setup(x => x.Friends.Add(friend)).Returns(true);
            _unitOfWorkMock.Setup(x => x.Complete()).Returns(It.IsAny<int>);


            //Act
            await _sut.SendFriendRequestAsync(username);

            //Assert
            _unitOfWorkMock.Verify(x => x.Complete(), Times.Once());
        }

    }
}
