namespace HappyTourManager.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Controls;

    public class BasePage<T> : Page where T : Bindable, new()
    {
        private T viewModel;

        public T ViewNodel
        {
            get
            {
                return this.viewModel;
            }

            set
            {
                if (this.viewModel == value)
                {
                    return;
                }
                this.viewModel = value;
                this.DataContext = this.viewModel;
            }
        }

        public BasePage()
        {
            this.ViewNodel= new T();
        }

    }
}
