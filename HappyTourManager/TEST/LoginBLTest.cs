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

        public LoginBLTest()
        {
            CreateUserTestArray();
            this.userRepository = new FakeRepository<User>(users);
        }

        private void CreateUserTestArray()
        {
            users = new[]
            {
                new User
                {
                    Username = "user1",
                    Password = "0A041B9462CAA4A31BAC3567E0B6E6FD9100787DB2AB433D96F6D178CABFCE90"
                }
            };
        }
    }
}
