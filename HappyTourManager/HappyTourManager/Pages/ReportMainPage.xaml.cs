// <copyright file="ReportMainPage.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager.Pages
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using DATA;
    using DATA.Interfaces;

    /// <summary>
    /// Interaction logic for ReportMainPage.xaml
    /// </summary>
    public partial class ReportMainPage : Page
    {
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

        private ReportMainViewModel reportVM;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportMainPage"/> class.
        /// report page
        /// </summary>
        /// <param name="reportRepository">report</param>
        /// <param name="orderRepository">order</param>
        /// <param name="customerRepository">custmer</param>
        /// <param name="tourRepository">tour</param>
        /// <param name="tourguideRepository">tourguide</param>
        /// <param name="languageRepository">language</param>
        /// <param name="onHolidayRepository">onholiday</param>
        /// <param name="programRepository">program</param>
        /// <param name="placeRepository">place</param>
        /// <param name="pltconRepository">plt</param>
        /// <param name="prtconRepository">prt</param>
        public ReportMainPage(
            IRepository<Report> reportRepository,
            IRepository<Order> orderRepository,
            IRepository<Customer> customerRepository,
            IRepository<Tour> tourRepository,
            IRepository<Tourguide> tourguideRepository,
            IRepository<Language> languageRepository,
            IRepository<OnHoliday> onHolidayRepository,
            IRepository<Program> programRepository,
            IRepository<Place> placeRepository,
            IRepository<PLTCON> pltconRepository,
            IRepository<PRTCON> prtconRepository)
        {
            this.reportRepository = reportRepository;
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.tourguideRepository = tourguideRepository;
            this.tourRepository = tourRepository;
            this.languageRepository = languageRepository;
            this.onHolidayRepository = onHolidayRepository;
            this.programRepository = programRepository;
            this.placeRepository = placeRepository;
            this.pltconRepository = pltconRepository;
            this.prtconRepository = prtconRepository;

            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.reportVM = new ReportMainViewModel(
                this.reportRepository,
                this.orderRepository,
                this.customerRepository,
                this.tourRepository,
                this.tourguideRepository,
                this.languageRepository,
                this.onHolidayRepository,
                this.programRepository,
                this.placeRepository,
                this.pltconRepository,
                this.prtconRepository);
            this.DataContext = this.reportVM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.reportVM.SelectedType != null)
            {
                try
                {
                    this.reportVM.GenerateReport();
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Missing data");
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("Wrong data type");
                }
                catch (NotImplementedException)
                {
                    MessageBox.Show("Report is not implemented!");
                }

                if (this.reportVM.SelectedType == "CUSTOMERREPORT")
                {
                    this.contReportDetails.Content = new PieChartUC(this.reportVM.Point1, this.reportVM.Point2);
                }
                else if (this.reportVM.SelectedType == "ORDERREPORT")
                {
                    this.contReportDetails.Content = new OrderReportUC(this.reportVM.Point1, this.reportVM.Point2, this.reportVM.Point3);
                }
                else if (this.reportVM.SelectedType == "TOURREPORT")
                {
                    this.contReportDetails.Content = new TourReport(this.reportVM.TourDatas);
                }
            }
            else
            {
                MessageBox.Show("Please select reporttype!");
            }
        }
    }
}
