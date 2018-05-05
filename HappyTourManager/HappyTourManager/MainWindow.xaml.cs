﻿// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
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

        LoginPage loginPage;
        MainPage mainPage;

        HappyTourDatabaseEntities entities;
        IRepository<Customer> customerRepo;
        IRepository<Language> languageRepo;
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
        IRepository<Office> officeRepo;

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
                this.mainPage = new MainPage(this.customerRepo,this.languageRepo,this.onHolidayRepo,this.orderRepo,this.placeRepo,this.pltconRepo,this.programRepo,this.prtconRepo,this.reportRepo,
                    this.tourguideRepo,this.tourRepo,this.userRepo, this.officeRepo,this);
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

        bool dispose = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (dispose)
            {
                if (disposing)
                {
                    if (entities != null) entities.Dispose();
                }
            }

        }
    }
}
