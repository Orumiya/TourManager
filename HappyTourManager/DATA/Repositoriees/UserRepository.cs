// <copyright file="UserRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DATA.Repositoriees
{
    using System;
    using System.Linq;
    using DATA.Interfaces;

    public class UserRepository : IRepository<User>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// creates the repository
        /// </summary>
        /// <param name="entities">database</param>
        public UserRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }

        /// <summary>
        /// adds a user object to the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Create(User dataobject)
        {
            this.ThrowIfExists(dataobject);
            this.entities.Users.Add(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// removes a user from the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Delete(User dataobject)
        {
            this.entities.Users.Remove(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// returns all users from the database
        /// </summary>
        /// <returns>userlist</returns>
        public IQueryable<User> GetAll()
        {
            return this.entities.Users;
        }

        /// <summary>
        /// throws exception if the new user is already exists
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void ThrowIfExists(User dataobject)
        {
            bool exist = this.entities.Users.Any(
                e => e.Username.Equals(dataobject.Username));
            if (exist)
            {
                throw new InvalidOperationException("Already exists!");
            }
        }
    }
}
