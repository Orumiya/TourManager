﻿namespace HappyTourManager
{
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

    /// <summary>
    /// Interaction logic for OrderDetailsUC.xaml
    /// </summary>
    public partial class OrderDetailsUC : UserControl
    {
        public OrderDetailsUC()
        {
            this.InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = -1; i < 10; i++)
            {
                this.cbAdults.Items.Add(i + 1);
            }
        }
    }
}
