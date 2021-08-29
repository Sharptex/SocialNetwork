using Moq;
using NUnit.Framework;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;

namespace SocialNetwork.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public void AddFriend_DoesNotThrowExceptions()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(p => p.FindByEmail("h@gmail.ru")).Returns(new UserEntity() { id = 2, email = "h@gmail.ru" });
            var mockfriendRepository = new Mock<IFriendRepository>();
            mockfriendRepository.Setup(p => p.Create(It.Is<FriendEntity>(h => h.id == 0 && h.user_id == 1 && h.friend_id == 2))).Returns(1);
            var userServiceTest = new UserService(mockUserRepository.Object, mockfriendRepository.Object);

            Assert.DoesNotThrow(() => userServiceTest.AddFriend(new UserAddFriendData() { UserId = 1, FriendEmail = "h@gmail.ru" }));
        }

        [Test]
        public void AddFriend_MustThrowUserNotFoundException()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(p => p.FindByEmail("h@gmail.ru")).Returns(new UserEntity() { id = 2, email = "h@gmail.ru" });
            var mockfriendRepository = new Mock<IFriendRepository>();
            mockfriendRepository.Setup(p => p.Create(It.Is<FriendEntity>(h => h.id == 0 && h.user_id == 1 && h.friend_id == 2))).Returns(1);
            var userServiceTest = new UserService(mockUserRepository.Object, mockfriendRepository.Object);

            Assert.Throws<UserNotFoundException>(() => userServiceTest.AddFriend(new UserAddFriendData() { UserId = 1, FriendEmail = "h2@gmail.ru" }));
        }

        [Test]
        public void DeleteFriend_NotExistsIdsPassed_MustThrowException()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockfriendRepository = new Mock<IFriendRepository>();
            mockfriendRepository.Setup(p => p.Delete(1, 2)).Returns(1);
            var userServiceTest = new UserService(mockUserRepository.Object, mockfriendRepository.Object);

            Assert.Throws<Exception>(() => userServiceTest.DeleteFriend(2, 1));
        }

        [Test]
        public void DeleteFriend_ExistedIdsPassed_DoesNotThrowException()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockfriendRepository = new Mock<IFriendRepository>();
            mockfriendRepository.Setup(p => p.Delete(1, 2)).Returns(1);
            var userServiceTest = new UserService(mockUserRepository.Object, mockfriendRepository.Object);

            Assert.DoesNotThrow(() => userServiceTest.DeleteFriend(1, 2));
        }
    }
}
