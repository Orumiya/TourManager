// <copyright file="IContentPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    /// <summary>
    /// Interface for ContentPages
    /// </summary>
    internal interface IContentPage
    {
        /// <summary>
        /// Show the result of the search
        /// </summary>
        void GetSearchResult();

        /// <summary>
        /// Check if the customer filled in all mandatory field
        /// </summary>
        /// <returns>true if values are ok</returns>
        bool Checkvalues();

        /// <summary>
        /// Save a new instance, or update it, if it exists.
        /// </summary>
        void SaveInstance();

        /// <summary>
        /// delete the selected instance
        /// </summary>
        void DeleteInstance();
    }
}
