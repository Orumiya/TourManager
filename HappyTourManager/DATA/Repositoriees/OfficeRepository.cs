// <copyright file="OfficeRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DATA.Repositoriees
{
    using System;
    using System.Linq;
    using DATA.Interfaces;

    public class OfficeRepository : IRepository<Office>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="OfficeRepository"/> class.
        /// creates the repository
        /// </summary>
        /// <param name="entities">database</param>
        public OfficeRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }

        /// <summary>
        /// adds an office object to the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Create(Office dataobject)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// removes an office from the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Delete(Office dataobject)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// returns all offices from the database
        /// </summary>
        /// <returns>officelist</returns>
        public IQueryable<Office> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Office> Search(object searchterm, object searchvalue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// throws an exception if an office already exists -
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void ThrowIfExists(Office dataobject)
        {
            throw new NotImplementedException();
        }
    }
}
