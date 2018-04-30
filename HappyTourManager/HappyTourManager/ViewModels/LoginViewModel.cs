namespace HappyTourManager
{
    using DATA.Repositoriees;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Security;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using BL;
    using DATA;
    using DATA.Interfaces;

    class LoginViewModel : Bindable
    {
        private IRepository<User> userRepository;
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

        public LoginViewModel(IRepository<User> userRepo)
        {
            this.userRepository = userRepo;
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
