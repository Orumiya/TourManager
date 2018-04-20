// <copyright file="PRTCONRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DATA.Repositoriees
{
    using System;
    using System.Linq;
    using DATA.Interfaces;

    public class PLTCONRepository : IRepository<PLTCON>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="PLTCONRepository"/> class.
        /// creates the repository
        /// </summary>
        /// <param name="entities">database</param>
        public PLTCONRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }

        /// <summary>
        /// adds a new Place-Tour connection object to the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Create(PLTCON dataobject)
        {
            this.ThrowIfExists(dataobject);
            this.entities.PLTCONs.Add(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// removes a new Place-Tour connection object to the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Delete(PLTCON dataobject)
        {
            this.entities.PLTCONs.Remove(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// returns all Place-Tour connections from the database
        /// </summary>
        /// <returns>input param</returns>
        public IQueryable<PLTCON> GetAll()
        {
            return this.entities.PLTCONs;
        }

        /// <summary>
        /// throws an exception if a Place-Tour connection already exists -
        /// same Tour & same Place
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void ThrowIfExists(PLTCON dataobject)
        {
            bool exist = this.entities.PLTCONs.Any(
                e => e.PlaceID.Equals(dataobject.PlaceID) &&
                e.TourID.Equals(dataobject.TourID));
            if (exist)
            {
                throw new InvalidOperationException("Already exists!");
            }
        }
    }
}
