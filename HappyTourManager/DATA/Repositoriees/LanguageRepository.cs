// <copyright file="LanguageRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DATA.Repositoriees
{
    using System;
    using System.Linq;
    using DATA.Interfaces;

    public class LanguageRepository : IRepository<Language>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageRepository"/> class.
        /// creates the repository
        /// </summary>
        /// <param name="entities">database</param>
        public LanguageRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }

        /// <summary>
        /// adds a new Language object to the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Create(Language dataobject)
        {
            this.ThrowIfExists(dataobject);
            this.entities.Languages.Add(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// removes a Language from the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Delete(Language dataobject)
        {
            this.entities.Languages.Remove(dataobject);
            this.entities.SaveChanges();
        }

        /// <summary>
        /// returns all Languages from the database
        /// </summary>
        /// <returns>language list</returns>
        public IQueryable<Language> GetAll()
        {
            return this.entities.Languages;
        }

        /// <summary>
        /// throws an exception if a Tourguide-language pair already exists -
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void ThrowIfExists(Language dataobject)
        {
            bool exist = this.entities.Languages.Any(
                e => e.TourguideID.Equals(dataobject.TourguideID) &&
                e.Language1.Equals(dataobject.Language1));
            if (exist)
            {
                throw new InvalidOperationException("Already exists!");
            }
        }
    }
}
