// <copyright file="MainPage.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager.Pages
{
    using System.Windows;
    using System.Windows.Controls;
    using DATA;
    using DATA.Interfaces;

    /// <summary>
    /// Interaction logic for CustomerMainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private IRepository<Customer> customerRepo;
        private IRepository<Language> languageRepo;
        private IRepository<OnHoliday> onHolidayRepo;
        private IRepository<Order> orderRepo;
        private IRepository<Place> placeRepo;
        private IRepository<PLTCON> pltconRepo;
        private IRepository<Program> programRepo;
        private IRepository<PRTCON> prtconRepo;
        private IRepository<Report> reportRepo;
        private IRepository<Tourguide> tourguideRepo;
        private IRepository<Tour> tourRepo;
        private IRepository<User> userRepo;
        private IRepository<Office> officeRepo;
        private MainWindow win;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// Main page
        /// </summary>
        /// <param name="customerRepo">customer</param>
        /// <param name="languageRepo">language</param>
        /// <param name="onHolidayRepo">onholiday</param>
        /// <param name="orderRepo">order</param>
        /// <param name="placeRepo">place</param>
        /// <param name="pltconRepo">pltcon</param>
        /// <param name="programRepo">program</param>
        /// <param name="prtconRepo">prtcon</param>
        /// <param name="reportRepo">report</param>
        /// <param name="tourguideRepo">tourguide</param>
        /// <param name="tourRepo">tourrepo</param>
        /// <param name="userRepo">userrepo</param>
        /// <param name="officeRepo">officerepo</param>
        /// <param name="parentWin">parentwn</param>
        public MainPage(
            IRepository<Customer> customerRepo,
                IRepository<Language> languageRepo,
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
                IRepository<Office> officeRepo,
                MainWindow parentWin)
        {
            this.InitializeComponent();
            this.customerRepo = customerRepo;
            this.languageRepo = languageRepo;
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

            this.officeRepo = officeRepo;
            this.win = parentWin;
        }

        private void BtnCustomer_Click(object sender, RoutedEventArgs e)
        {
            this.mFrame.Content = new CustomerMainPage(this.customerRepo);
        }

        private void BtnTour_Click(object sender, RoutedEventArgs e)
        {
            this.mFrame.Content = new TourMainPage(this.tourRepo, this.placeRepo, this.pltconRepo, this.programRepo, this.prtconRepo, this.tourguideRepo);
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            this.mFrame.Content = new OrderMainPage(this.orderRepo, this.customerRepo, this.tourRepo, this.programRepo, this.placeRepo, this.pltconRepo, this.prtconRepo);
        }

        private void BtnTGuide_Click(object sender, RoutedEventArgs e)
        {
            this.mFrame.Content = new TourGuideMainPage(this.tourguideRepo, this.languageRepo, this.onHolidayRepo);
        }

        private void BtnOffice_Click(object sender, RoutedEventArgs e)
        {
            this.mFrame.Content = new OfficeMainPage(this.officeRepo);
        }

        private void BtnReports_Click(object sender, RoutedEventArgs e)
        {
            this.mFrame.Content = new ReportMainPage(this.reportRepo, this.orderRepo, this.customerRepo, this.tourRepo, this.tourguideRepo,
                                        this.languageRepo, this.onHolidayRepo, this.programRepo, this.placeRepo, this.pltconRepo, this.prtconRepo);
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.win.MainFrame.Content = new LoginPage(this.userRepo, this.win);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
