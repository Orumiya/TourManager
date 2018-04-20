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
            throw new NotImplementedException();
        }

        /// <summary>
        /// removes a report object from the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Delete(Report dataobject)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// returns all reports from the database
        /// </summary>
        /// <returns>reportlist</returns>
        public IQueryable<Report> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Report> Search(object searchterm, object searchvalue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// throws an exception if a report already exists -
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void ThrowIfExists(Report dataobject)
        {
            throw new NotImplementedException();
        }
    }
}
