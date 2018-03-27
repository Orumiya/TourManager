namespace DATA.Repositoriees
{
    using DATA.Interfaces;
    using System;
    using System.Linq;

    class OfficeRepository : IRepository<Office>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// creates the repository
        /// </summary>
        /// <param name="entities"></param>
        public OfficeRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }
        public void Create(Office dataobject)
        {
            throw new NotImplementedException();
        }

        public void Delete(Office dataobject)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Office> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Office> Search(object searchterm, object searchvalue)
        {
            throw new NotImplementedException();
        }

        public void ThrowIfExists(Office dataobject)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
