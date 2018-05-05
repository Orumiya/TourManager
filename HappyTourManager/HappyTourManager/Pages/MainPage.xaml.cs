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
    using DATA.Repositories;

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
            this.InitializeComponent();
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
            this.mFrame.Content = new CustomerMainPage(this.customerRepo);
        }

        private void btnTour_Click(object sender, RoutedEventArgs e)
        {
            this.mFrame.Content = new TourMainPage(this.tourRepo, this.placeRepo, this.pltconRepo, this.programRepo, this.prtconRepo, this.tourguideRepo);
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            this.mFrame.Content = new OrderMainPage(this.orderRepo, this.customerRepo, this.tourRepo, this.programRepo, this.placeRepo, this.pltconRepo, this.prtconRepo);
        }

        private void btnTGuide_Click(object sender, RoutedEventArgs e)
        {
            this.mFrame.Content = new TourGuideMainPage(this.tourguideRepo,this.languageRepo,this.onHolidayRepo,this.tourRepo);
        }

        private void btnOffice_Click(object sender, RoutedEventArgs e)
        {
            this.mFrame.Content = new OfficeMainPage();
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            this.mFrame.Content = new ReportMainPage(reportRepo, orderRepo,customerRepo, tourRepo, tourguideRepo,
                                        languageRepo, onHolidayRepo, programRepo, placeRepo, pltconRepo, prtconRepo);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.win.MainFrame.Content = new LoginPage(this.userRepo, this.win);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
