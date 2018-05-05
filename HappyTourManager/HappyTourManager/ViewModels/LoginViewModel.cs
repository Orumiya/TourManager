// <copyright file="App.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using BL;
    using DATA;
    using DATA.Interfaces;

    class LoginViewModel : Bindable
    {
        private IRepository<User> userRepository;
        private string userName;
        private string password;
        public LoginBL loginBL;

        /// <summary>
        /// Contains given username
        /// </summary>
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

        /// <summary>
        /// contains given password
        /// </summary>
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

        /// <summary>
        /// Constructor of Login view model
        /// </summary>
        /// <param name="userRepo"></param>
        public LoginViewModel(IRepository<User> userRepo)
        {
            this.userRepository = userRepo;
            this.loginBL = new LoginBL(this.userRepository);
        }

        /// <summary>
        /// Method for signing in user
        /// </summary>
        /// <returns>true, if sign in was successful</returns>
        public bool SignIn()
        {
            return this.loginBL.Login(this.Username, this.Password);
        }

        /// <summary>
        /// method for user signup
        /// </summary>
        /// <returns>true, if sign up was successful</returns>
        public bool SignUp()
        {
            return this.loginBL.RegisterUser(this.Username, this.Password);
        }

    }
}
