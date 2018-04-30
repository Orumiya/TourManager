using DATA;
using DATA.Interfaces;
using DATA.Repositoriees;
using DATA.Repositories;
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

        IRepository<Customer> customerRepo;
        IRepository<Language> languageRepo;
        IRepository<Office> officeRepo;
        IRepository<OnHoliday> onHolidayRepo;
        IRepository<Order> orderRepo;
        IRepository<Place> placeRepo;
        IRepository<PLTCON> pltconRepo;
        IRepository<Program> programRepo;
        IRepository<PRTCON> prtconRepo;
        IRepository<Report> reportRepo;
        IRepository<Tourguide> tourguideRepo;
        IRepository<Tour> tourRepo;
        IRepository<User> userRepo;
        private string selectedPage;
        MainWindow win;

        public MainPage(IRepository<Customer> customerRepo,
                IRepository<Language> languageRepo,
                IRepository<Office> officeRepo,
                IRepository<OnHoliday> onHolidayRepo,
                IRepository<Order> orderRepo,
                IRepository<Place> placeRepo,
                IRepository<PLTCON> pltconRepo,
                IRepository<Program> programRepo,
                IRepository<PRTCON> prtconRepo,
                IRepository<Report> reportRepo,
                IRepository<Tourguide> tourguideRepo,
                IRepository<Tour> tourRepo,
                IRepository<User> userRepo,
                MainWindow parentWin)
        {
            InitializeComponent();
            this.customerRepo = customerRepo;
            this.languageRepo = languageRepo;
            this.officeRepo = officeRepo;
            this.onHolidayRepo = onHolidayRepo;
            this.orderRepo = orderRepo;
            this.placeRepo = placeRepo;
            this.pltconRepo = pltconRepo;
            this.programRepo = programRepo;
            this.prtconRepo = prtconRepo;
            this.reportRepo = reportRepo;
            this.tourguideRepo = tourguideRepo;
            this.tourRepo = tourRepo;
            this.userRepo = userRepo;
            this.win = parentWin;

        }


        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        { 
            this.mFrame.Content = new CustomerMainPage(customerRepo);
        }

        private void btnTour_Click(object sender, RoutedEventArgs e)
        {
            this.mFrame.Content = new TourMainPage(tourRepo, placeRepo, pltconRepo, programRepo, prtconRepo, tourguideRepo);
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            this.mFrame.Content = new OrderMainPage(orderRepo, customerRepo, tourRepo, programRepo, placeRepo, pltconRepo, prtconRepo);
        }

        private void btnTGuide_Click(object sender, RoutedEventArgs e)
        {
            this.mFrame.Content = new TourGuideMainPage();
        }

        private void btnOffice_Click(object sender, RoutedEventArgs e)
        {
            this.mFrame.Content = new OfficeMainPage();
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            this.mFrame.Content = new ReportMainPage();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            win.MainFrame.Content = new LoginPage(userRepo, win);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
