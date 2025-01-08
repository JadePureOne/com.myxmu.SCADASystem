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
using SqlSugar;

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

        #region pager

        int totalDataNumber = 0;

        [ObservableProperty]
        int _pageSize = 20;

        [ObservableProperty]
        private int _totalPages = 1;

        [ObservableProperty]
        int _currentPage = 1;

        //使用[ObservableProperty] 特性 会自动生成OnxxxxChanged
        partial void OnCurrentPageChanged(int value)
        {
            Search();
        }


        [RelayCommand]
        void GoToFirstPage()
        {
            CurrentPage = 1;
        }

        [RelayCommand]
        void GoToNextPage()
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage++;
            }
        }

        [RelayCommand]
        void GoToPreviousPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
            }
        }

        [RelayCommand]
        void GoToLastPage()
        {
            CurrentPage = TotalPages;
        }

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
            //ScadaReadDataList=SqlSugarHelper.Db.Queryable<ScadaReadDataModel>().Where(x => x.CreateDateTime >= StartTime && x.CreateDateTime <= EndTime).ToList();
            ScadaReadDataList = QueryTable();
            TotalPages = (int)Math.Ceiling((double)totalDataNumber / PageSize);


        }

        /// <summary>
        /// 窗口初始化时加载table数据
        /// </summary>
        [RelayCommand]
        private void WindowLoaded()
        {
            Search();
        }

        private List<ScadaReadDataModel> QueryTable() => SqlSugarHelper.Db.Queryable<ScadaReadDataModel>().ToPageList(CurrentPage,PageSize,ref totalDataNumber);
    }
}
