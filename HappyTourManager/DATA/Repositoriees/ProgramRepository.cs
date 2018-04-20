// <copyright file="ProgramRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DATA.Repositoriees
{
    using System;
    using System.Linq;
    using DATA.Interfaces;

    public class ProgramRepository : IRepository<Program>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramRepository"/> class.
        /// creates the repository
        /// </summary>
        /// <param name="entities">database</param>
        public ProgramRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }

        /// <summary>
        /// adds a new Program object to the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Create(Program dataobject)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// removes a Program from the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Delete(Program dataobject)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// returns all Programs from the database
        /// </summary>
        /// <returns>ProgramList</returns>
        public IQueryable<Program> GetAll()
        {
            throw new NotImplementedException();
        }


        public IQueryable<Program> Search(object searchterm, object searchvalue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// throws an exception if the new Program exists
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void ThrowIfExists(Program dataobject)
        {
            throw new NotImplementedException();
        }
    }
}
