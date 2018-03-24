namespace DATA.Interfaces
{
    using System.Linq;

    public interface IRepository<T>
    {
        /// <summary>
        /// returns all data objects
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// create new dataobject
        /// </summary>
        /// <param name="dataobject"></param>
        void Create(T dataobject);

        /// <summary>
        /// Update data (generic method)
        /// </summary>
        void Update();

        /// <summary>
        /// Delete dataobject (generic method)
        /// </summary>
        /// <param name="dataobject"></param>
        void Delete(T dataobject);

        /// <summary>
        /// throws exception, if same object exists
        /// </summary>
        /// <param name="dataobject"></param>
        void ThrowIfExists(T dataobject);

        /// <summary>
        /// searches for a dataobject with specified searchterm and value
        /// </summary>
        /// <param name="searchterm"></param>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        IQueryable<T> Search(object searchterm, object searchvalue);

    }
}
