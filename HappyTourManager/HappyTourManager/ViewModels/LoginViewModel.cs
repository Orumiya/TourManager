using BL;
using DATA;
using DATA.Interfaces;
using DATA.Repositoriees;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HappyTourManager
{
    class LoginViewModel : Bindable
    {
        private UserRepository userRepository;
        private string userName;
        private string password;
        public LoginBL loginBL;

        public string Username {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged(nameof(Username));
            }

        }

        public string Password {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }

        }

        public LoginViewModel(HappyTourDatabaseEntities entities)
        {
            userRepository = new UserRepository(entities);
            loginBL = new LoginBL(userRepository);
        }


        public bool SignIn()
        {
            return loginBL.Login(Username, Password);
        }

        public bool SignUp()
        {
            return loginBL.RegisterUser(Username, Password);
        }

    }
}
