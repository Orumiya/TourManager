namespace HappyTourManager
{
    using BL;
    using DATA;

    /// <summary>
    /// View model for office page
    /// </summary>
    class OfficeMainViewModel : Bindable
    {
        private OfficeBL officeBL;
        private Office currentOffice;

        /// <summary>
        /// current Office instance
        /// </summary>
        public Office CurrentOffice
        {
            get
            {
                return currentOffice;
            }

            set
            {
                currentOffice = value;
                this.OnPropertyChanged(nameof(CurrentOffice));
            }
        }

        /// <summary>
        /// constructor for office view model
        /// </summary>
        public OfficeMainViewModel()
        {
            officeBL = new OfficeBL();
        }

        /// <summary>
        /// Save office data
        /// </summary>
        public void SaveInstance()
        {
            if (this.CurrentOffice != null)
            {

                this.officeBL.Save(CurrentOffice);
            }



    }
    }
}
