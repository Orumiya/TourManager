// <copyright file="IReportList.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BL.Interfaces
{
    using System;
    using DATA;

    public interface IReportList
    {
        /// <summary>
        /// Is fired when a delete or save happens.
        /// </summary>
        event EventHandler ReportListChanged;

        /// <summary>
        /// Deletes Report from the ReportList list.
        /// </summary>
        /// <param name="report">input param</param>
        void Delete(Report report);

        /// <summary>
        /// Saves Report regardless of it is already in the list
        /// </summary>
        /// <param name="report">input param</param>
        void Save(Report report);

        /// <summary>
        /// Checks whether a Report exists in the list.
        /// </summary>
        /// <param name="report">input param</param>
        void ThrowIfExists(Report report);

        /// <summary>
        /// updates an entry
        /// </summary>
        void Update();
    }
}
