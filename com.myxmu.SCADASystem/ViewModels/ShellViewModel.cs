﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
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
                //SqlSugarHelper.Db.CodeFirst.InitTables<MenuModel>();
                //SqlSugarHelper.Db.CodeFirst.InitTables<ScadaReadDataModel>();
                SqlSugarHelper.Db.CodeFirst.InitTables<FormulaEntity>();
            }

            //insert rcp data
            if (false)
            {
                // 插入数据 
                var formulaEntityList = new List<FormulaEntity>();

                for (int i = 0; i < 10; i++)
                {
                    var formulaEntity = new FormulaEntity()
                    {
                        Name = "配方" + i,
                        Description = "配方描述" + i,
                        IsSelected = false,
                        DegreasingSetPressureUpperLimit
                            = GetRandomFloat(0.5f, 5.0f),
                        DegreasingSetPressureLowerLimit
                            = GetRandomFloat(6.0f, 9.0f),
                        RoughWashingSprayPumpOverloadUpperLimit
                            = GetRandomFloat(1.0f, 4.0f),
                        RoughWashingLevelLowerLimit
                            = GetRandomFloat(0.8f, 3.5f),
                        CeramicCoatingSprayPumpOverloadUpperLimit
                            = GetRandomFloat(4.0f, 7.0f),
                        FineWashingSprayPumpOverloadUpperLimit
                            = GetRandomFloat(1.2f, 4.5f),
                        FineWashingLevelLowerLimit
                            = GetRandomFloat(40.0f, 80.0f),
                        MoistureFurnaceTemperatureUpperLimit
                            = GetRandomFloat(120.0f, 200.0f),
                        MoistureFurnaceTemperatureLowerLimit
                            = GetRandomFloat(120.0f, 200.0f),
                        CoolingRoomCentrifugalFanOverloadUpperLimit
                            = GetRandomFloat(120.0f, 200.0f),
                        CuringOvenTemperatureUpperLimit
                            = GetRandomFloat(120.0f, 200.0f),
                        CuringOvenTemperatureLowerLimit
                            = GetRandomFloat(120.0f, 200.0f),
                        ConveyorSetSpeed = GetRandomFloat(120.0f, 200.0f),
                        ConveyorSetFrequency = GetRandomFloat(120.0f, 200.0f),

                        CreateDateTime = DateTime.Now,
                        UpDateTime = DateTime.Now,
                    };
                    formulaEntityList.Add(formulaEntity);
                }

                SqlSugarHelper.Db.Insertable(formulaEntityList).ExecuteCommand();
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
                    //new ()
                    //{
                    //    MenuName = "报表管理",
                    //    Icon = "FileReport",
                    //    View = "ReportView",
                    //    Sort = 7,
                    //},
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

            //insert scada data
            if (false)
            {
                // 插入一些用户表里面的数据 root test test123

                var scadaReadDatalist = new List<ScadaReadDataModel>();

                for (int i = 0; i < 100; i++)
                {

                    var scadaReadData = new ScadaReadDataModel()
                    {
                        DegreasingSprayPumpPressure = GetRandomFloat(0.5f, 5.0f),
                        DegreasingPhValue = GetRandomFloat(6.0f, 9.0f),
                        RoughWashSprayPumpPressure = GetRandomFloat(1.0f, 4.0f),
                        PhosphatingSprayPumpPressure = GetRandomFloat(0.8f, 3.5f),
                        PhosphatingPhValue = GetRandomFloat(4.0f, 7.0f),
                        FineWashSprayPumpPressure = GetRandomFloat(1.2f, 4.5f),
                        MoistureFurnaceTemperature = GetRandomFloat(40.0f, 80.0f),
                        CuringFurnaceTemperature = GetRandomFloat(120.0f, 200.0f),
                        FactoryTemperature = GetRandomFloat(15.0f, 35.0f),
                        FactoryHumidity = GetRandomFloat(30.0f, 80.0f),
                        ProductionCount = GetRandomFloat(0, 1000),
                        DefectiveCount = GetRandomFloat(0, 50),
                        ProductionPace = GetRandomFloat(0.5f, 2.0f),
                        AccumulatedAlarms = GetRandomFloat(0, 20),
                        CreateDateTime = DateTime.Now.AddDays(GetRandomFloat(1f, 10f)),
                        UpDateTime = DateTime.Now.AddDays(GetRandomFloat(1f, 10f))
                    };

                    scadaReadDatalist.Add(scadaReadData);

                }


                SqlSugarHelper.Db.Insertable(scadaReadDatalist).ExecuteCommand();
            }
        }

        private float GetRandomFloat(float min, float max)
        {
            return (float)(new Random().NextDouble() * (max - min) + min);
        }
    }
}
