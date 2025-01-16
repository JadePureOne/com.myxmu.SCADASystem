using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.myxmu.SCADASystem.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Options;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class ParamsViewModel:ObservableObject
    {
        [ObservableProperty] private AppSetting appSetting;

        public ParamsViewModel(IOptionsSnapshot<AppSetting> optionsSnapshot)
        {
            this.appSetting = optionsSnapshot.Value;
        }
    }
}
