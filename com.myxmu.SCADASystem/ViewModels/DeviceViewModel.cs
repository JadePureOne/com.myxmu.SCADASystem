using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.myxmu.SCADASystem.Services;
using Common.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Model;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class DeviceViewModel:ObservableObject
    {

        [ObservableProperty]
        private ScadaReadDataModel _scadaReadData = new();

        [ObservableProperty]
        string _logContent = string.Empty;

        [ObservableProperty]
        List<string> _logList=new List<string>();

        private readonly ILogger _logger;

        public readonly GlobalConfig GlobalConfig;
        public readonly UserSession userSession;

        public DeviceViewModel(GlobalConfig globalConfig, UserSession userSession,ILogger<DeviceViewModel> log)
        {
            GlobalConfig = globalConfig;
            this.userSession = userSession;
            LogContent = $"程序运行中...{Environment.NewLine}程序已启动...{Environment.NewLine}";
            _logger = log;
            _logger.LogInformation("DeviceViewModel 初始化完成");
        }

        [RelayCommand]
        void ClearLog()
        {
            LogContent = string.Empty;
        }

        bool CanToggleCollection()=> GlobalConfig.PlcConnected;


        // TODO: 检查命令是否可以执行。
        [RelayCommand(CanExecute =nameof(CanToggleCollection))]
        void ToggleCollection(string workStation)
        {
            if (!GlobalConfig.PlcConnected)
            {
                LogContent += "PLC未连接或连接异常" + Environment.NewLine;
                _logger.LogError("PLC未连接或连接异常");
                userSession.ShowMessageBox("PLC未连接或连接异常");
                return;
            }

            var readAddress = GlobalConfig.ReadEntities.FirstOrDefault(x => x.En == workStation);

            
            if (readAddress ==null)
            {
                userSession.ShowMessageBox("找不到对应读取地址");
                LogContent += "找不到对应的读取地址" + Environment.NewLine;
                _logger.LogError("找不到对应读取地址");
                return;
            }

            var value=(bool)ScadaReadData.GetType().GetProperty(readAddress.En)?.GetValue(ScadaReadData);
            //写入PLC
            var res = GlobalConfig.Plc.Write(readAddress.Address, value);

            if (res.IsSuccess)
            {
                //userSession.ShowMessageBox("指令执行成功");
                LogContent += $"写入{workStation} 地址{readAddress.Address} 写入值:{value} {Environment.NewLine}";
                _logger.LogInformation($"写入{workStation} 地址{readAddress.Address} 写入值:{value}");

            }
        }

        [RelayCommand]
        private void WriteDeviceControl(string paramsName)
        {
            if (!GlobalConfig.PlcConnected)
            {
                LogContent += "PLC未连接或连接异常" + Environment.NewLine;
                _logger.LogError("PLC未连接或连接异常");
                userSession.ShowMessageBox("PLC未连接或连接异常");
                return;
            }

            var readAddress=GlobalConfig.ReadEntities.FirstOrDefault(x => x.En == paramsName)?.Address;

            if (string.IsNullOrEmpty(readAddress)) {
                userSession.ShowMessageBox("找不到对应读取地址");
                LogContent += "找不到对应的读取地址" + Environment.NewLine;
                _logger.LogError("找不到对应读取地址");
                return;
            }

            //写入PLC
            var res=GlobalConfig.Plc.Write(readAddress,true );

            if (res.IsSuccess) {
                //userSession.ShowMessageBox("指令执行成功");
                LogContent += $"写入{paramsName} 地址{readAddress} 写入值:true{Environment.NewLine}";
                _logger.LogInformation($"写入{paramsName} 地址{readAddress} 写入值:true");

            }

        }
    }
}
