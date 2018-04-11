namespace TEST.Fakes
{
    using DATA.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FakeRepository<T> : IRepository<T>
    {
        /// <summary>
        /// instead of database entities, we use an IEnumerable generic variable
        /// </summary>
        private IEnumerable<T> expected;

        /// <summary>
        /// list of deleted objects
        /// </summary>
        public IList<T> DeletedObjects { get; } = new List<T>();

        /// <summary>
        /// list of saved objects
        /// </summary>
        public IList<T> SavedObjects { get; } = new List<T>();

        /// <summary>
        /// list for faking exceptions
        /// </summary>
        public IList<T> ThrowCalls { get; } = new List<T>();

        /// <summary>
        /// creates a fake repository
        /// </summary>
        /// <param name="expected">input param as fake database</param>
        public FakeRepository(IEnumerable<T> expected) => this.expected = expected;

        /// <summary>
        /// puts the object to the savedobjects list
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Create(T dataobject)
        {
            SavedObjects.Add(dataobject);
        }

        /// <summary>
        /// puts the object to the deletedobjects list
        /// </summary>
        /// <param name="dataobject"></param>
        public void Delete(T dataobject)
        {
            DeletedObjects.Add(dataobject);
        }

        /// <summary>
        /// fakes the db.GetAll method
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return expected.AsQueryable();
        }

        /// <summary>
        /// fakes the exception throwing method
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void ThrowIfExists(T dataobject)
        {
            ThrowCalls.Add(dataobject);
        }
    }
}
