using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.myxmu.SCADASystem.Models;
using Common.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class MainViewModel :ObservableObject
    {
        public List<MenuModel> MenuEntities { get; set; } = new();

        public MainViewModel()
        {
            InitData();
        }

        private void InitData()
        {
            MenuEntities=SqlSugarHelper.Db.Queryable<MenuModel>().ToList();
        }
    }
}
