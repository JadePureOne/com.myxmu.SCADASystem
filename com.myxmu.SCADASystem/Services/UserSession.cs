using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Model;
using com.myxmu.SCADASystem.UserControls;
using CommunityToolkit.Mvvm.ComponentModel;
using MaterialDesignThemes.Wpf;

namespace com.myxmu.SCADASystem.Services
{
    public class UserSession:ObservableObject
    {
        private UserModel _user=new();

        public UserModel CurrentUser
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public void ShowMessageBox(string header, MessageBoxButton btn = MessageBoxButton.OK)
        {
            App.current.Dispatcher.Invoke(() =>
            {
                DialogHost.Show(new MsgBox(header, btn), "ShellDialog");
            });
        }
    }
}
