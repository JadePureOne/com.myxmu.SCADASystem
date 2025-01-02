using com.myxmu.SCADASystem.ViewModels;
using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace com.myxmu.SCADASystem.Views
{
    /// <summary>
    /// ShellView.xaml 的交互逻辑
    /// </summary>
    public partial class ShellView : MetroWindow
    {
        public ShellView()
        {
            InitializeComponent();

            this.DataContext = App.current.Service.GetService<ShellViewModel>();
        }
    }
}
