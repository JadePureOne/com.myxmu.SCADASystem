using System.Windows;
using com.myxmu.SCADASystem.Models;
using com.myxmu.SCADASystem.Services;
using com.myxmu.SCADASystem.Views;
using Common.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        /// 添加用户
        /// </summary>
        [RelayCommand]
        private async void AddUser(UserViewModel user)
        {
            if (UserSession.CurrentUser.Role != 0)
            {
                MessageBox.Show("no permission");
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
        private void DeleteUser(UserViewModel user)
        {
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        [RelayCommand]
        private void ModifyUser(UserViewModel user)
        {
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