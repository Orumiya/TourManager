// <copyright file="ReportRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DATA.Repositoriees
{
    using System;
    using System.Linq;
    using DATA.Interfaces;

    public class ReportRepository : IRepository<Report>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportRepository"/> class.
        /// creates the repository
        /// </summary>
        /// <param name="entities">database</param>
        public ReportRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }

        /// <summary>
        /// adds a report object to the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Create(Report dataobject)
        {
            this.ThrowIfExists(dataobject);
            this.entities.Reports.Add(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// removes a report object from the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Delete(Report dataobject)
        {
            this.entities.Reports.Remove(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// returns all reports from the database
        /// </summary>
        /// <returns>reportlist</returns>
        public IQueryable<Report> GetAll()
        {
            return this.entities.Reports;
        }

        /// <summary>
        /// throws an exception if a report already exists -
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void ThrowIfExists(Report dataobject)
        {
            bool exist = this.entities.Reports.Any(
               e => e.ReportType.Equals(dataobject.ReportType) && e.ReportText.Equals(dataobject.ReportText));
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
