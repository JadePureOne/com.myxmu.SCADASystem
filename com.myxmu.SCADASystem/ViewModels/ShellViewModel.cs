using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.myxmu.SCADASystem.Models;
using Common.Helpers;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class ShellViewModel:ObservableObject
    {
        public ShellViewModel()
        {
            InitData();
        }

        /// <summary>
        /// 初次进行数据库 CodeFirst 创建表及初始化一些数据
        /// </summary>
        private void InitData()
        {
            if (false)
            {
                //1. 建库
                //SqlSugarHelper.Db.DbMaintenance.CreateDatabase();

                //2. 建表
                //SqlSugarHelper.Db.CodeFirst.InitTables<UserModel>();
                SqlSugarHelper.Db.CodeFirst.InitTables<MenuModel>();
            }

            //insert menu data
            if (false)
            {
                List<MenuModel> menuList =
                [
                    new()
                    {
                        MenuName = "首页",
                        Icon = "Home",
                        View = "IndexView",
                        Sort = 1,
                    },
                    new()
                    {
                        MenuName = "设备总控",
                        Icon = "Devices",
                        View = "DeviceView",
                        Sort = 2,
                    },
                    new()
                    {
                        MenuName = "配方管理",
                        Icon = "AirFilter",
                        View = "FormulaView",
                        Sort = 3,
                    },
                    new ()
                    {
                        MenuName = "参数管理",
                        Icon = "AlphabetCBoxOutline",
                        View = "ParamsView",
                        Sort = 4,
                    },
                    new ()
                    {
                        MenuName = "数据查询",
                        Icon = "DataUsage",
                        View = "DataQueryView",
                        Sort = 5,
                    },
                    new ()
                    {
                        MenuName = "数据趋势",
                        Icon = "ChartFinance",
                        View = "ChartView",
                        Sort = 6,
                    },
                    new ()
                    {
                        MenuName = "报表管理",
                        Icon = "FileReport",
                        View = "ReportView",
                        Sort = 7,
                    },
                    new ()
                    {
                        MenuName = "日志管理",
                        Icon = "NotebookOutline",
                        View = "LogView",
                        Sort = 8,
                    },
                    new ()
                    {
                        MenuName = "用户管理",
                        Icon = "UserMultipleOutline",
                        View = "UserView",
                        Sort = 9,
                    }
                ];
                SqlSugarHelper.Db.Insertable(menuList).ExecuteCommand();

            }

            //insert user data
            if (false)
            {
                List<UserModel> userlist =
                [
                    new ()
                    {
                        UserName = "Admin",
                        Password = "123456",
                        Role = 0
                    },
                    new ()
                    {
                        UserName = "test",
                        Password = "123456",
                        Role = 1
                    },
                    new ()
                    {
                        UserName = "test123",
                        Password = "123456",
                        Role = 1
                    }
                ];
                SqlSugarHelper.Db.Insertable(userlist).ExecuteCommand();
            }
        }
    }
}
