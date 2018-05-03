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
    /// Interaction logic for TGDetailsUC.xaml
    /// </summary>
    public partial class TGDetailsUC : UserControl
    {
        public TGDetailsUC()
        {
            InitializeComponent();

            cboxLanguage.Items.Add("english");
            cboxLanguage.Items.Add("german");
            cboxLanguage.Items.Add("french");
            cboxLanguage.Items.Add("spanish");
            cboxLanguage.Items.Add("italian");
            cboxLanguage.Items.Add("chinese");
            cboxLanguage.Items.Add("japanese");
        }
    }
}
