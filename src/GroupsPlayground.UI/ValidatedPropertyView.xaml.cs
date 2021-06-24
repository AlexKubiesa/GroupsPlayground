﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GroupsPlayground.UI
{
    /// <summary>
    /// Interaction logic for ValidatedPropertyView.xaml
    /// </summary>
    public partial class ValidatedPropertyView : UserControl
    {
        public ValidatedPropertyView()
        {
            InitializeComponent();
        }

        public string PropertyLabel
        {
            get => PropertyLabelTextBlock.Text;
            set => PropertyLabelTextBlock.Text = value;
        }
    }
}
