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
        LoginViewModel loginVM;
        IRepository<User> userRepo;
        MainWindow win;

        public LoginPage(IRepository<User> userRepo, MainWindow parentWin)
        {
            this.InitializeComponent();
            this.win = parentWin;
            this.userRepo = userRepo;

        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
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

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.loginVM.SignUp();
            }
            catch (InvalidOperationException ex)
            {

                MessageBox.Show(ex.Message);
            }
            catch(NullReferenceException)
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
