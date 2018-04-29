using BL;
using DATA;
using DATA.Repositoriees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyTourManager
{
    class CustomerMainViewModel : Bindable
    {
        private CustomerRepository custRepository;
        private CustomerBL custBL;

        private List<string> searchCategories;

        public List<string> SearchCategories
        {
            get
            {
                return searchCategories;
            }

            set
            {
                searchCategories = value;
                OnPropertyChanged(nameof(SearchCategories));
            }
        }

        public string SelectedCtegory
        {
            get
            {
                return selectedCtegory;
            }

            set
            {
                selectedCtegory = value;
                OnPropertyChanged(nameof(SelectedCtegory));
            }
        }

        public DateTime SelectedDateFrom
        {
            get
            {
                return selectedDateFrom;
            }

            set
            {
                selectedDateFrom = value;
                OnPropertyChanged(nameof(SelectedDateFrom));
            }
        }

        public DateTime SelectedDateTo
        {
            get
            {
                return selectedDateTo;
            }

            set
            {
                selectedDateTo = value;
                OnPropertyChanged(nameof(SelectedDateTo));
            }
        }

        public string SelectedValue
        {
            get
            {
                return selectedValue;
            }

            set
            {
                selectedValue = value;
                OnPropertyChanged(nameof(SelectedValue));
            }
        }

        private string selectedCtegory = "DEFAULT";
        private string selectedValue;
        private DateTime selectedDateFrom = DateTime.Today;
        private DateTime selectedDateTo = DateTime.Today;

        public CustomerMainViewModel(HappyTourDatabaseEntities entities)
        {
            custRepository = new CustomerRepository(entities);
            custBL = new CustomerBL(custRepository);

            searchCategories = new List<string>();
            foreach (CustomerTerms item in Enum.GetValues(typeof(CustomerTerms)))
            {
                searchCategories.Add(item.ToString());
            }
        }



    }
}
