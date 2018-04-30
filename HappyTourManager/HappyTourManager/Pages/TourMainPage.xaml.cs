using DATA;
using DATA.Repositoriees;
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

namespace HappyTourManager.Pages
{
    
    
    /// <summary>
    /// Interaction logic for TourMainPage.xaml
    /// </summary>
    /// 
    public partial class TourMainPage : Page
    {
        TourRepository tourRepo;

        public TourMainPage(TourRepository tourRepo)
        {
            InitializeComponent();
            this.tourRepo = tourRepo;
        }
    }
}
