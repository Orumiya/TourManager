namespace DATA.Repositoriees
{
    using System;
    using System.Linq;
    using DATA.Interfaces;

    public enum CustomerTerms
    {
        LoyaltyCard,
        LastName,
        FirstName,
        BirthDate,
        AddressCity,
        IDNumber,
        ValidTo
    }
    public class CustomerRepository : IRepository<Customer>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// creates the repository
        /// </summary>
        /// <param name="entities"></param>
        public CustomerRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }

        /// <summary>
        /// adds a new Customer object to the database
        /// </summary>
        /// <param name="dataobject"></param>
        public void Create(Customer dataobject)
        {
            ThrowIfExists(dataobject);
            entities.People.Add(dataobject.Person);
            entities.Customers.Add(dataobject);
            entities.SaveChanges();
        }

        /// <summary>
        /// removes a Customer from the database
        /// </summary>
        /// <param name="dataobject"></param>
        public void Delete(Customer dataobject)
        {
            entities.Customers.Remove(dataobject);
            entities.SaveChanges();
        }

        /// <summary>
        /// returns all Customers from the database
        /// </summary>
        /// <returns></returns>
        public IQueryable<Customer> GetAll()
        {
            return entities.Customers;
        }

        public IQueryable<Customer> Search(object searchterm, object searchvalue)
        {
            if ((CustomerTerms)searchterm == CustomerTerms.FirstName)
            {
                var customers = entities.Customers.Where(e => e.Person.FirstName.Equals((string)searchvalue));
                return customers;
            }
            else if ((CustomerTerms)searchterm == CustomerTerms.LastName)
            {
                var customers = entities.Customers.Where(e => e.Person.LastName.Equals((string)searchvalue));
                return customers;
            }
            else if ((CustomerTerms)searchterm == CustomerTerms.BirthDate)
            {
                var customers = entities.Customers.Where(e => e.Person.BirthDate.Equals((string)searchvalue));
                return customers;
            }
            else if ((CustomerTerms)searchterm == CustomerTerms.AddressCity)
            {
                var customers = entities.Customers.Where(e => e.Person.AddressCity.Equals((string)searchvalue));
                return customers;
            }
            else if ((CustomerTerms)searchterm == CustomerTerms.IDNumber)
            {
                var customers = entities.Customers.Where(e => e.Person.IDNumber.Equals((string)searchvalue));
                return customers;
            }
            else if ((CustomerTerms)searchterm == CustomerTerms.LoyaltyCard)
            {
                var customers = entities.Customers.Where(e => e.LoyaltyCard.Equals((string)searchvalue));
                return customers;
            }
            else if ((CustomerTerms)searchterm == CustomerTerms.ValidTo)
            {
                var customers = entities.Customers.Where(e => e.Person.ValidTo.Equals((string)searchvalue));
                return customers;
            }
            else
            {
                throw new InvalidOperationException("Not found");
            }
        }

        public void ThrowIfExists(Customer dataobject)
        {
            bool exist = entities.Customers.Any(
                e => e.Person.FirstName.Equals(dataobject.Person.FirstName) &&
                e.Person.LastName.Equals(dataobject.Person.LastName) && 
                e.Person.BirthDate.Equals(dataobject.Person.BirthDate));
            if (exist)
            {
                throw new InvalidOperationException("Already exists!");
            }
        }

        /// <summary>
        /// updates an entry in the database
        /// </summary>
        public void Update()
        {
            entities.SaveChanges();
        }
    }
}
