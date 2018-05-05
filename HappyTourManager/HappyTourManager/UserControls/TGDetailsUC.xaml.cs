// <copyright file="TGDetailsUC.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for TGDetailsUC.xaml
    /// </summary>
    public partial class TGDetailsUC : UserControl
    {
        public TGDetailsUC()
        {
            this.InitializeComponent();

            this.cbIDtype.Items.Add("identity card");
            this.cbIDtype.Items.Add("passport");
            this.cbIDtype.Items.Add("driving licence");
        }
    }
}
