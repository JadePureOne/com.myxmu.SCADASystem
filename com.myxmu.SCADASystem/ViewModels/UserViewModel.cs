using System.Windows;
using AngleSharp.Dom;
using com.myxmu.SCADASystem.Models;
using com.myxmu.SCADASystem.Services;
using com.myxmu.SCADASystem.Views;
using Common.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Masuit.Tools;
using MaterialDesignThemes.Wpf;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class UserViewModel : ObservableObject
    {
        public UserSession UserSession { get; }

        [ObservableProperty]
        private List<UserModel> __userList = new();

        public UserViewModel(UserSession userSession)
        {
            UserSession = userSession;
        }


        /// <summary>
        /// 是否有管理员权限
        /// </summary>
        /// <returns></returns>
        private bool HasPermiss()
        {
            if (UserSession.CurrentUser.Role == 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show("no permission");
                return false;

            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        [RelayCommand]
        private async void AddUser(UserViewModel user)
        {
            if (!HasPermiss())
            {
                return;
            }

            var entity = new UserModel();
            var view = new UserOperateDialogView() { DataContext = new UpdateViewModel<UserModel>(entity) };
            var result = (bool)await DialogHost.Show(view, "ShellDialog");
            if (result)
            {
                //Console.WriteLine($"Name:{entity.UserName}");
                int count = await SqlSugarHelper.Db.Insertable<UserModel>(entity).ExecuteCommandAsync();

                if(count > 0)
                {
                    UserList = QueryTable();
                    MessageBox.Show("添加成功");
                }

            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        [RelayCommand]
        private void DeleteUser(UserModel user)
        {
            if (!HasPermiss())
            {
                return;
            }
            if(UserSession.CurrentUser.UserName == user.UserName)
            {
                MessageBox.Show("不能删除自己");
                return;
            }

            var res = MessageBox.Show("确定要删除吗", "警告", MessageBoxButton.OKCancel);
            if (res == MessageBoxResult.OK)
            {
                int count = SqlSugarHelper.Db.Deleteable<UserModel>().Where(e => e.Id == user.Id).ExecuteCommand();
                if (count > 0)
                {
                    MessageBox.Show("Delete success");
                    UserList = QueryTable();

                }
            }
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        [RelayCommand]
        private async void EditUser(UserModel user)
        {
            if (!HasPermiss())
            {
                return;
            }

            //在对话框中，你实际上是在操作这个副本，而不是原始对象。这样可以确保原始数据不受影响，直到用户确认修改。
            var userTemplate = user.DeepClone();

            var view = new UserOperateDialogView()
            {
                DataContext = new UpdateViewModel<UserModel>(userTemplate)
            };
            var result = (bool)await DialogHost.Show(view, "ShellDialog");
            if (result)
            {
                //Console.WriteLine($"Name:{entity.UserName}");

                user.UserName = userTemplate.UserName;
                user.Password = userTemplate.Password;
                user.Role = userTemplate.Role;
                user.UpDateTime = DateTime.Now;
                
                int count = await SqlSugarHelper.Db.Updateable<UserModel>(user).ExecuteCommandAsync();

                if (count > 0)
                {
                    UserList = QueryTable();
                    MessageBox.Show("edit success");
                }
                else
                {
                    MessageBox.Show("edit fail");

                }

            }
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        [RelayCommand]
        private void QueryUser()
        {
            UserList = QueryTable();
        }

        /// <summary>
        /// 窗口初始化时加载table数据
        /// </summary>
        [RelayCommand]
        private void WindowLoaded()
        {
            UserList = QueryTable();
        }

        private List<UserModel> QueryTable() => SqlSugarHelper.Db.Queryable<UserModel>().ToList();
    }
}