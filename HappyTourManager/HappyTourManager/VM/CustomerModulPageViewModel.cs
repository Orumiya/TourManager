// <copyright file="CustomerModulPageViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager.VM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HappyTourManager.Model;

    class CustomerModulPageViewModel
    {
        public CustomerModulPageViewModel()
        {
            this.Handler = new CustomerHandler();

            // dummy customer
            this.Customer = new Customer()
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

        public Customer Customer { get; set; }

        public CustomerHandler Handler { get; set; }

        public void Save()
        {
            this.Handler.CreateEntry(this.Customer);
        }

        public void Search()
        {
            this.Handler.SearchEntry(this.Customer);
        }

        public void ListAll()
        {
            this.Handler.ListAllEntry();
        }

        public void Delete()
        {
            this.Handler.DeleteEntry(this.Customer);
        }

        public void NewCustomer()
        {
            this.Handler.CreateEntry(this.Customer);
        }
    }
 }