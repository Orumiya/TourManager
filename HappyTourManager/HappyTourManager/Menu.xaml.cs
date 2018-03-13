using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HappyTourManager
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        /// <summary>
        /// hivatkozást tárol a MainWindow mainFrame-jére
        /// </summary>
        private Frame mainFrame;

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        /// <param name="mainFrame">a mainframe-t kapja meg paraméterként</param>
        public Menu(Frame mainFrame)
        {
            this.InitializeComponent();
            this.mainFrame = mainFrame;
        }
    }
}
