// <copyright file="IOffice.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BL.Interfaces
{
    using System;
    using DATA;

    public interface IOffice
    {
        /// <summary>
        /// Is fired when a delete or save happens.
        /// </summary>
        event EventHandler OfficeDataChanged;

        /// <summary>
        /// Deletes officedata
        /// </summary>
        /// <param name="office">input param</param>
        void Delete(Office office);

        /// <summary>
        /// Saves officedata
        /// </summary>
        /// <param name="office">input param</param>
        void Save(Office office);

        /// <summary>
        /// updates an entry
        /// </summary>
        void Update();
    }
}
