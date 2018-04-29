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

    public class ReportBL : ISearcheable<Report>
    {
        public IList<Report> Search(object searchterm, object searchvalue)
        {
            throw new NotImplementedException();
        }
    }
}
}
