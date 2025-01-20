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
        private CancellationTokenSource _cts = new();

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


        [RelayCommand]
        void ToggleMock()
        {
            if (AppSetting.PlcParam.AutoMock)
            {
                StartMockData();
            }
            else
            {
                StopMockData();
            }
        }

        private void StartMockData()
        {
            _cts = new CancellationTokenSource();

            Task.Run(async () => {
                var scadaData = new ScadaReadDataModel ();
                // 获取所有浮点型的属性
                var properties = typeof(ScadaReadDataModel).GetProperties()
                .Where(p => p.PropertyType == typeof(float));
                // 获取 Bool 值
                var propertiesBool = typeof(ScadaReadDataModel).GetProperties()
                    .Where(p => p.PropertyType == typeof(bool));

                while (!_cts.Token.IsCancellationRequested)
                {
                    var random = new Random();

                    foreach (var property in properties)
                    {
                        var value = (float)(random.NextDouble() * 4 + 1);

                        var address = _globalConfig.ReadEntities.FirstOrDefault(
                            x => x.En == property.Name)?.Address;

                        if (!string.IsNullOrEmpty(address))
                        {
                            await _globalConfig.Plc.WriteAsync(address, value);
                        }
                    }

                    foreach (var property in propertiesBool)
                    {
                        var address = _globalConfig.ReadEntities.FirstOrDefault(
                            x => x.En == property.Name)?.Address;

                        if (!string.IsNullOrEmpty(address))
                        {
                            if (random.Next(0, 2) != 0)
                            {
                                await _globalConfig.Plc.WriteAsync(address, true);
                            }
                            else
                            {
                                await _globalConfig.Plc.WriteAsync(address, false);
                            }
                        }
                    }

                    await Task.Delay(1000);
                }
            }, _cts.Token);
        }

        private void StopMockData()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }
    }
}
