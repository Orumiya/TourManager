// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using System;
    using System.Collections.Generic;
    using System.IO;
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
    //using HappyTourManager.VM;
    //using Helper;
    using DATA;
    using DATA.Repositories;
    using DATA.Interfaces;
    using Pages;
    using DATA.Repositoriees;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// a főmenü page-t indítja
        /// </summary>
        /// 

        LoginPage loginPage;
        MainPage mainPage;

        HappyTourDatabaseEntities entities;
        CustomerRepository customerRepo;
        LanguageRepository languageRepo;
        OfficeRepository officeRepo;
        OnholidayRepository onHolidayRepo;
        OrderRepository orderRepo;
        PlaceRepository placeRepo;
        PLTCONRepository pltconRepo;
        ProgramRepository programRepo;
        PRTCONRepository prtconRepo;
        ReportRepository reportRepo;
        TourguideRepository tourguideRepo;
        TourRepository tourRepo;
        UserRepository userRepo;




        public MainWindow()
        {
            this.InitializeComponent();
            
        }

        public void SetPage(string pagetype)
        {
            if (pagetype == "LoginPage")
            {
                loginPage = new LoginPage(userRepo, this);
                this.MainFrame.Content = loginPage;
            }
            else
            {
                mainPage = new MainPage(customerRepo,languageRepo,officeRepo,onHolidayRepo,orderRepo,placeRepo,pltconRepo,programRepo,prtconRepo,reportRepo,
                    tourguideRepo,tourRepo,userRepo, this);
                this.MainFrame.Content = mainPage;
            }
        }

        private void AppWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.entities = new HappyTourDatabaseEntities();
            customerRepo = new CustomerRepository(entities);
            languageRepo = new LanguageRepository(entities);
            officeRepo = new OfficeRepository(entities);
            onHolidayRepo = new OnholidayRepository(entities);
            orderRepo = new OrderRepository(entities);
            placeRepo = new PlaceRepository(entities);
            pltconRepo = new PLTCONRepository(entities);
            programRepo = new ProgramRepository(entities);
            prtconRepo = new PRTCONRepository(entities);
            reportRepo = new ReportRepository(entities);
            tourguideRepo = new TourguideRepository(entities);
            tourRepo = new TourRepository(entities);
            userRepo = new UserRepository(entities);

            SetPage("MainPage");

            this.DataContext = new WindowViewModel(this);
        }
    }
}
