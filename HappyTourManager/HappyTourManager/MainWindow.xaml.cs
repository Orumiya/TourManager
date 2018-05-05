// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using System;
    using System.Windows;
    using DATA;
    using DATA.Repositories;
    using DATA.Interfaces;
    using Pages;
    using DATA.Repositoriees;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// a főmenü page-t indítja
        /// </summary>
        ///

        private LoginPage loginPage;
        private MainPage mainPage;

        private HappyTourDatabaseEntities entities;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// Contructor of main window
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        public void SetPage(string pagetype)
        {
            if (pagetype == "LoginPage")
            {
                this.loginPage = new LoginPage(this.userRepo, this);
                this.MainFrame.Content = this.loginPage;
            }
            else
            {
                this.mainPage = new MainPage(this.customerRepo, this.languageRepo, this.onHolidayRepo, this.orderRepo, this.placeRepo, this.pltconRepo, this.programRepo, this.prtconRepo, this.reportRepo,
                    this.tourguideRepo, this.tourRepo, this.userRepo, this.officeRepo, this);
                this.MainFrame.Content = this.mainPage;
            }
        }

        private void AppWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.entities = new HappyTourDatabaseEntities();
            this.customerRepo = new CustomerRepository(this.entities);
            this.languageRepo = new LanguageRepository(this.entities);
            this.onHolidayRepo = new OnholidayRepository(this.entities);
            this.orderRepo = new OrderRepository(this.entities);
            this.placeRepo = new PlaceRepository(this.entities);
            this.pltconRepo = new PLTCONRepository(this.entities);
            this.programRepo = new ProgramRepository(this.entities);
            this.prtconRepo = new PRTCONRepository(this.entities);
            this.reportRepo = new ReportRepository(this.entities);
            this.tourguideRepo = new TourguideRepository(this.entities);
            this.tourRepo = new TourRepository(this.entities);
            this.userRepo = new UserRepository(this.entities);
            this.officeRepo = new OfficeRepository(this.entities);

            this.SetPage("LoginPage");
            //this.resource;
            this.DataContext = new WindowViewModel(this);
        }

        private bool dispose = false;

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.dispose)
            {
                if (disposing)
                {
                    if (this.entities != null)
                    {
                        this.entities.Dispose();
                    }
                }
            }
        }
    }
}
