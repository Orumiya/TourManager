namespace DATA.Repositoriees
{
    using System;
    using System.Linq;
    using DATA.Interfaces;

    public class CustomerRepository : IRepository<Customer>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRepository"/> class.
        /// creates the repository
        /// </summary>
        /// <param name="entities">input param</param>
        public CustomerRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }

        /// <summary>
        /// adds a new Customer object to the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Create(Customer dataobject)
        {
            this.ThrowIfExists(dataobject);
            this.entities.People.Add(dataobject.Person);
            this.entities.Customers.Add(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// removes a Customer from the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Delete(Customer dataobject)
        {
            this.entities.Customers.Remove(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// returns all Customers from the database
        /// </summary>
        /// <returns>input param</returns>
        public IQueryable<Customer> GetAll()
        {
            return this.entities.Customers;
        }

        /// <summary>
        /// throws an exception if a Customer already exists -
        /// same Customer = same FirstName, same LastName, same BirthDate
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void ThrowIfExists(Customer dataobject)
        {
            bool exist = this.entities.Customers.Any(
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
            this.entities.SaveChanges();
        }
    }
}
