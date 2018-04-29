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
    /// Interaction logic for CustomerMainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        CustomerMainViewModel custVM;
        HappyTourDatabaseEntities entities;
        MainWindow win;

        public MainPage(HappyTourDatabaseEntities entities, MainWindow parentWin)
        {
            InitializeComponent();
            this.entities = entities;
            this.win = parentWin;

        }


        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        { 
            this.mFrame.Content = new CustomerMainPage(entities);
        }

        private void btnTour_Click(object sender, RoutedEventArgs e)
        {
            this.mFrame.Content = new TourMainPage(entities);
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = new OrderMainViewModel();
        }

        private void btnTGuide_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = new TourGuideMainViewModel();
        }

        private void btnOffice_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = new OfficeMainViewModel();
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = new ReportMainViewModel();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            win.MainFrame.Content = new LoginPage(entities, win);
        }
    }
}
