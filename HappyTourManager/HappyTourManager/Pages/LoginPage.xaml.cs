// <copyright file="LoginPage.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager.Pages
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using DATA;
    using DATA.Interfaces;

    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private LoginViewModel loginVM;
        private IRepository<User> userRepo;
        private MainWindow win;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class.
        /// Login page
        /// </summary>
        /// <param name="userRepo">user repository</param>
        /// <param name="parentWin">parent window</param>
        public LoginPage(IRepository<User> userRepo, MainWindow parentWin)
        {
            this.InitializeComponent();
            this.win = parentWin;
            this.userRepo = userRepo;
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (this.loginVM.SignIn())
            {
                this.win.SetPage("MainPage");
            }
            else
            {
                MessageBox.Show("Incorrect username or password!");
            }
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.loginVM.SignUp();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Data is not valid!");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.loginVM = new LoginViewModel(this.userRepo);
            this.DataContext = this.loginVM;
        }
    }
}
