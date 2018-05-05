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
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class ReportMainViewModel : Bindable
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

        private ReportBL reportBL;

        private string selectedCtegory = "DEFAULT";
        private string selectedValue;
        private DateTime selectedDateFrom = DateTime.Today;
        private DateTime selectedDateTo = DateTime.Today;
        private ObservableCollection<Report> resultList;
        private Report selectedReport;
        private List<string> searchCategories;
        private string selectedType;
        private List<string> typeCategories;
        private int point1;
        private int point2;
        private int point3;


        public string SelectedCtegory
        {
            get
            {
                return selectedCtegory;
            }

            set
            {
                selectedCtegory = value;
                this.OnPropertyChanged(nameof(SelectedCtegory));
            }
        }

        public string SelectedValue
        {
            get
            {
                return selectedValue;
            }

            set
            {
                selectedValue = value;
                this.OnPropertyChanged(nameof(SelectedValue));
            }
        }

        public DateTime SelectedDateFrom
        {
            get
            {
                return selectedDateFrom;
            }

            set
            {
                selectedDateFrom = value;
                this.OnPropertyChanged(nameof(SelectedDateFrom));
            }
        }

        public DateTime SelectedDateTo
        {
            get
            {
                return selectedDateTo;
            }

            set
            {
                selectedDateTo = value;
                this.OnPropertyChanged(nameof(SelectedDateTo));
            }
        }

        public ObservableCollection<Report> ResultList
        {
            get
            {
                return resultList;
            }

            set
            {
                resultList = value;
                this.OnPropertyChanged(nameof(ResultList));
            }
        }

        public Report SelectedReport
        {
            get
            {
                return selectedReport;
            }

            set
            {
                selectedReport = value;
                this.OnPropertyChanged(nameof(SelectedReport));
            }
        }

        public List<string> SearchCategories
        {
            get
            {
                return searchCategories;
            }

            set
            {
                searchCategories = value;
                this.OnPropertyChanged(nameof(SearchCategories));
            }
        }

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
        /// Get the search result list
        /// </summary>
        public void GetSearchResult()
        {
            IList<Report> rL;
            if (this.SelectedCtegory == "REPORTDATE")
            {
                DateTime[] dt = new DateTime[2];
                dt[0] = this.SelectedDateFrom;
                dt[1] = this.SelectedDateTo;

                rL = this.reportBL.Search(Enum.Parse(typeof(ReportTerms), this.SelectedCtegory), dt);
            }
            else
            {
                rL = this.reportBL.Search(Enum.Parse(typeof(ReportTerms), this.SelectedCtegory), this.SelectedValue);
            }
            this.ResultList = new ObservableCollection<Report>(rL);
        }

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
