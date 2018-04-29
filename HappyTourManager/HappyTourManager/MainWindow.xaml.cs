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
    using DATA.Repositoriees;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HappyTourDatabaseEntities entities;
        private IRepository<Report> reportRepository;
        private IRepository<Order> orderRepository;
        private IRepository<Customer> customerRepository;
        private IRepository<Tour> tourRepository;
        private IRepository<Tourguide> tourguideRepository;
        private IRepository<Language> languageRepository;
        private IRepository<OnHoliday> onHolidayRepository;
        private IRepository<Program> programRepository;
        private IRepository<Place> placeRepository;
        private IRepository<PLTCON> pltconRepository;
        private IRepository<PRTCON> prtconRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// a főmenü page-t indítja
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            //Menu page1 = new Menu(this.mainFrame);
            //this.mainFrame.Content = page1;

            this.DataContext = new WindowViewModel(this);
        }

        private void AppWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.entities = new HappyTourDatabaseEntities();
            this.reportRepository = new ReportRepository(entities);
            this.orderRepository = new OrderRepository(entities);
            this.customerRepository = new CustomerRepository(entities);
            this.tourguideRepository = new TourguideRepository(entities);
            this.tourRepository = new TourRepository(entities);
            this.languageRepository = new LanguageRepository(entities);
            this.onHolidayRepository = new OnholidayRepository(entities);
            this.programRepository = new ProgramRepository(entities);
            this.placeRepository = new PlaceRepository(entities);
            this.pltconRepository = new PLTCONRepository(entities);
            this.prtconRepository = new PRTCONRepository(entities);
        }
    }
}
