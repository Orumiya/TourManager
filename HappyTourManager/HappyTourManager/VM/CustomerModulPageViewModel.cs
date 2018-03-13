using HappyTourManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyTourManager.VM
{
    class CustomerModulPageViewModel
    {
        public Customer Customer { get; set; }
        public CustomerHandler handler;

        public CustomerModulPageViewModel()
        {
            handler = new CustomerHandler();
            Customer = new Customer()
            {
                Person = new Person()
                {
                    FirstName = "Pamela",
                    LastName = "Parker",
                    BirthDate = new DateTime(1978, 10, 4),
                    Phone = 06201231232,
                    AddressCity = "Budapest",
                    AddressZip = "1222",
                    AddressFree = "Neumann u 12",
                    AddressCountry = "Hungary",
                    IDType = "passport",
                    IDNumber = 234567898,
                    ValidTo = new DateTime(2023, 10, 10)
                },
                LoyaltyCard = "Y"
            };
        }

        public void Save()
        {
            handler.CreateEntry(Customer);
        }

        public void Read()
        {
            handler.SearchEntry(Customer);
        }
    }
 }