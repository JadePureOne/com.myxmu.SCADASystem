using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Castle.Core.Logging;
using com.myxmu.SCADASystem.Messages;
using com.myxmu.SCADASystem.Models;
using com.myxmu.SCADASystem.Services;
using Common.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class MainViewModel :ObservableObject
    {
        //测试日志和参数
        private readonly ILogger<MainViewModel> _logger;
        private AppSetting _appSetting;

        public UserSession UserSession { get; }
        public List<MenuModel> MenuEntities { get; set; } = new();

        public MainViewModel(UserSession userSession, ILogger<MainViewModel> logger,IOptionsSnapshot<AppSetting> appSetting)
        {
            UserSession = userSession;
            _logger = logger;
            _appSetting = appSetting.Value;
            InitData();
        }

        private void InitData()
        {
            MenuEntities=SqlSugarHelper.Db.Queryable<MenuModel>().ToList();
            _logger.LogInformation("test log data");
            _logger.LogInformation(_appSetting.PlcParam.PlcIp);

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

        [RelayCommand]
        void ChangeUser(MenuModel menu)
        {
            // 用户登出的消息
            WeakReferenceMessenger.Default.Send(new LoginOutMsg(UserSession.CurrentUser));
        }
    }
}
