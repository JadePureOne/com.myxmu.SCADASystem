﻿using com.myxmu.SCADASystem.ViewModels;
using Microsoft.Extensions.DependencyInjection;
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

namespace com.myxmu.SCADASystem.Views
{
    /// <summary>
    /// FlowNodeView.xaml 的交互逻辑
    /// </summary>
    public partial class FlowNodeView : UserControl
    {
        public FlowNodeView()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            this.DataContext = App.Current.Services.GetService<FlowNodeViewModel>();
        }
    }
}
