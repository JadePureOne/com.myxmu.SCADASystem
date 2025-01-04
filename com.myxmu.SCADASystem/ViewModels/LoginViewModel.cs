using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using com.myxmu.SCADASystem.Models;
using Common.Helpers;
using CommunityToolkit.Mvvm.Input;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class LoginViewModel:ObservableObject
    {
        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        [RelayCommand]
        private void Login()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("密码或用户名为空"); 
                return;
            }

            var userList = SqlSugarHelper.Db.Queryable<UserModel>().Where(e => e.UserName == UserName && e.Password == Password)
                .ToList();

            if (userList.Count>0)
            {
                MessageBox.Show("login success");
            }
            else
            {
                MessageBox.Show("Login false,check name or pwd");
            }

        }
    }
}
