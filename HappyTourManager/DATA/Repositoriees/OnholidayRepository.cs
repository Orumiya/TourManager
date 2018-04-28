// <copyright file="OnholidayRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DATA.Repositoriees
{
    using System;
    using System.Linq;
    using DATA.Interfaces;

    public class OnholidayRepository : IRepository<OnHoliday>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnholidayRepository"/> class.
        /// creates the repository
        /// </summary>
        /// <param name="entities">database</param>
        public OnholidayRepository(HappyTourDatabaseEntities entities)
        {
           this.entities = entities;
        }

        /// <summary>
        /// adds a new Holiday object to the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Create(OnHoliday dataobject)
        {
            this.ThrowIfExists(dataobject);
            this.entities.OnHolidays.Add(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// removes a Holiday from the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Delete(OnHoliday dataobject)
        {
            this.entities.OnHolidays.Remove(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// returns all holidays from the database
        /// </summary>
        /// <returns>holidaylist</returns>
        public IQueryable<OnHoliday> GetAll()
        {
            return this.entities.OnHolidays;
        }

        /// <summary>
        /// throws an exception if a Holiday already exists -
        /// same Holiday = same StartDate, same EndDate, same tourguide
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void ThrowIfExists(OnHoliday dataobject)
        {
            bool exist = this.entities.OnHolidays.Any(
                e => e.TourguideID.Equals(dataobject.TourguideID)
                && e.StartDate.Equals(dataobject.StartDate)
                && e.EndDate.Equals(dataobject.EndDate));
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
