using HappyTourManager.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyTourManager.Model
{
    class CustomerHandler : ICrudFunctions
    {
        HappyDataBaseEntities1 database;

        public CustomerHandler()
        {
            database = new HappyDataBaseEntities1();
        }

        public void CreateEntry(object obj)
        {
            // var Customer = new Customer()
            // {
            var Person = new Person()
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
            database.People.Add(Person);
            database.SaveChanges();
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
            
            var person = database.People.Single(e => e.PersonID == 2);
            person.FirstName = "Kankalin";
            Console.WriteLine(person.FirstName);
            database.SaveChanges();
            Person persn = database.People.Single(e => e.PersonID == 2);
            Console.WriteLine(persn.FirstName);
            return persn;
        }
    }
}
