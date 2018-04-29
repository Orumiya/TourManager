// <copyright file="PRTCONRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DATA.Repositoriees
{
    using System;
    using System.Linq;
    using DATA.Interfaces;

    public class PRTCONRepository : IRepository<PRTCON>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="PRTCONRepository"/> class.
        /// creates the repository
        /// </summary>
        /// <param name="entities">database</param>
        public PRTCONRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }

        /// <summary>
        /// adds a new Program-Tour connection object to the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Create(PRTCON dataobject)
        {
            this.ThrowIfExists(dataobject);
            this.entities.PRTCONs.Add(dataobject);
            this.entities.SaveChanges();
        }


        /// <summary>
        /// removes a new Program-Tour connection object to the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Delete(PRTCON dataobject)
        {
            this.entities.PRTCONs.Remove(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// returns all Program-Tour connections from the database
        /// </summary>
        /// <returns>input param</returns>
        public IQueryable<PRTCON> GetAll()
        {
            return this.entities.PRTCONs;
        }

        /// <summary>
        /// throws an exception if a Program-Tour connection already exists -
        /// same Tour & same Program
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void ThrowIfExists(PRTCON dataobject)
        {
            bool exist = this.entities.PRTCONs.Any(
                e => e.ProgramID.Equals(dataobject.ProgramID) &&
                e.TourID.Equals(dataobject.TourID));
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
