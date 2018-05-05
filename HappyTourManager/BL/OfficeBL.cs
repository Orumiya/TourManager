// <copyright file="OfficeBL.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BL
{
    using System;
    using BL.Interfaces;
    using DATA;
    using DATA.Interfaces;

    public class OfficeBL : IOffice
    {
        private readonly IRepository<Office> officeRepository;

        public OfficeBL(IRepository<Office> officeRepository)
        {
            this.officeRepository = officeRepository;
        }

        /// <inheritdoc />
        public event EventHandler OfficeDataChanged;

        /// <inheritdoc />
        public void Delete(Office office)
        {
            try
            {
                this.officeRepository.Delete(office);
            }
            finally
            {
                this.OnOfficeChanged();
            }
        }

        /// <inheritdoc />
        public void Save(Office office)
        {
            try
            {
                this.officeRepository.Create(office);
            }
            finally
            {
                this.OnOfficeChanged();
            }
        }

        /// <inheritdoc />
        public void Update() => this.officeRepository.Update();

        /// <summary>
        /// notifies the outside about any collection change manually
        /// </summary>
        private void OnOfficeChanged()
        {
            this.OfficeDataChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
