// <copyright file="ITourList.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BL.Interfaces
{
    using System;
    using DATA;

    public interface ITourList
    {
        /// <summary>
        /// Is fired when a delete or save happens.
        /// </summary>
        event EventHandler TourListChanged;

        /// <summary>
        /// Deletes Tour from the TourList list.
        /// </summary>
        /// <param name="tour">input param</param>
        void Delete(Tour tour);

        /// <summary>
        /// Saves Tour regardless of it is already in the list
        /// </summary>
        /// <param name="tour">input param</param>
        void Save(Tour tour);

        /// <summary>
        /// Checks whether a Tour exists in the list.
        /// </summary>
        /// <param name="tour">input param</param>
        void ThrowIfExists(Tour tour);
    }
}
