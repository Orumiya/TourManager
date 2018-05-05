// <copyright file="OfficeBL.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BL
{
    using System;
    using BL.Interfaces;
    using DATA;

    public class OfficeBL : IOffice
    {
        public event EventHandler OfficeDataChanged;

        public void Delete(Office office)
        {
            throw new NotImplementedException();
        }

        public void Save(Office office)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
