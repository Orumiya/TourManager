// <copyright file="TourguideBLTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TEST
{
    using BL;
    using DATA;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TEST.Fakes;

    [TestFixture]
    public class ReportBLTest
    {
        private Customer[] customers;
        private Report[] reports;
        private Order[] orders;
        private Tour[] tours;
        private Tourguide[] guides;
        private Language[] languages;
        private OnHoliday[] onholidays;
        private Program[] programs;
        private Place[] places;
        private PLTCON[] pltcons;
        private PRTCON[] prtcons;
        private ReportBL bl;
        private FakeRepository<Customer> customerRepository;
        private FakeRepository<Report> reportRepository;
        private FakeRepository<Order> orderRepository;
        private FakeRepository<Tour> tourRepository;
        private FakeRepository<Tourguide> tourguideRepository;
        private FakeRepository<Language> languageRepository;
        private FakeRepository<OnHoliday> onHolidayRepository;
        private FakeRepository<Program> programRepository;
        private FakeRepository<Place> placeRepository;
        private FakeRepository<PLTCON> pltconRepository;
        private FakeRepository<PRTCON> prtconRepository;

        public ReportBLTest()
        {
            this.CreateTestdataArrays();
            this.customerRepository = new FakeRepository<Customer>(customers);
            this.reportRepository = new FakeRepository<Report>(reports);
            this.orderRepository = new FakeRepository<Order> (orders);
            this.tourRepository = new FakeRepository<Tour>(tours);
            this.tourguideRepository = new FakeRepository <Tourguide>(guides);
            this.languageRepository = new FakeRepository<Language>(languages);
            this.onHolidayRepository = new FakeRepository<OnHoliday>(onholidays);
            this.programRepository = new FakeRepository<Program>(programs);
            this.placeRepository = new FakeRepository<Place>(places);
            this.pltconRepository = new FakeRepository<PLTCON>(pltcons);
            this.prtconRepository = new FakeRepository<PRTCON>(prtcons);
            this.bl = new ReportBL(reportRepository, orderRepository, customerRepository, tourRepository, tourguideRepository, languageRepository, onHolidayRepository, programRepository, placeRepository, pltconRepository, prtconRepository);
        }

        private void CreateTestdataArrays()
        {
            customers = new[]
            {
                new Customer()
                {
                LoyaltyCard = "1",
                Person = new Person()
                    {
                        FirstName = "Marcus",
                        LastName = "Aurelius",
                        BirthDate = new DateTime(1978, 10, 2),
                        IDNumber = 231234567,
                        IDType = "identity card",
                        AddressCity = "Miskolc",
                        AddressCountry = "Hungary",
                        AddressFree = "Kossuth u 34",
                        AddressZip = "3200",
                        Phone = 063012312312,
                        ValidTo = new DateTime(2025, 02, 02)
                    }
                },
                new Customer()
                {
                    LoyaltyCard = "0",
                    Person = new Person()
                    {
                        FirstName = "Vanessa",
                        LastName = "Williams",
                        BirthDate = new DateTime(1978, 10, 2),
                        IDNumber = 999234567,
                        IDType = "identity card",
                        AddressCity = "Budapest",
                        AddressCountry = "Hungary",
                        AddressFree = "Kossuth u 34",
                        AddressZip = "3200",
                        Phone = 063012312312,
                        ValidTo = new DateTime(2018, 09, 02)
                    }
                },
                new Customer()
                {
                LoyaltyCard = "1",
                Person = new Person()
                    {
                        FirstName = "Hermina",
                        LastName = "Aurelius",
                        BirthDate = new DateTime(1978, 10, 2),
                        IDNumber = 239994567,
                        IDType = "identity card",
                        AddressCity = "Miskolc",
                        AddressCountry = "Hungary",
                        AddressFree = "Kossuth u 34",
                        AddressZip = "3200",
                        Phone = 063099912312,
                        ValidTo = new DateTime(2021, 02, 02)
                    }
                },
                new Customer()
                {
                LoyaltyCard = "1",
                Person = new Person()
                    {
                        FirstName = "Herkules",
                        LastName = "Aurelius",
                        BirthDate = new DateTime(1978, 10, 2),
                        IDNumber = 239994522,
                        IDType = "identity card",
                        AddressCity = "Miskolc",
                        AddressCountry = "Hungary",
                        AddressFree = "Kossuth u 34",
                        AddressZip = "3200",
                        Phone = 063099912399,
                        ValidTo = new DateTime(2021, 02, 02)
                    }
                },
                new Customer()
                {
                LoyaltyCard = "0",
                Person = new Person()
                    {
                        FirstName = "Töhötöm",
                        LastName = "Aurelius",
                        BirthDate = new DateTime(1978, 10, 2),
                        IDNumber = 239997777,
                        IDType = "identity card",
                        AddressCity = "Miskolc",
                        AddressCountry = "Hungary",
                        AddressFree = "Kossuth u 34",
                        AddressZip = "3200",
                        Phone = 063022912312,
                        ValidTo = new DateTime(2019, 02, 02)
                    }
                }
            };

            tours = new[]
            {
                new Tour{
                    TourID = 1,
                    TravelName = "wonderful east",
                    Description = "loremipsum",
                    AdultPrice = 121000,
                    ChildPrice = 0,
                    MinNumber = 3,
                    MaxNumber = 6,
                    StartDate = new DateTime(2018,09,10),
                    EndDate = new DateTime(2018,9,23),
                    Transport = "bus"
                },
                new Tour{
                    TourID = 2,
                    TravelName = "french travel",
                    Description = "loremipsum",
                    AdultPrice = 89000,
                    ChildPrice = 0,
                    MinNumber = 6,
                    MaxNumber = 10,
                    StartDate = new DateTime(2018,06,30),
                    EndDate = new DateTime(2018,07,10),
                    Transport = "bus"
                }
            };

            orders = new[]
            {
                new Order {
                    OrderID = 1,
                    OrderDate = new DateTime(2018,04,28),
                    CustomerID = 1,
                    TourID = 1,
                    TotalSum = 0,
                    PersonCount = 2,
                    IsCancelled = "0",
                    IsPayed = "1",
                    IsLoyalty = "1"
                },
                new Order
                {
                    OrderID = 2,
                    OrderDate = new DateTime(2018,03,28),
                    CustomerID = 2,
                    TourID = 2,
                    TotalSum = 0,
                    PersonCount = 5,
                    IsCancelled = "0",
                    IsPayed = "0",
                    IsLoyalty = "0"
                },
                new Order
                {
                    OrderID = 3,
                    OrderDate = new DateTime(2018,02,28),
                    CustomerID = 1,
                    TourID = 2,
                    TotalSum = 0,
                    PersonCount = 5,
                    IsCancelled = "1",
                    IsPayed = "0",
                    IsLoyalty = "1"
                },
                new Order
                {
                    OrderID = 4,
                    OrderDate = new DateTime(2018,03,05),
                    CustomerID = 1,
                    TourID = 2,
                    TotalSum = 0,
                    PersonCount = 4,
                    IsCancelled = "0",
                    IsPayed = "1",
                    IsLoyalty = "1"
                }
            };

            this.ConnectCustomersAndOrders(orders[0], customers[0]);
            this.ConnectCustomersAndOrders(orders[1], customers[1]);
            this.ConnectCustomersAndOrders(orders[2], customers[0]);
            this.ConnectCustomersAndOrders(orders[3], customers[0]);
            this.ConnectToursAndOrders(orders[0], tours[0]);
            this.ConnectToursAndOrders(orders[1], tours[1]);
            this.ConnectToursAndOrders(orders[2], tours[1]);
            this.ConnectToursAndOrders(orders[3], tours[1]);
        }

        private void ConnectToursAndOrders(Order order, Tour tour)
        {
            tour.Orders.Add(order);
            order.Tour = tour;
        }

        private void ConnectCustomersAndOrders(Order order, Customer customer)
        {
            customer.Orders.Add(order);
            order.Customer = customer;
        }

        [Test]
        public void WhenCreatingReportBL_ThenReportBLIsNotNull()
        {
            // ARRANGE - ACT
            //arranged in constructor
            // ASSERT
            Assert.That(bl, Is.Not.Null);
        }

        [Test]
        public void WhenCollectOrderInfoForReport_ThenInfoIsAvailable()
        {
            //Tuple<int, int, int> OrderReportResult;
            //ACT
            Tuple<int, int, int, IList<Order>, IList<Order>, IList<Order>> result = bl.CollectOrderInfoForReport();
            // payed, cancelled, pending
            Assert.That(result.Item1,Is.EqualTo(2));
            Assert.That(result.Item2, Is.EqualTo(1));
            Assert.That(result.Item2, Is.EqualTo(1));
        }

        [Test]
        public void WhenCollectCustomerInfoForReport_ThenInfoIsAvailable()
        {
            // ASSERT
            Tuple<int, int, IList<Customer>, IList<Customer>> result = bl.CollectCustomerInfo();
            //ACT

            // CustomersWithLoyalty
            Assert.That(result.Item1, Is.EqualTo(3));
            // CustomersWithoutLoyalty
            Assert.That(result.Item2, Is.EqualTo(2));
        }

        [Test]
        public void WhenCollectTourAndOrderInfoForReport_ThenInfoIsAvailable()
        {
            Dictionary<Tour, decimal> dictionary = bl.CollectTourAndOrderInfo();
            Assert.That(dictionary[tours[1]],Is.EqualTo((decimal)9));

        }
    }
}
