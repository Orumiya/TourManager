// <copyright file="TourRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DATA.Repositoriees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DATA.Interfaces;

    public class TourRepository : IRepository<Tour>, IUpdateRepo
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="TourRepository"/> class.
        /// creates the repository
        /// </summary>
        /// <param name="entities">database</param>
        public TourRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }

        /// <summary>
        /// adds a Tour object to the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Create(Tour dataobject)
        {
            this.ThrowIfExists(dataobject);
            this.entities.Tours.Add(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// removes a Program from the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Delete(Tour dataobject)
        {
            this.entities.Tours.Remove(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// returns all Tours from the database
        /// </summary>
        /// <returns>TourList</returns>
        public IQueryable<Tour> GetAll()
        {
            return this.entities.Tours;
        }

        /// <summary>
        /// throws an exception if the new Tour already exists:
        /// same start and end date & same min and max number & same TravelName
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void ThrowIfExists(Tour dataobject)
        {
            bool exist = this.entities.Tours.Any(
                e => e.StartDate.Equals(dataobject.StartDate) &&
                e.EndDate.Equals(dataobject.EndDate) && e.MinNumber.Equals(dataobject.MinNumber) &&
                e.MaxNumber.Equals(dataobject.MaxNumber) && e.TravelName.Equals(dataobject.TravelName));
            if (exist)
            {
                throw new InvalidOperationException("Already exists!");
            }
        }

        /// <summary>
        /// updates an entry
        /// </summary>
        public void Update()
        {
            this.entities.SaveChanges();
        }
    }
}
