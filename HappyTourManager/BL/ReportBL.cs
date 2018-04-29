// <copyright file="ReportBL.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BL.Interfaces;
    using DATA;
    using DATA.Interfaces;

    public class ReportBL : ISearcheable<Report>, IReportList
    {
        

        /// <inheritdoc />
        public event EventHandler ReportListChanged;

        /// <inheritdoc />
        public void Delete(Report report)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Save(Report report)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IList<Report> Search(object searchterm, object searchvalue)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void ThrowIfExists(Report report)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
