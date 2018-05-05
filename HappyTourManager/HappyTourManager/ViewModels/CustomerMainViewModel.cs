﻿// <copyright file="App.xaml.cs" company="PlaceholderCompany">
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
        public IEnumerable<string> countryList;

        /// <summary>
        /// List for all selectable search categories
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
        /// Contains the selected category
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
        /// Cointain date search value
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
        /// Cointain date search value
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
        /// Contains string searchvalue
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
        /// Contains the list of the search result
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
        /// Contains selected customer
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
        /// Constructor for CustomerMainViewModel
        /// </summary>
        /// <param name="custRepository"></param>
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
        /// <returns></returns>
        public bool Checkvalues()
        {
            bool isNull = false;

            foreach (var item in this.SelectedCustomer.Person.GetType().GetProperties())
            {
                if (item.Name != "BirthDate" && item.Name != "ValidTo" && item.Name != "PersonID"
                    && item.Name != "Customer" && item.Name != "Tourguide")
                {
                    Decimal parsedValue;

                    if (item.GetValue(this.SelectedCustomer.Person) == null)
                    {
                        isNull = true;
                    }
                    else if (Decimal.TryParse(item.GetValue(this.SelectedCustomer.Person).ToString(), out parsedValue))
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

            this.countryList = countryNames.OrderBy(names => names).Distinct();
        }

    }
}
