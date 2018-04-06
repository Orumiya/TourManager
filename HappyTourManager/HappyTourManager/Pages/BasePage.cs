using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using HappyTourManager.ViewModels;

namespace HappyTourManager.Pages
{
    public class BasePage<T> : Page where T : Bindable, new()
    {
        private T viewModel;

        public T ViewNodel
        {
            get
            { return viewModel; }
            set
            {
                if (viewModel == value)
                {
                    return;
                }

                viewModel = value;
                this.DataContext = viewModel;
            }
        }

        public BasePage()
        {
            this.ViewNodel= new T();
        }

    }
}
