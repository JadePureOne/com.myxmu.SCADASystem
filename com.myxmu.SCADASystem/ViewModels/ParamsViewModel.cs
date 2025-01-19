using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Options;
using CommunityToolkit.Mvvm.Input;
using Common.Helpers;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class ParamsViewModel:ObservableObject
    {
        private GlobalConfig _globalConfig;

        [ObservableProperty] private AppSetting appSetting;

        public ParamsViewModel(IOptionsSnapshot<AppSetting> optionsSnapshot, GlobalConfig globalConfig)
        {
            this.appSetting = optionsSnapshot.Value;
            _globalConfig = globalConfig;
        }

        [RelayCommand]
        void ToggleCollection()
        {
            //Console.WriteLine("11111", AppSetting.PlcParam.AutoCollect);
            if (AppSetting.PlcParam.AutoCollect)
            {
                _globalConfig.StopCollection();
                _globalConfig.StartCollection();
            }
            else
            {
                _globalConfig.StopCollection();
            }
            
        }
    }
}
