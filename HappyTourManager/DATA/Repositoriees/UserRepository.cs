namespace DATA.Repositoriees
{
    using DATA.Interfaces;
    using System;
    using System.Linq;

    class UserRepository : IRepository<User>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// creates the repository
        /// </summary>
        /// <param name="entities"></param>
        public UserRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }
        public void Create(User dataobject)
        {
            throw new NotImplementedException();
        }

        public void Delete(User dataobject)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> Search(object searchterm, object searchvalue)
        {
            throw new NotImplementedException();
        }

        public void ThrowIfExists(User dataobject)
        {
            throw new NotImplementedException();
        }
    }
}
