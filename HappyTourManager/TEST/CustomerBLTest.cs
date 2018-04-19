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
    class CustomerBLTest
    {
        private Customer[] customers;
        private CustomerBL bl;
        private FakeRepository<Customer> customerRepository;

        public CustomerBLTest()
        {
            CreateTestdataArrays();
            this.customerRepository = new FakeRepository<Customer>(customers);
            bl = new CustomerBL(customerRepository);
        }

        private void CreateTestdataArrays()
        {
            customers = new[]
            {
                new Customer()
                {
                LoyaltyCard = '1',
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
                    LoyaltyCard = '0',
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
        }

        [Test]
        public void WhenCreatingCustomerBL_ThenCustomerBLIsNotNull()
        {
            // ARRANGE - ACT
            //arranged in constructor
            // ASSERT
            Assert.That(bl, Is.Not.Null);
        }

        [Test]
        public void WhenCreatingNewCustomer_ThenCustomerIsSaved()
        {
            // ARRANGE 
            Customer cust = new Customer
            {
                PersonID = 3,
                Person = new Person()
                {
                    FirstName = "Mirabella",
                    LastName = "Nemesis",
                    BirthDate = new DateTime(1997, 10, 02),
                    IDNumber = 333333338,
                    IDType = "identity card",
                    AddressCity = "Wien",
                    AddressCountry = "Austria",
                    AddressFree = "Neumann u 34",
                    AddressZip = "3200",
                    Phone = 063099912333,
                    ValidTo = new DateTime(2024, 02, 02)
                },
                LoyaltyCard = '1',
                Orders = null
            };
            ///ACT
            bl.Save(cust);

            ///ASSERT
            Assert.That(customerRepository.SavedObjects.Count, Is.EqualTo(1));
        }

        [TestCase("Aurelius")]
        [TestCase("auRELIUS")]
        public void WhenSearchingForLastName_ThenWeFindACustomer(string lastName)
        {
            //ARRANGE
            //arranged in  CreateTestdataArrays();
            //ACT
            IList<Customer> list = bl.Search(CustomerTerms.LastName, lastName);
            //ASSERT
            Assert.That(list.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenSearchingForIDNumber_ThenWeFindACustomer()
        {
            //ARRANGE
            //arranged in  CreateTestdataArrays();
            //ACT
            IList<Customer> list = bl.Search(CustomerTerms.IDNumber, 999234567);
            //ASSERT
            Assert.That(list.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenSearchingForDefault_ThenWeGetAllCustomers()
        {
            //ARRANGE
            //arranged in  CreateTestdataArrays();
            //ACT
            IList<Customer> list = bl.Search(CustomerTerms.Default, null);
            //ASSERT
            Assert.That(list.Count, Is.EqualTo(2));
        }

        [TestCase("miskOLC")]
        [TestCase("Miskolc")]
        public void WhenSearchingForAddressCity_ThenWeFindACustomer(string city)
        {
            //ARRANGE
            //arranged in  CreateTestdataArrays();
            //ACT
            IList<Customer> list = bl.Search(CustomerTerms.AddressCity, city);
            //ASSERT
            Assert.That(list.Count, Is.EqualTo(1));
        }

        [TestCase(1,1)]
        public void WhenSearchingForLoyaltyCardHolders_ThenWeFindACustomer(string loyalty)
        {
            //ARRANGE
            //arranged in  CreateTestdataArrays();
            //ACT
            IList<Customer> list = bl.Search(CustomerTerms.LoyaltyCard, 1);
            //ASSERT
            Assert.That(list.Count, Is.EqualTo(1));
        }

        [TestCase(2018, 04, 10, 2018, 05, 10, 0)] //before
        [TestCase(2026, 07, 21, 2026, 08, 09, 0)] //after
        [TestCase(2024, 06, 25, 2024, 07, 05, 1)] //1 after, 1 before
        [TestCase(2018, 04, 16, 2026, 05, 20, 2)] //2 inside
        public void WhenSearchingForValidToDateBetween2Dates_ThenGetCustomerWithValidToPassport(
            int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay, int result)
        {
            //ARRANGE
            //arranged in  CreateTestdataArrays();
            //ACT
            IList<Customer> list = bl.Search(CustomerTerms.ValidTo, new DateTime[] { new DateTime(startYear, startMonth, startDay), new DateTime(endYear, endMonth, endDay) });
            //ASSERT
            Assert.That(list.Count, Is.EqualTo(result));

        }
    }
}
