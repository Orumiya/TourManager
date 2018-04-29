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
    class MainViewModel
    {
        //private CustomerRepository custRepository;
        //private CustomerBL custBL;
        HappyTourDatabaseEntities entities;
        private string selectedPage;

        public MainViewModel(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
            
        }

    }
}
