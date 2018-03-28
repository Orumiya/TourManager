using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HappyTourManager
{
    /// <summary>
    /// View Model for the basic window
    /// </summary>
    class WindowViewModel
    {
        private Window window;

        public string Test { get; set; } = "My string";


        /// <summary>
        /// Construnctor
        /// </summary>
        /// <param name="window"></param>
        public WindowViewModel(Window window)
        {
            this.window = window;
        }

    }
}
