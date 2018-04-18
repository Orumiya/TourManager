using BL;
using DATA;
using DATA.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEST.Fakes;

namespace TEST
{
    [TestFixture]
    class LoginBLTest
    {
        private FakeRepository<User> userRepository;
        private User[] users;
        private LoginBL bl;

        public LoginBLTest()
        {
            CreateUserTestArray();
            this.userRepository = new FakeRepository<User>(users);
            bl = new LoginBL(userRepository);

        }

        private void CreateUserTestArray()
        {
            users = new[]
            {
                new User
                {
                    Username = "user1",
                    Password = "CZYdfgdnpA6AtF+PmpqsrflTDlL2HXDSGfppavmbHNE="
                }
            };
        }

        [Test]
        public void WhenLoginBLCreated_ThenLoginBLIsNotNull()
        {
            //ARRANGE + ACT
            //ASSERT
            Assert.That(bl, Is.Not.Null);
        }

        [Test]
        public void WhenNewUserWithValidUsernameAndPasswordCreated_ThenValidUsernameAndPasswordValidatesToTrue()
        {
            //ARRANGE
            string username = "aranka4";
            string password = "aranka4";

            //ACT
            bool valid = bl.RegisterUser(username, password);

            //ASSERT
            Assert.That(valid, Is.True);
        }

        [TestCase("dre4","aranka4")] //username less than 5
        [TestCase("dred4", "ara4")] //password less than 5
        [TestCase("aaaaaaaaaasdsd1", "aranka4")] //username not less than 15
        [TestCase("user3", "aranka4rfdertzu")] //username not less than 15
        [TestCase("user!gh", "aranka4")] //username with spec char
        [TestCase("user3", "ara%nka4")] //password with spec char
        [TestCase("user1", "aranka4")] //username already exists
        public void WhenNewUserWithInvalidUsernameAndPasswordCreated_ThenThrowsInvalidOperationexception(string username, string password)
        {
            //ACT + ASSERT
            Assert.That(() => bl.RegisterUser(username, password), Throws.InvalidOperationException);
        }

        [Test]
        public void WhenUserWithCorrectPasswordLogsIn_ThenLoginIsSuccessful()
        {
            //ARRANGE
            CreateUserTestArray();
            //ACT
            bool loggedin = bl.Login("user1","user1");

            //ASSERT
            Assert.That(loggedin, Is.True);
        }

    }
}
