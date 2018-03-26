namespace DATA.Repositoriees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DATA.Interfaces;

    public enum CustomerTerms
    {

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
            this.entities = new HappyTourDatabaseEntities();
        }



    }
}
