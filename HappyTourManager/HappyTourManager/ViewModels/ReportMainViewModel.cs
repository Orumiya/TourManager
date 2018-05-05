// <copyright file="App.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using BL;
    using DATA;
    using DATA.Interfaces;
    using System;
    using System.Collections.Generic;

    class ReportMainViewModel : Bindable
    {
        private ReportBL reportBL;
        private List<string> searchCategories;
        private string selectedType;
        private List<string> typeCategories;
        private int point1;
        private int point2;
        private int point3;

        /// <summary>
        /// contains selected report type
        /// </summary>
        public string SelectedType
        {
            get
            {
                return selectedType;
            }

            set
            {
                selectedType = value;
                this.OnPropertyChanged(nameof(SelectedType));
            }
        }

        /// <summary>
        /// Listof selectable report types
        /// </summary>
        public List<string> TypeCategories
        {
            get
            {
                return typeCategories;
            }

            set
            {
                typeCategories = value;
                this.OnPropertyChanged(nameof(TypeCategories));
            }
        }

        /// <summary>
        /// report value 1
        /// </summary>
        public int Point1
        {
            get
            {
                return point1;
            }

            set
            {
                point1 = value;
                this.OnPropertyChanged(nameof(Point1));
            }
        }

        /// <summary>
        /// report value 2
        /// </summary>
        public int Point2
        {
            get
            {
                return point2;
            }

            set
            {
                point2 = value;
                this.OnPropertyChanged(nameof(Point2));
            }
        }

        /// <summary>
        /// report value 3
        /// </summary>
        public int Point3
        {
            get
            {
                return point3;
            }

            set
            {
                point3 = value;
                this.OnPropertyChanged(nameof(Point3));
            }
        }

        /// <summary>
        /// constructor for Report viewmodel
        /// </summary>
        /// <param name="reportRepository"></param>
        /// <param name="orderRepository"></param>
        /// <param name="customerRepository"></param>
        /// <param name="tourRepository"></param>
        /// <param name="tourguideRepository"></param>
        /// <param name="languageRepository"></param>
        /// <param name="onHolidayRepository"></param>
        /// <param name="programRepository"></param>
        /// <param name="placeRepository"></param>
        /// <param name="pltconRepository"></param>
        /// <param name="prtconRepository"></param>
        public ReportMainViewModel(
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
            IRepository<PRTCON> prtconRepository
            )
        {
            this.reportBL = new ReportBL(reportRepository, orderRepository, customerRepository, tourRepository,
                                    tourguideRepository, languageRepository, onHolidayRepository, programRepository, 
                                            placeRepository, pltconRepository, prtconRepository);

            this.searchCategories = new List<string>();
            foreach (ReportTerms item in Enum.GetValues(typeof(ReportTerms)))
            {
                this.searchCategories.Add(item.ToString());
            }

            this.typeCategories = new List<string>();
            foreach (ReportTypes item in Enum.GetValues(typeof(ReportTypes)))
            {
                this.typeCategories.Add(item.ToString());
            }
        }


        /// <summary>
        /// Generate report
        /// </summary>
        public void GenerateReport()
        {
            
            this.reportBL.GenerateNewReport((ReportTypes)Enum.Parse(typeof(ReportTypes), this.SelectedType));
            if (this.SelectedType == "CUSTOMERREPORT")
            {
                Point1 = reportBL.CustomerReportResult.Item1;
                Point2 = reportBL.CustomerReportResult.Item2;
            }
            else if (this.SelectedType == "ORDERREPORT")
            {
                Point1 = reportBL.OrderReportResult.Item1;
                Point2 = reportBL.OrderReportResult.Item2;
                Point3 = reportBL.OrderReportResult.Item3;
            }

            

        }
    }
}
