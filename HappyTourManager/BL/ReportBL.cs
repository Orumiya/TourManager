// <copyright file="ReportBL.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BL.Interfaces;
    using DATA;
    using DATA.Interfaces;

    /// <summary>
    /// searchterms for Report entities
    /// </summary>
    public enum ReportTerms
    {
        REPORTDATE,
        REPORTTYPE,
        DEFAULT
    }

    /// <summary>
    /// defines the type of reports which can be generated
    /// </summary>
    public enum ReportTypes
    {
        TOURREPORT,
        GUIDEREPORT,
        CUSTOMERREPORT,
        ORDERREPORT,
        HOLIDAYREPORT
    }

    public class ReportBL : ISearcheable<Report>, IReportList
    {
        private readonly IRepository<Report> reportRepository;
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<Customer> customerRepository;
        private readonly IRepository<Tour> tourRepository;
        private readonly IRepository<Tourguide> tourguideRepository;
        private readonly IRepository<Language> languageRepository;
        private readonly IRepository<OnHoliday> onHolidayRepository;
        private readonly IRepository<Program> programRepository;
        private readonly IRepository<Place> placeRepository;
        private readonly IRepository<PLTCON> pltconRepository;
        private readonly IRepository<PRTCON> prtconRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportBL"/> class.
        /// </summary>
        /// <param name="reportRepository">reporitory of report</param>
        /// <param name="orderRepository">reporitory of </param>
        /// <param name="customerRepository">reporitory of customers</param>
        /// <param name="tourRepository">reporitory of tours</param>
        /// <param name="tourguideRepository">reporitory of tourguides</param>
        /// <param name="languageRepository">reporitory of languages</param>
        /// <param name="onHolidayRepository">reporitory of holidays</param>
        /// <param name="programRepository">reporitory of programs</param>
        /// <param name="placeRepository">reporitory of places</param>
        /// <param name="pltconRepository">reporitory of pltcons</param>
        /// <param name="prtconRepository">reporitory of prtcons</param>
        public ReportBL(
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
        }

        /// <inheritdoc />
        public event EventHandler ReportListChanged;

        /// <inheritdoc />
        public void Delete(Report report)
        {
            try
            {
                this.reportRepository.Delete(report);
            }
            finally
            {
                this.OnReportListChanged();
            }
        }

        /// <inheritdoc />
        public void Save(Report report)
        {
            try
            {
                this.reportRepository.Create(report);
            }
            finally
            {
                this.OnReportListChanged();
            }
        }

        /// <inheritdoc />
        public IList<Report> Search(object searchterm, object searchvalue)
        {
            var reports = this.reportRepository.GetAll();

            // searching for reports which are created between 2 dates
            // searchvalue must be a DateTime[]
            if ((ReportTerms)searchterm == ReportTerms.REPORTDATE)
            {
                DateTime[] interval = (DateTime[])searchvalue;
                DateTime startInterval = interval[0];
                DateTime endInterval = interval[1];
                reports = reports.Where(
                    i => (i.ReportDate <= endInterval) && (startInterval <= i.ReportDate));
                return reports.ToList();
            }

            // returns the reports of the searched type
            // searchvalue must be a string
            else if ((ReportTerms)searchterm == ReportTerms.REPORTTYPE)
            {
                string reporttype = (string)searchvalue;
                reports = reports.Where(e => e.ReportType.Equals(reporttype));
                return reports.ToList<Report>();
            }

            // searches for all Reports
            else if ((ReportTerms)searchterm == ReportTerms.DEFAULT)
            {
                return reports.ToList<Report>();
            }
            else
            {
                throw new InvalidOperationException("Not found");
            }
        }

        /// <inheritdoc />
        public void ThrowIfExists(Report report)
        {
            this.reportRepository.ThrowIfExists(report);
        }

        /// <inheritdoc />
        public void Update()
        {
            this.reportRepository.Update();
        }

        /// <summary>
        /// notifies the outside about any collection change manually
        /// </summary>
        private void OnReportListChanged()
        {
            this.ReportListChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
