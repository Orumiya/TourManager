namespace HappyTourManager.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using DATA;
    using DATA.Interfaces;
    using DATA.Repositoriees;

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
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            this.loginVM.SignUp();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.loginVM = new LoginViewModel(this.userRepo);
            this.DataContext = this.loginVM;
        }
    }
}
