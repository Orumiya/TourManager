namespace DATA.Repositoriees
{
    using DATA.Interfaces;
    using System;
    using System.Linq;

    public class OrderRepository : IRepository<Order>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// creates the repository
        /// </summary>
        /// <param name="entities"></param>
        public OrderRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }

        public void Create(Order dataobject)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order dataobject)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Order> Search(object searchterm, object searchvalue)
        {
            throw new NotImplementedException();
        }

        public void ThrowIfExists(Order dataobject)
        {
            throw new NotImplementedException();
        }
    }
}
