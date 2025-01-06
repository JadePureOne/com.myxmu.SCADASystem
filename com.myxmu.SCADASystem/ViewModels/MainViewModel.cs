using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using com.myxmu.SCADASystem.Models;
using com.myxmu.SCADASystem.Services;
using Common.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class MainViewModel :ObservableObject
    {
        public UserSession UserSession { get; }
        public List<MenuModel> MenuEntities { get; set; } = new();

        public MainViewModel(UserSession userSession)
        {
            UserSession = userSession;
            InitData();
        }

        private void InitData()
        {
            MenuEntities=SqlSugarHelper.Db.Queryable<MenuModel>().ToList();
        }

        /// <summary>
        ///  发送导航消息
        /// </summary>
        /// <param name="menu"></param>
        [RelayCommand]
        void Navigation(MenuModel menu)
        {
            WeakReferenceMessenger.Default.Send(menu);
        }
    }
}
