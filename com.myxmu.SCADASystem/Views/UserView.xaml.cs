using com.myxmu.SCADASystem.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace com.myxmu.SCADASystem.Views
{
    /// <summary>
    /// UserView.xaml 的交互逻辑
    /// </summary>
    public partial class UserView : UserControl
    {
        public UserView()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            this.DataContext = App.Current.Services.GetService<UserViewModel>();
        }
    }
}