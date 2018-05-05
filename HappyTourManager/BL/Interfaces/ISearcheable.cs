// <copyright file="ISearcheable.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BL.Interfaces
{
    using System.Collections.Generic;

    public interface ISearcheable<T>
    {
        /// <summary>
        /// searches for a dataobject with specified searchterm and value
        /// </summary>
        /// <param name="searchterm">searchcategory</param>
        /// <param name="searchvalue">value</param>
        /// <returns>objectlist</returns>
        IList<T> Search(object searchterm, object searchvalue);
    }
}
