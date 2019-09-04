/* 
 * Author: Smailovic Alen
 * Date:   01.09.2019
*/

using IntrovertAuthenticationAPI.Entities;
using IntrovertAuthenticationAPI.IntegrationTests.Helper;
using IntrovertAuthenticationAPI.Services;
using IntrovertAuthenticationAPI.Settings;
using NUnit.Framework;

namespace IntrovertAuthenticationAPI.IntegrationTests.Services
{
    [TestFixture]
    class UserServiceTests
    {
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            IdentityDatabase identityDatabase = ConnectionHelper.Create();

            _userService = new UserService(identityDatabase);
        }

        [TearDown]
        public void TearDown()
        {
            _userService = null;
        }

        [Test]
        public void CreateUserAsyncTest()
        {
            UserEntity userEntity = new UserEntity
            {
                Id = "testID",
                Username = "test"
            };

            _userService.CreateUser(userEntity).Wait();
            _userService.RemoveUser("testID").Wait();

            UserEntity result = _userService.GetUser("testID").Result;
            Assert.IsNull(result);
        }

        [Test]
        public void RemoveUserAsyncTest()
        {
            UserEntity userEntity = new UserEntity
            {
                Id = "testID",
                Username = "test"
            };

            _userService.CreateUser(userEntity).Wait();

            UserEntity result = _userService.GetUser("testID").Result;
            Assert.AreEqual(userEntity, result);

            _userService.RemoveUser("testID").Wait();

            result = _userService.GetUser("testID").Result;
            Assert.IsNull(result);
        }
    }
}
