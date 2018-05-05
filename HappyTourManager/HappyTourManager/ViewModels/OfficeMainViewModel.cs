// <copyright file="OfficeMainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using BL;
    using DATA;
    using DATA.Interfaces;

    /// <summary>
    /// View model for office page
    /// </summary>
    internal class OfficeMainViewModel : Bindable
    {
        private OfficeBL officeBL;
        private Office currentOffice;

        /// <summary>
        /// Initializes a new instance of the <see cref="OfficeMainViewModel"/> class.
        /// constructor for office view model
        /// </summary>
        /// <param name="officeRepository">office repository</param>
        public OfficeMainViewModel(IRepository<Office> officeRepository)
        {
            this.officeBL = new OfficeBL(officeRepository);
        }

        /// <summary>
        /// Gets or sets current Office instance
        /// </summary>
        public Office CurrentOffice
        {
            get
            {
                return this.currentOffice;
            }

            set
            {
                this.currentOffice = value;
                this.OnPropertyChanged(nameof(this.CurrentOffice));
            }
        }

        /// <summary>
        /// Save office data
        /// </summary>
        public void SaveInstance()
        {
            if (this.CurrentOffice != null)
            {
                this.officeBL.Save(this.CurrentOffice);
            }
    }
    }
}
