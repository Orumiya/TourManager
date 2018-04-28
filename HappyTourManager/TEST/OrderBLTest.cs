using BL;
using DATA;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEST.Fakes;

namespace TEST
{
    [TestFixture]
    class OrderBLTest
    {
        private Tour[] tours;
        private Order[] orders;
        private Customer[] customers;
        private OrderBL bl;
        private FakeRepository<Tour> tourRepository;
        private FakeRepository<Order> orderRepository;
        private FakeRepository<Customer> customerRepository;

        public OrderBLTest()
        {
            CreateTestdataArrays();
            this.tourRepository = new FakeRepository<Tour>(tours);
            this.orderRepository = new FakeRepository<Order>(orders);
            this.customerRepository = new FakeRepository<Customer>(customers);
            this.bl = new OrderBL(orderRepository, customerRepository, tourRepository);
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
                        ValidTo = new DateTime(2020, 02, 02)
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
                    ChildPrice = 89000,
                    MinNumber = 6,
                    MaxNumber = 20,
                    StartDate = new DateTime(2018,09,10),
                    EndDate = new DateTime(2018,9,23),
                    Transport = "bus"
                },
                new Tour{
                    TourID = 2,
                    TravelName = "french travel",
                    Description = "loremipsum",
                    AdultPrice = 89000,
                    ChildPrice = 67000,
                    MinNumber = 10,
                    MaxNumber = 45,
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
                    IsLoyalty = "0"
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
                    IsLoyalty = "1"
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
                }
            };

            ConnectCustomersAndOrders(orders[0], customers[0]);
            ConnectCustomersAndOrders(orders[1], customers[1]);
            ConnectCustomersAndOrders(orders[2], customers[0]);
            ConnectToursAndOrders(orders[0], tours[0]);
            ConnectToursAndOrders(orders[1], tours[1]);
            ConnectToursAndOrders(orders[2], tours[1]);
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
        public void WhenCreatingOrderBL_ThenOrderBLIsNotNull()
        {
            // ARRANGE - ACT
            //arranged in constructor
            // ASSERT
            Assert.That(bl, Is.Not.Null);
        }

        [TestCase(2,10000,3,8000,44000)]
        [TestCase(0, 10000, 3, 8000, 24000)]
        [TestCase(2, 10000, 0, 8000, 20000)]
        public void WhenAdultAndChildCountGiven_ThenCalculatesTheTotalFullPrice(int adultCount, int adultPrice, int childCount, int childPrice, int result)
        {
            //ACT
            int sum = bl.CalculateOrderPriceBeforeLoyaltyCounted(adultCount,adultPrice, childCount, childPrice);

            // ASSERT
            Assert.That(sum, Is.EqualTo(result));
        }

        [TestCase(-1, 10000, 3, 8000)]
        [TestCase(1, 10000, -3, 8000)]
        public void WhenFalseAdultAndChildCountGiven_ThenThrowsAnExceptionAndNotCalculatingPrice(int adultCount, int adultPrice, int childCount, int childPrice)
        {
            // ASSERT
            Assert.That(() => bl.CalculateOrderPriceBeforeLoyaltyCounted(adultCount, adultPrice, childCount, childPrice), Throws.InvalidOperationException);
        }

        [TestCase(10000,true, 9500)]
        [TestCase(10000, false, 10000)]
        public void WhenCalculatingPriceWithLoyalty_ThenGetsADiscountOrNot(int sum, bool isLoyalty, int result)
        {
            int newSum = bl.CalculateOrderPriceWithLoyaltyCounted(sum, isLoyalty);
            Assert.That(newSum, Is.EqualTo(result));
        }

        [Test]
        public void WhenSelectingATour_ThenCountsTheBookedSeatsToThisTourFromOneOrder()
        {
            int count = bl.BookedTourActualPersonCount(tours[0]);

            Assert.That(count, Is.EqualTo(2));
        }

        [Test]
        public void WhenSelectingATour_ThenCountsTheBookedSeatsToThisTourFromMoreOrders()
        {
            int count = bl.BookedTourActualPersonCount(tours[1]);

            Assert.That(count, Is.EqualTo(10));
        }
    }
}
