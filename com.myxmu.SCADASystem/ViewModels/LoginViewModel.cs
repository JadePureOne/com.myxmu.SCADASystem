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
using System.ComponentModel;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class LoginViewModel:ObservableObject
    {
        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        //extend IDataErrorInfo
        //public string Error => null;
        //public string this[string columnName]
        //{
        //    get
        //    {
        //        string result = null;
        //        switch (columnName)
        //        {
        //            case nameof(UserName):
        //                if (string.IsNullOrWhiteSpace(UserName))
        //                    result = "用户名不能为空";
        //                break;
        //            case nameof(Password):
        //                if (string.IsNullOrWhiteSpace(Password))
        //                    result = "密码不能为空";
        //                break;
        //        }
        //        return result;
        //    }


        public bool CanLogin => !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password);




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
