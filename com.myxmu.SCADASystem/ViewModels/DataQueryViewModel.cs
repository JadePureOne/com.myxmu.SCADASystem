using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using com.myxmu.SCADASystem.Models;
using Common.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class DataQueryViewModel : ObservableObject
    {
        [ObservableProperty]
        List<ScadaReadDataModel> _scadaReadDataList = new();

        #region 表单数据

        [ObservableProperty]
        DateTime _startTime = DateTime.Now.AddDays(-30);

        [ObservableProperty]
        DateTime _endTime = DateTime.Now;

        #endregion

        [RelayCommand]
        void Reset()
        {
            StartTime= DateTime.Now.AddDays(-30);
            EndTime= DateTime.Now;
            ScadaReadDataList = SqlSugarHelper.Db.Queryable<ScadaReadDataModel>().ToList();
        }

        [RelayCommand]
        private void Search()
        {
            if (StartTime > EndTime)
            {
                MessageBox.Show("开始时间不能超过结束时间");

                return;
            }
            ScadaReadDataList=SqlSugarHelper.Db.Queryable<ScadaReadDataModel>().Where(x => x.CreateDateTime >= StartTime && x.CreateDateTime <= EndTime).ToList();
        }

        /// <summary>
        /// 窗口初始化时加载table数据
        /// </summary>
        [RelayCommand]
        private void WindowLoaded()
        {
            ScadaReadDataList = QueryTable();
        }

        private List<ScadaReadDataModel> QueryTable() => SqlSugarHelper.Db.Queryable<ScadaReadDataModel>().ToList();
    }
}
