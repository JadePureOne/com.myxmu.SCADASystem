using Common.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class FormulaViewModel:ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<FormulaEntity> _formulaList = new();

        [RelayCommand]
        void QueryFormula() {
            FormulaList.Clear();
            SqlSugarHelper.Db.Queryable<FormulaEntity>()
                    .OrderBy(f => f.CreateDateTime, SqlSugar.OrderByType.Desc)
                    .ToList()
                    .ForEach(e=>FormulaList.Add(e));
        }

        [RelayCommand]
        void NewFormulaCommand() { }

        [RelayCommand]
        void SaveFormula() { }
        [RelayCommand]
        void DeleteFormula() { }

        [RelayCommand]
        void DownloadFormula() { }
    }
}
