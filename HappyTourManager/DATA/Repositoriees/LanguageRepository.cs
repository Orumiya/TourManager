namespace DATA.Repositoriees
{
    using DATA.Interfaces;
    using System;
    using System.Linq;

    class LanguageRepository : IRepository<Language>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// creates the repository
        /// </summary>
        /// <param name="entities"></param>
        public LanguageRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }

        public void Create(Language dataobject)
        {
            throw new NotImplementedException();
        }

        public void Delete(Language dataobject)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Language> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Language> Search(object searchterm, object searchvalue)
        {
            throw new NotImplementedException();
        }

        public void ThrowIfExists(Language dataobject)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
