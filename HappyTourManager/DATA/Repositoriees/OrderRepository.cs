// <copyright file="OrderRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DATA.Repositoriees
{
    using System;
    using System.Linq;
    using DATA.Interfaces;

    public class OrderRepository : IRepository<Order>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepository"/> class.
        /// creates the repository
        /// </summary>
        /// <param name="entities">database</param>
        public OrderRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }

        /// <summary>
        /// adds a order object to the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Create(Order dataobject)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// removes an order object from the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Delete(Order dataobject)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// returns all orders from the database
        /// </summary>
        /// <returns>orderlist</returns>
        public IQueryable<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Order> Search(object searchterm, object searchvalue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// throws an exception if an order already exists -
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void ThrowIfExists(Order dataobject)
        {
            throw new NotImplementedException();
        }
    }
}
