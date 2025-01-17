using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Model;
using Common.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FastReport.Export.Pdf;
using FastReport;
using MiniExcelLibs;
using SqlSugar;
using System.Data;
using com.myxmu.SCADASystem.Services;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class DataQueryViewModel : ObservableObject
    {
        [ObservableProperty]
        List<ScadaReadDataModel> _scadaReadDataList = new();

        private readonly UserSession _userSession;

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

        public DataQueryViewModel(UserSession userSession)
        {
            _userSession = userSession;
        }

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

        #region ToolBar

        [RelayCommand]
        void Reset()
        {
            StartTime = DateTime.Now.AddDays(-30);
            EndTime = DateTime.Now;
            ScadaReadDataList = SqlSugarHelper.Db.Queryable<ScadaReadDataModel>().ToList();
        }

        [RelayCommand]
        private void Search()
        {
            if (StartTime > EndTime)
            {
                _userSession.ShowMessageBox("开始时间不能超过结束时间");

                return;
            }
            //ScadaReadDataList=SqlSugarHelper.Db.Queryable<ScadaReadDataModel>().Where(x => x.CreateDateTime >= StartTime && x.CreateDateTime <= EndTime).ToList();
            ScadaReadDataList = QueryTable();
            TotalPages = (int)Math.Ceiling((double)totalDataNumber / PageSize);


        }

        [RelayCommand]
        void ExportCurrentPage()
        {
            SaveByMiniExcel<ScadaReadDataModel>(ScadaReadDataList);
        }

        [RelayCommand]
        void ExportAllPage()
        {
            var list = SqlSugarHelper.Db.Queryable<ScadaReadDataModel>().ToList();
            SaveByMiniExcel(list);
        }

        [RelayCommand]
        void DesignReport()
        {
            try
            {
                var report = new Report();
                // 加载报表设计文件
                var path = $@"{Environment.CurrentDirectory}\Configs\report.frx";
                report.Load(path);
                report.Design();

                // 导出 PDF
                var pdfExport = new PDFExport();
                pdfExport.Export(report);

                report.Dispose();
            }
            catch (Exception e)
            {
                _userSession.ShowMessageBox(e.Message);
            }
        }

        [RelayCommand]
        void PreviewReport()
        {
            var report = new Report();
            try
            {
                var dataSet = ScadaReadDataList.ConvertToDataSet();

                if (dataSet == null || dataSet.Tables.Count == 0)
                {
                    throw new Exception("DataSet is empty or not properly formed.");
                }

                var path = Path.Combine(Environment.CurrentDirectory, "Configs", "report.frx");
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("The report file was not found at the specified path.", path);
                }

                report.Load(path);

                // Register each table in the DataSet
                report.RegisterData(dataSet);


                report.Prepare();
                report.ShowPrepared();
            }
            catch (Exception e)
            {
                _userSession.ShowMessageBox(e.Message);
            }
            finally
            {
                report?.Dispose();
            }
        }

        [RelayCommand]
        void ExportReport()
        {
            try
            {
                var dateSet = ScadaReadDataList.ConvertToDataSet();
                var report = new Report();
                var path = $@"{Environment.CurrentDirectory}\Configs\report.frx";
                report.Load(path);
                report.RegisterData(dateSet);
                report.Prepare();
                // 导出 PDF
                var pdfExport = new PDFExport();
                pdfExport.Export(report);
                report.Dispose();
            }
            catch (Exception e)
            {
                _userSession.ShowMessageBox(e.Message);
            }
        }

        [RelayCommand]
        void PushData() { }

        #endregion

        void SaveByMiniExcel<T>(List<T> list)
        {
            if (list.Count < 0)
            {
                return;
            }

            var rootPath = AppDomain.CurrentDomain.BaseDirectory + "\\excels\\";
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            var excelPath = rootPath + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
            try
            {
                MiniExcel.SaveAs(excelPath, list);
                _userSession.ShowMessageBox($"导出成功--{excelPath}");
            }
            catch (Exception e)
            {
                _userSession.ShowMessageBox($"导出异常--{e.Message}");
            }
        }

        #region LifeCycle


        /// <summary>
        /// 窗口初始化时加载table数据
        /// </summary>
        [RelayCommand]
        private void WindowLoaded()
        {
            Search();
        }

        private List<ScadaReadDataModel> QueryTable() => SqlSugarHelper.Db.Queryable<ScadaReadDataModel>().ToPageList(CurrentPage, PageSize, ref totalDataNumber);

        #endregion
    }
}
