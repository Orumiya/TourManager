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
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Save(Report report)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IList<Report> Search(object searchterm, object searchvalue)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void ThrowIfExists(Report report)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
