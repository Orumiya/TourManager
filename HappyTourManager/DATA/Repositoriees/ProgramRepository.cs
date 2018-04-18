namespace DATA.Repositoriees
{
    using DATA.Interfaces;
    using System;
    using System.Linq;

    class ProgramRepository : IRepository<Program>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// creates the repository
        /// </summary>
        /// <param name="entities"></param>
        public ProgramRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }
        public void Create(Program dataobject)
        {
            throw new NotImplementedException();
        }

        public void Delete(Program dataobject)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Program> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Program> Search(object searchterm, object searchvalue)
        {
            throw new NotImplementedException();
        }

        public void ThrowIfExists(Program dataobject)
        {
            throw new NotImplementedException();
        }
    }
}
