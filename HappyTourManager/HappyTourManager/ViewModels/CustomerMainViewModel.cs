// <copyright file="CustomerMainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using BL;
    using DATA;
    using DATA.Interfaces;

    /// <summary>
    /// View model for Customer page
    /// </summary>
    public class CustomerMainViewModel : Bindable, IContentPage
    {
        private CustomerBL custBL;
        private string selectedCtegory = "DEFAULT";
        private string selectedValue;
        private DateTime selectedDateFrom = DateTime.Today;
        private DateTime selectedDateTo = DateTime.Today;
        private IList<Customer> rL;
        private ObservableCollection<Customer> resultList;
        private Customer selectedCustomer;
        private List<string> searchCategories;
        private IEnumerable<string> countryList;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerMainViewModel"/> class.
        /// Constructor for CustomerMainViewModel
        /// </summary>
        /// <param name="custRepository">repository</param>
        public CustomerMainViewModel(IRepository<Customer> custRepository)
        {
            this.custBL = new CustomerBL(custRepository);
            this.CreateCountryList();

            this.searchCategories = new List<string>();
            foreach (CustomerTerms item in Enum.GetValues(typeof(CustomerTerms)))
            {
                this.searchCategories.Add(item.ToString());
            }
        }

        /// <summary>
        /// Gets or sets list for all selectable search categories
        /// </summary>
        public List<string> SearchCategories
        {
            get
            {
                return this.searchCategories;
            }

            set
            {
                this.searchCategories = value;
                this.OnPropertyChanged(nameof(this.SearchCategories));
            }
        }

        /// <summary>
        /// Gets or sets contains the selected category
        /// </summary>
        public string SelectedCtegory
        {
            get
            {
                return this.selectedCtegory;
            }

            set
            {
                this.selectedCtegory = value;
                this.OnPropertyChanged(nameof(this.SelectedCtegory));
            }
        }

        /// <summary>
        /// Gets or sets cointain date search value
        /// </summary>
        public DateTime SelectedDateFrom
        {
            get
            {
                return this.selectedDateFrom;
            }

            set
            {
                this.selectedDateFrom = value;
                this.OnPropertyChanged(nameof(this.SelectedDateFrom));
            }
        }

        /// <summary>
        /// Gets or sets cointain date search value
        /// </summary>
        public DateTime SelectedDateTo
        {
            get
            {
                return this.selectedDateTo;
            }

            set
            {
                this.selectedDateTo = value;
                this.OnPropertyChanged(nameof(this.SelectedDateTo));
            }
        }

        /// <summary>
        /// Gets or sets contains string searchvalue
        /// </summary>
        public string SelectedValue
        {
            get
            {
                return this.selectedValue;
            }

            set
            {
                this.selectedValue = value;
                this.OnPropertyChanged(nameof(this.SelectedValue));
            }
        }

        /// <summary>
        /// Gets or sets contains the list of the search result
        /// </summary>
        public ObservableCollection<Customer> ResultList
        {
            get
            {
                return this.resultList;
            }

            set
            {
                this.resultList = value;
                this.OnPropertyChanged(nameof(this.ResultList));
            }
        }

        /// <summary>
        /// Gets or sets contains selected customer
        /// </summary>
        public Customer SelectedCustomer
        {
            get
            {
                return this.selectedCustomer;
            }

            set
            {
                this.selectedCustomer = value;
                this.OnPropertyChanged(nameof(this.SelectedCustomer));
            }
        }

        /// <summary>
        /// Gets or sets property
        /// </summary>
        public IEnumerable<string> CountryList
        {
            get
            {
                return this.countryList;
            }

            set
            {
                this.countryList = value;
            }
        }

        /// <summary>
        /// Get the search result list
        /// </summary>
        public void GetSearchResult()
        {
            if (this.SelectedCtegory == "VALIDTO")
            {
                DateTime[] dt = new DateTime[2];
                dt[0] = this.SelectedDateFrom;
                dt[1] = this.SelectedDateTo;

                this.rL = this.custBL.Search(Enum.Parse(typeof(CustomerTerms), this.SelectedCtegory), dt);
            }
            else if (this.SelectedCtegory == "LOYALTYCARD")
            {
                if (this.SelectedValue == "yes")
                {
                    this.rL = this.custBL.Search(Enum.Parse(typeof(CustomerTerms), this.SelectedCtegory), "1");
                }
                else
                {
                    this.rL = this.custBL.Search(Enum.Parse(typeof(CustomerTerms), this.SelectedCtegory), "0");
                }
            }
            else if (this.SelectedCtegory == "IDNUMBER")
            {
                this.rL = this.custBL.Search(Enum.Parse(typeof(CustomerTerms), this.SelectedCtegory), int.Parse(this.SelectedValue));
            }
            else
            {
                this.rL = this.custBL.Search(Enum.Parse(typeof(CustomerTerms), this.SelectedCtegory), this.SelectedValue);
            }

            this.ResultList = new ObservableCollection<Customer>(this.rL);
        }

        /// <summary>
        /// Save a new customer or update an existing one
        /// </summary>
        public void SaveInstance()
        {
            if (this.ResultList != null && this.ResultList.Contains(this.SelectedCustomer))
            {
                this.custBL.Update();
            }
            else
            {
                this.custBL.Save(this.SelectedCustomer);
            }
        }

        /// <summary>
        /// Delete selected customer
        /// </summary>
        public void DeleteInstance()
        {
            this.custBL.Delete(this.SelectedCustomer);
        }

        /// <summary>
        /// Check if all mandatory value is filled in
        /// </summary>
        /// <returns>returns true if data is ok</returns>
        public bool Checkvalues()
        {
            bool isNull = false;

            foreach (var item in this.SelectedCustomer.Person.GetType().GetProperties())
            {
                if (item.Name != "BirthDate" && item.Name != "ValidTo" && item.Name != "PersonID"
                    && item.Name != "Customer" && item.Name != "Tourguide")
                {
                    decimal parsedValue;

                    if (item.GetValue(this.SelectedCustomer.Person) == null)
                    {
                        isNull = true;
                    }
                    else if (decimal.TryParse(item.GetValue(this.SelectedCustomer.Person).ToString(), out parsedValue))
                    {
                        isNull = parsedValue == 0;
                    }
                }
            }

            return isNull;
        }

        private void CreateCountryList()
        {
            RegionInfo country = new RegionInfo(new CultureInfo("en-US", false).LCID);
            List<string> countryNames = new List<string>();
            foreach (CultureInfo cul in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                country = new RegionInfo(new CultureInfo(cul.Name, false).LCID);

                countryNames.Add(country.DisplayName.ToString());
            }

            this.CountryList = countryNames.OrderBy(names => names).Distinct();
        }
    }
}
