using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class FormulaViewModel:ObservableObject
    {

        [RelayCommand]
        void QueryFormula() { }

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
