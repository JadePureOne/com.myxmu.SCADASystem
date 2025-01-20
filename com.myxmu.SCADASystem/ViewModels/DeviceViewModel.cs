using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.myxmu.SCADASystem.Services;
using Common.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class DeviceViewModel:ObservableObject
    {

        [ObservableProperty]
        private ScadaReadDataModel _scadaReadDataModel = new();

        public readonly GlobalConfig GlobalConfig;
        public readonly UserSession userSession;

        public DeviceViewModel(GlobalConfig globalConfig, UserSession userSession)
        {
            GlobalConfig = globalConfig;
            this.userSession = userSession;
        }

        [RelayCommand]
        private void WriteDeviceControl(string paramsName)
        {
            if (!GlobalConfig.PlcConnected)
            {
                userSession.ShowMessageBox("PLC未连接或连接异常");
                return;
            }

            var readAddress=GlobalConfig.ReadEntities.FirstOrDefault(x => x.En == paramsName)?.Address;

            if (string.IsNullOrEmpty(readAddress)) {
                userSession.ShowMessageBox("找不到对应读取地址");
                return;
            }

            //写入PLC
            var res=GlobalConfig.Plc.Write(readAddress,true );

            if (res.IsSuccess) {
                //userSession.ShowMessageBox("指令执行成功");
            }

        }
    }
}
