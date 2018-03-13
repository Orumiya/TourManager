// <copyright file="CustomerHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HappyTourManager.Interface;

    class CustomerHandler : ICrudFunctions
    {
        private HappyDataBaseEntities1 database;

        public CustomerHandler()
        {
            this.database = new HappyDataBaseEntities1();
        }

        public void CreateEntry(object obj)
        {
            // var Customer = new Customer()
            // {
            var person = new Person()
            {
                    PersonID = 6,
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

               // },

                // LoyaltyCard = "Y"
            };
            this.database.People.Add(person);
            this.database.SaveChanges();
        }

        public void DeleteEntry(object obj)
        {
            throw new NotImplementedException();
        }

        public void EditEntry(object obj)
        {
            throw new NotImplementedException();
        }

        public List<object> ListAllEntry()
        {
            throw new NotImplementedException();
        }

        public object SearchEntry(object obj)
        {
            var person = this.database.People.Single(e => e.PersonID == 2);
            person.FirstName = "Kankalin";
            Console.WriteLine(person.FirstName);
            this.database.SaveChanges();
            Person persn = this.database.People.Single(e => e.PersonID == 2);
            Console.WriteLine(persn.FirstName);
            return persn;
        }
    }
}
