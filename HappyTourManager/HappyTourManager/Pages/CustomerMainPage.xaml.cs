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
    public partial class CustomerMainPage : Page
    {
        

        public CustomerMainPage()
        {
            InitializeComponent();
        }

        

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = new CustomerMainViewModel();
        }

        private void btnTour_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = new TourMainViewModel();
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
            
        }
    }
}
