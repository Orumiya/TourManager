namespace BL.Interfaces
{
    using System.Collections.Generic;

    public interface ISearcheable<T>
    {
        /// <summary>
        /// searches for a dataobject with specified searchterm and value
        /// </summary>
        /// <param name="searchterm"></param>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        IList<T> Search(object searchterm, object searchvalue);
    }
}
