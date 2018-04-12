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

        public void Create(User dataobject)
        {
            this.ThrowIfExists(dataobject);
            this.entities.Users.Add(dataobject);
            this.entities.SaveChanges();
        }

        public void Delete(User dataobject)
        {
            this.entities.Users.Remove(dataobject);
            this.entities.SaveChanges();
        }

        public IQueryable<User> GetAll()
        {
            return this.entities.Users;
        }

        /// <summary>
        /// throws exception if user is already exists
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
