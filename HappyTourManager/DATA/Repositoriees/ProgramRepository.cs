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
            this.ThrowIfExists(dataobject);
            this.entities.Programs.Add(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// removes a Program from the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Delete(Program dataobject)
        {
            this.entities.Programs.Remove(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// returns all Programs from the database
        /// </summary>
        /// <returns>ProgramList</returns>
        public IQueryable<Program> GetAll()
        {
            return this.entities.Programs;
        }

        /// <summary>
        /// throws an exception if the new Program exists
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void ThrowIfExists(Program dataobject)
        {
            bool exist = this.entities.Programs.Any(
                e => e.ProgramType.Equals(dataobject.ProgramType));
            if (exist)
            {
                throw new InvalidOperationException("Already exists!");
            }
        }
    }
}
