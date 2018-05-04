namespace HappyTourManager
{
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
    using DATA.Repositoriees;

    class LoginViewModel : Bindable
    {
        private IRepository<User> userRepository;
        private string userName;
        private string password;
        public LoginBL loginBL;

        public string Username
        {
            get
            {
                return this.userName;
            }

            set
            {
                this.userName = value;
                this.OnPropertyChanged(nameof(this.Username));
            }

        }

        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                this.password = value;
                this.OnPropertyChanged(nameof(this.Password));
            }

        }

        public LoginViewModel(IRepository<User> userRepo)
        {
            this.userRepository = userRepo;
            this.loginBL = new LoginBL(this.userRepository);
        }

        public bool SignIn()
        {
            return this.loginBL.Login(this.Username, this.Password);
        }

        public bool SignUp()
        {
            return this.loginBL.RegisterUser(this.Username, this.Password);
        }

    }
}
