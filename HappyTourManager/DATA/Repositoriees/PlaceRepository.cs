// <copyright file="PlaceRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DATA.Repositories
{
    using System;
    using System.Linq;
    using DATA.Interfaces;

    public class PlaceRepository : IRepository<Place>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceRepository"/> class.
        /// creates the repository
        /// </summary>
        /// <param name="entities">database</param>
        public PlaceRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }

        /// <summary>
        /// adds a new Place object to the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Create(Place dataobject)
        {
            this.ThrowIfExists(dataobject);
            this.entities.Places.Add(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// removes a Place from the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Delete(Place dataobject)
        {
            this.entities.Places.Remove(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// returns all Places from the database
        /// </summary>
        /// <returns>all Places</returns>
        public IQueryable<Place> GetAll()
        {
            return this.entities.Places;
        }

        /// <summary>
        /// throws an exception if the new Place exists
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void ThrowIfExists(Place dataobject)
        {
            bool exist = this.entities.Places.Any(
                e => e.City.Equals(dataobject.City) &&
                e.Country.Equals(dataobject.Country));
            if (exist)
            {
                throw new InvalidOperationException("Already exists!");
            }
        }
    }
}
