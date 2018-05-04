// <copyright file="ITourguideList.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BL.Interfaces
{
    using System;
    using DATA;

    public interface ITourguideList
    {
        /// <summary>
        /// Is fired when a delete or save happens.
        /// </summary>
        event EventHandler TourguideListChanged;

        /// <summary>
        /// Deletes Tourguide from the TourguideList list.
        /// </summary>
        /// <param name="guide">input param</param>
        void Delete(Tourguide guide);

        /// <summary>
        /// Saves Tourguide regardless of it is already in the list
        /// </summary>
        /// <param name="guide">input param</param>
        void Save(Tourguide guide);

        /// <summary>
        /// Checks whether a Tourguide exists in the list.
        /// </summary>
        /// <param name="guide">input param</param>
        void ThrowIfExists(Tourguide guide);

        /// <summary>
        /// updates an entry
        /// </summary>
        void Update();
    }
}
