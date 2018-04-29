using DATA;
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

namespace HappyTourManager.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        LoginViewModel loginVM;
        HappyTourDatabaseEntities entities;
        MainWindow win;

        public LoginPage(HappyTourDatabaseEntities entities, MainWindow parentWin)
        {
            InitializeComponent();
            win = parentWin;
            this.entities = entities;
            loginVM = new LoginViewModel(entities);
            this.DataContext = loginVM;

        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (loginVM.SignIn())
            {
                win.SetPage("MainPage");
            }
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            loginVM.SignUp();
        }
    }
}
