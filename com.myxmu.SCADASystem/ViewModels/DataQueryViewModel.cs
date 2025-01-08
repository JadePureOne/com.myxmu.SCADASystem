using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.myxmu.SCADASystem.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class DataQueryViewModel : ObservableObject
    {
        [ObservableProperty]
        List<ScadaReadDataModel> _scadaReadDataList = new();

        [ObservableProperty]
        DateTime _startTime = DateTime.Now.AddDays(-30);

        [ObservableProperty]
        DateTime _endTime = DateTime.Now;
    }
}
