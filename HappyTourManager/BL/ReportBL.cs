// <copyright file="ReportBL.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
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
        CUSTOMERREPORT,
        ORDERREPORT
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
        private Tuple<int, int> customerReportResult;
        private Tuple<int, int, int> orderReportResult;
        private Dictionary<Tour, decimal> tourReportResult;

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

        /// <summary>
        /// Gets or sets info for the Customer chart
        /// </summary>
        public Tuple<int, int> CustomerReportResult
        {
            get { return this.customerReportResult; }
            set { this.customerReportResult = value; }
        }

        /// <summary>
        /// Gets or sets info for the Order chart:
        /// item1 = payed
        /// item2 = cancelled
        /// item3 = pending
        /// </summary>
        public Tuple<int, int, int> OrderReportResult
        {
            get { return this.orderReportResult; }
            set { this.orderReportResult = value; }
        }

       /// <summary>
       /// Gets or sets info for the Tour chart
       /// </summary>
        public Dictionary<Tour, decimal> TourReportResult
        {
            get { return this.tourReportResult; }
            set { this.tourReportResult = value; }
        }

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
            if ((ReportTerms)Enum.Parse(typeof(ReportTerms), (string)searchterm) == ReportTerms.REPORTDATE)
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
            else if ((ReportTerms)Enum.Parse(typeof(ReportTerms), (string)searchterm) == ReportTerms.REPORTTYPE)
            {
                string reporttype = (string)searchvalue;
                reports = reports.Where(e => e.ReportType.Equals(reporttype));
                return reports.ToList<Report>();
            }

            // searches for all Reports
            else if ((ReportTerms)Enum.Parse(typeof(ReportTerms), (string)searchterm) == ReportTerms.DEFAULT)
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
        /// start the generator methods depending of selected report type
        /// </summary>
        /// <param name="type">report type</param>
        public void GenerateNewReport(ReportTypes type)
        {
            if (type.Equals(ReportTypes.CUSTOMERREPORT))
            {
                this.GenerateCustomerReport();
            }
            else if (type.Equals(ReportTypes.ORDERREPORT))
            {
                this.GenerateOrderReport();
            }
            else
            {
                throw new InvalidOperationException("unknown reporting type");
            }
        }

        /// <summary>
        /// collects the order info for the generator method
        /// </summary>
        /// <returns>a tuple of info</returns>
        public Tuple<int, int, int, IList<Order>, IList<Order>, IList<Order>> CollectOrderInfoForReport()
        {
            var orders = this.orderRepository.GetAll();
            var payedOrders = orders.Where(e => e.IsPayed.Equals("1"));
            IList<Order> payedOrdersList = payedOrders.ToList();
            int payedOrderCount = payedOrdersList.Count();
            var cancelledOrders = orders.Where(e => e.IsCancelled.Equals("1"));
            IList<Order> cancelledOrdersList = cancelledOrders.ToList();
            int cancelledOrderCount = cancelledOrdersList.Count();
            var pendingOrders = orders.Where(e => !e.IsPayed.Equals("1") && !e.IsCancelled.Equals("1"));
            IList<Order> pendingOrdersList = pendingOrders.ToList();
            int pendingOrderCount = pendingOrdersList.Count();
            Tuple<int, int, int, IList<Order>, IList<Order>, IList<Order>> result =
                new Tuple<int, int, int, IList<Order>, IList<Order>, IList<Order>>(payedOrderCount, cancelledOrderCount, pendingOrderCount, payedOrdersList, cancelledOrdersList, pendingOrdersList);
            return result;
        }

        /// <summary>
        /// Collects the customer info for the generator method
        /// </summary>
        /// <returns>Tuple of info</returns>
        public Tuple<int, int, IList<Customer>, IList<Customer>> CollectCustomerInfo()
        {
            var customers = this.customerRepository.GetAll();
            var customersWithLoyalty = customers.Where(e => e.LoyaltyCard.Equals("1"));
            IList<Customer> customersWithLoyaltyList = customersWithLoyalty.ToList();
            int customersWithLoyaltyCount = customersWithLoyaltyList.Count();
            var customersWithoutLoyalty = customers.Where(e => e.LoyaltyCard.Equals("0") || e.LoyaltyCard.Equals(null));
            IList<Customer> customersWithoutLoyaltyList = customersWithoutLoyalty.ToList();
            int customersWithoutLoyaltyCount = customersWithoutLoyaltyList.Count();

            Tuple<int, int, IList<Customer>, IList<Customer>> result = new Tuple<int, int, IList<Customer>, IList<Customer>>(customersWithLoyaltyCount, customersWithoutLoyaltyCount, customersWithLoyaltyList, customersWithoutLoyaltyList);
            return result;
        }

        /// <summary>
        /// Collects the tour and order info for the generator method
        /// </summary>
        /// <returns>Dictionary of info</returns>
        public Dictionary<Tour, decimal> CollectTourAndOrderInfo()
        {
            var orders = this.orderRepository.GetAll();
            IList<Order> allOrders = orders.Where(e => (!e.IsCancelled.Equals("1"))).ToList();
            var tours = this.tourRepository.GetAll();
            IList<Tour> allTours = tours.ToList();
            Dictionary<Tour, decimal> dictionary = new Dictionary<Tour, decimal>();
            foreach (Tour item in allTours)
            {
                decimal value = item.Orders.Where(e => (!e.IsCancelled.Equals("N"))).Sum(i => i.PersonCount);
                dictionary.Add(item, value);
            }

            if (dictionary.Count != 0 || dictionary != null)
            {
                return dictionary;
            }
            else
            {
                throw new InvalidOperationException("No data");
            }
        }

        /// <summary>
        /// creates a Tour report (each tour how much order has)
        /// </summary>
        private void GenerateTourReport()
        {
            this.TourReportResult = this.CollectTourAndOrderInfo();
        }

        /// <summary>
        /// generates a CustomerXMLreport and updates the OrderReportResult for the chart
        /// </summary>
        private void GenerateCustomerReport()
        {
            // pie or doughnut chart needed
            Tuple<int, int, IList<Customer>, IList<Customer>> info = this.CollectCustomerInfo();

            this.CreateXMLCustomerReport(info.Item3, info.Item4);
            this.CustomerReportResult = new Tuple<int, int>(info.Item1, info.Item2);
        }

        /// <summary>
        /// creates a CustomerXML
        /// </summary>
        /// <param name="customersWithLoyalty">input list of customersWithLoyalty</param>
        /// <param name="customersWithoutLoyalty">input list of customersWithoutLoyalty</param>
        private void CreateXMLCustomerReport(IList<Customer> customersWithLoyalty, IList<Customer> customersWithoutLoyalty)
        {
            XDocument customerReport = new XDocument(
#pragma warning disable SA1118 // Parameter must not span multiple lines
            new XDeclaration("1.0", "utf-8", "yes"),
            new XElement(
                "CustomerReport",
                from custL in customersWithLoyalty
                select
                new XElement(
                    "CustomersWithLoyaltyCard",
                new XAttribute(
                    "ID", custL.PersonID),
                new XElement(
                    "LastName", custL.Person.LastName),
                new XElement(
                    "FirstName", custL.Person.FirstName),
                new XElement(
                    "AddressCity", custL.Person.AddressCity),
                new XElement(
                    "BirthDate", custL.Person.BirthDate)),
                from custN in customersWithoutLoyalty
                select
                new XElement(
                    "CustomersWithoutLoyaltyCard",
                new XAttribute(
                    "ID", custN.PersonID),
                new XElement(
                    "LastName", custN.Person.LastName),
                new XElement(
                    "FirstName", custN.Person.FirstName),
                new XElement(
                    "AddressCity", custN.Person.AddressCity),
                new XElement(
                    "BirthDate", custN.Person.BirthDate))));
#pragma warning restore SA1118 // Parameter must not span multiple lines

            DateTime generateTime = DateTime.Now;
            string filename = "CustomerReport_" + generateTime.Year.ToString() + generateTime.Month.ToString() + generateTime.Day.ToString() + "_" + generateTime.Hour.ToString() + generateTime.Minute.ToString() + ".xml";
            customerReport.Save(filename);
        }

        /// <summary>
        /// Generates the Order report and updates the OrderReportResult for the chart
        /// </summary>
        private void GenerateOrderReport()
        {
            // basic column chart needed
            Tuple<int, int, int, IList<Order>, IList<Order>, IList<Order>> info = this.CollectOrderInfoForReport();

            this.CreateXMLOrderReport(info.Item4, info.Item5, info.Item6);
            this.OrderReportResult = new Tuple<int, int, int>(info.Item1, info.Item2, info.Item3);
        }

        /// <summary>
        /// generates an Order XML
        /// </summary>
        /// <param name="payedOrders">list of payedOrders</param>
        /// <param name="cancelledOrders">list of cancelledOrders</param>
        /// <param name="pendingOrders">list of pendingOrders</param>
        private void CreateXMLOrderReport(IList<Order> payedOrders, IList<Order> cancelledOrders, IList<Order> pendingOrders)
        {
            XDocument orderReport = new XDocument(
#pragma warning disable SA1118 // Parameter must not span multiple lines
            new XDeclaration("1.0", "utf-8", "yes"),
            new XElement(
                "OrderReport",
                from payed in payedOrders
                select
                new XElement(
                    "PayedOrders",
                new XAttribute(
                    "ID", payed.OrderID),
                new XElement(
                    "OrderDate", payed.OrderDate),
                new XElement(
                    "Customer", payed.Customer.Person.FirstName + " " + payed.Customer.Person.LastName),
                new XElement(
                    "NumberOfPerson", payed.PersonCount),
                new XElement(
                    "TravelName", payed.Tour.TravelName),
                new XElement(
                    "TotalSum", payed.TotalSum)),
                from cancelled in cancelledOrders
                select
                new XElement(
                    "CancelledOrsers",
                new XAttribute(
                    "ID", cancelled.OrderID),
                new XElement(
                    "OrderDate", cancelled.OrderDate),
                new XElement(
                    "Customer", cancelled.Customer.Person.FirstName + " " + cancelled.Customer.Person.LastName),
                new XElement(
                    "NumberOfPerson", cancelled.PersonCount),
                new XElement(
                    "TravelName", cancelled.Tour.TravelName),
                new XElement(
                    "TotalSum", cancelled.TotalSum)),
                from pending in pendingOrders
                select
                new XElement(
                    "PendingOrders",
                new XAttribute(
                    "ID", pending.OrderID),
                new XElement(
                    "OrderDate", pending.OrderDate),
                new XElement(
                    "Customer", pending.Customer.Person.FirstName + " " + pending.Customer.Person.LastName),
                new XElement(
                    "NumberOfPerson", pending.PersonCount),
                new XElement(
                    "TravelName", pending.Tour.TravelName),
                new XElement(
                    "TotalSum", pending.TotalSum))));
#pragma warning restore SA1118 // Parameter must not span multiple lines
            DateTime generateTime = DateTime.Now;
            string filename = "OrderReport_" + generateTime.Year.ToString() + generateTime.Month.ToString() + generateTime.Day.ToString() + "_" + generateTime.Hour.ToString() + generateTime.Minute.ToString() + ".xml";
            orderReport.Save(filename);
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
