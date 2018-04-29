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
            this.reportRepository.Delete(report);
        }

        /// <inheritdoc />
        public void Save(Report report)
        {
            this.reportRepository.Create(report);
        }

        /// <inheritdoc />
        public IList<Report> Search(object searchterm, object searchvalue)
        {
            throw new NotImplementedException();
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
    }
}
