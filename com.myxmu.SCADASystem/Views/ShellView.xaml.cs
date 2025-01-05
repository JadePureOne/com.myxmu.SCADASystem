using com.myxmu.SCADASystem.ViewModels;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using com.myxmu.SCADASystem.Messages;
using CommunityToolkit.Mvvm.Messaging;

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
            InitData();

            LoadLoginWindow();
        }

        private void LoadLoginWindow()
        {
            //get login view
            container.Content = App.current.Service.GetService<LoginView>();

            //if success login, show main window(message note from LoginViewModel)
            WeakReferenceMessenger.Default.Register<LoginMsg>(this, (sender, args) =>
            {
                container.Content = App.current.Service.GetService<MainView>();
                Width = 800;
                Height = 450;
                // 设置窗体弹出的坐标位置
                SetWindowLocation();
            });
            
        }

        private void SetWindowLocation()
        {
            Left = (SystemParameters.WorkArea.Width - Width) / 3;
            Top = (SystemParameters.WorkArea.Height - Height) / 3;
        }

        private void InitData()
        {
            this.DataContext = App.current.Service.GetService<ShellViewModel>();
        }
    }
}
