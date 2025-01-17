using com.myxmu.SCADASystem.ViewModels;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using com.myxmu.SCADASystem.Messages;
using Model;
using com.myxmu.SCADASystem.Services;
using CommunityToolkit.Mvvm.Messaging;

namespace com.myxmu.SCADASystem.Views
{
    /// <summary>
    /// ShellView.xaml 的交互逻辑
    /// </summary>
    public partial class ShellView : MetroWindow
    {
        private UserSession _userSession;
        public ShellView(UserSession userSession)
        {
            InitializeComponent();
            _userSession = userSession;
            InitData();
            LoadLoginWindow();
        }

        private void LoadLoginWindow()
        {
            //get login view
            container.Content = App.current.Services.GetService<LoginView>();

            //if success login, show main window(message note from LoginViewModel)
            WeakReferenceMessenger.Default.Register<LoginMsg>(this, (sender, args) =>
            {
                container.Content = App.current.Services.GetService<MainView>();
                Width = 1200;
                Height = 800;
                // 设置窗体弹出的坐标位置
                SetWindowLocation();
            });

            //切换用户
            WeakReferenceMessenger.Default.Register<LoginOutMsg>(this, (sender, args) =>
            {

                container.Content = App.current.Services.GetService<LoginView>();
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
            this.DataContext = App.current.Services.GetService<ShellViewModel>();
        }
    }
}
