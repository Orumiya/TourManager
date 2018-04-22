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
            this.ThrowIfExists(dataobject);
            this.entities.Orders.Add(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// removes an order object from the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Delete(Order dataobject)
        {
            this.entities.Orders.Remove(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// returns all orders from the database
        /// </summary>
        /// <returns>orderlist</returns>
        public IQueryable<Order> GetAll()
        {
            return this.entities.Orders;
        }

        /// <summary>
        /// throws an exception if an order already exists -
        /// same order = same Customer + same Tour + same OrderDate + same Personcount
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void ThrowIfExists(Order dataobject)
        {
            bool exist = this.entities.Orders.Any(
               e => e.CustomerID.Equals(dataobject.CustomerID) && e.TourID.Equals(dataobject.TourID)
               && e.OrderDate.Equals(dataobject.OrderDate) && e.PersonCount.Equals(dataobject.PersonCount));
            if (exist)
            {
                throw new InvalidOperationException("Already exists!");
            }
        }
    }
}
