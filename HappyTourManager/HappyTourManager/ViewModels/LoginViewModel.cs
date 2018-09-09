// <copyright file="LoginViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using BL;
    using DATA;
    using DATA.Interfaces;

    /// <summary>
    /// Login viewmodel
    /// </summary>
    internal class LoginViewModel : Bindable
    {
        private IRepository<User> userRepository;
        private string userName;
        private string password;
        private LoginBL loginBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
        /// Constructor of Login view model
        /// </summary>
        /// <param name="userRepo">user repository</param>
        public LoginViewModel(IRepository<User> userRepo)
        {
            this.userRepository = userRepo;
            this.loginBL = new LoginBL(this.userRepository);
        }

        /// <summary>
        /// Gets or sets contains given username
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
        /// Gets or sets contains given password
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
        /// Method for signing in user
        /// </summary>
        /// <returns>true, if sign in was successful</returns>
        public bool SignIn(string Password)
        {
            return this.loginBL.Login(this.Username, Password);
        }

        /// <summary>
        /// method for user signup
        /// </summary>
        /// <returns>true, if sign up was successful</returns>
        public bool SignUp(string Password)
        {
            return this.loginBL.RegisterUser(this.Username, Password);
        }
    }
}
