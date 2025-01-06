using com.myxmu.SCADASystem.Models;
using Common.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class UserViewModel : ObservableObject
    {
        [ObservableProperty]
        private List<UserModel> __userList = new();

        /// <summary>
        /// 添加用户
        /// </summary>
        [RelayCommand]
        private void AddUser(UserViewModel user)
        {
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