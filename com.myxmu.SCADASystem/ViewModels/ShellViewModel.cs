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
                SqlSugarHelper.Db.DbMaintenance.CreateDatabase();

                //2. 建表
                SqlSugarHelper.Db.CodeFirst.InitTables<UserModel>();
            }

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
