namespace HappyTourManager
{
    /// <summary>
    /// Interface for ContentPages
    /// </summary>
    interface IContentPage
    {
        /// <summary>
        /// Show the result of the search
        /// </summary>
        void GetSearchResult();

        /// <summary>
        /// Check if the customer filled in all mandatory field
        /// </summary>
        /// <returns></returns>
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
