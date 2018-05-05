// <copyright file="AddCustomerUC.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for AddCustomerUC.xaml
    /// </summary>
    public partial class AddCustomerUC : UserControl
    {
        public AddCustomerUC()
        {
            this.InitializeComponent();

            this.cbIDtype.Items.Add("identity card");
            this.cbIDtype.Items.Add("passport");
            this.cbIDtype.Items.Add("driving licence");
        }
    }
}
