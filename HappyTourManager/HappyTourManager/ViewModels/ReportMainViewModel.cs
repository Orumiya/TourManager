// <copyright file="ReportMainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using System;
    using System.Collections.Generic;
    using BL;
    using DATA;
    using DATA.Interfaces;

    /// <summary>
    /// report viewmodel
    /// </summary>
    internal class ReportMainViewModel : Bindable
    {
        private ReportBL reportBL;
        private List<string> searchCategories;
        private string selectedType;
        private List<string> typeCategories;
        private int point1;
        private int point2;
        private int point3;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportMainViewModel"/> class.
        /// constructor for Report viewmodel
        /// </summary>
        /// <param name="reportRepository"> report</param>
        /// <param name="orderRepository">order</param>
        /// <param name="customerRepository">customer</param>
        /// <param name="tourRepository">tour</param>
        /// <param name="tourguideRepository">tourguide</param>
        /// <param name="languageRepository">language</param>
        /// <param name="onHolidayRepository">onHoliday</param>
        /// <param name="programRepository">program</param>
        /// <param name="placeRepository">place</param>
        /// <param name="pltconRepository">pltcon</param>
        /// <param name="prtconRepository">prtcon</param>
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
            IRepository<PRTCON> prtconRepository)
        {
            this.reportBL = new ReportBL(
                reportRepository,
                orderRepository,
                customerRepository,
                tourRepository,
                tourguideRepository,
                languageRepository,
                onHolidayRepository,
                programRepository,
                placeRepository,
                pltconRepository,
                prtconRepository);

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
        /// Gets or sets contains selected report type
        /// </summary>
        public string SelectedType
        {
            get
            {
                return this.selectedType;
            }

            set
            {
                this.selectedType = value;
                this.OnPropertyChanged(nameof(this.SelectedType));
            }
        }

        /// <summary>
        /// Gets or sets listof selectable report types
        /// </summary>
        public List<string> TypeCategories
        {
            get
            {
                return this.typeCategories;
            }

            set
            {
                this.typeCategories = value;
                this.OnPropertyChanged(nameof(this.TypeCategories));
            }
        }

        /// <summary>
        /// Gets or sets report value 1
        /// </summary>
        public int Point1
        {
            get
            {
                return this.point1;
            }

            set
            {
                this.point1 = value;
                this.OnPropertyChanged(nameof(this.Point1));
            }
        }

        /// <summary>
        /// Gets or sets report value 2
        /// </summary>
        public int Point2
        {
            get
            {
                return this.point2;
            }

            set
            {
                this.point2 = value;
                this.OnPropertyChanged(nameof(this.Point2));
            }
        }

        /// <summary>
        /// Gets or sets report value 3
        /// </summary>
        public int Point3
        {
            get
            {
                return this.point3;
            }

            set
            {
                this.point3 = value;
                this.OnPropertyChanged(nameof(this.Point3));
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
                this.Point1 = this.reportBL.CustomerReportResult.Item1;
                this.Point2 = this.reportBL.CustomerReportResult.Item2;
            }
            else if (this.SelectedType == "ORDERREPORT")
            {
                this.Point1 = this.reportBL.OrderReportResult.Item1;
                this.Point2 = this.reportBL.OrderReportResult.Item2;
                this.Point3 = this.reportBL.OrderReportResult.Item3;
            }
        }
    }
}
