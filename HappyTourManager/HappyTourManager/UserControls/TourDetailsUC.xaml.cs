// <copyright file="TourDetailsUC.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for TourDetailsUC.xaml
    /// </summary>
    public partial class TourDetailsUC : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TourDetailsUC"/> class.
        /// Tour datail usercontrol
        /// </summary>
        public TourDetailsUC()
        {
            this.InitializeComponent();

            this.cbTrans.Items.Add("bus");
            this.cbTrans.Items.Add("flight");
            this.cbTrans.Items.Add("cruise");
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
