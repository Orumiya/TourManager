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
            //ThrowIfExists(dataobject);
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
            //entities.People.Remove(dataobject.Person);
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
            throw new NotImplementedException();
        }

        public void ThrowIfExists(Customer dataobject)
        {
            throw new NotImplementedException();
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
