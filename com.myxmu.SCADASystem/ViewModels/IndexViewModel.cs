using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using Model;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class IndexViewModel:ObservableObject
    {
        [ObservableProperty]
        private ScadaReadDataModel _scadaReadData = new();

        private CancellationTokenSource _cts = new();

        private readonly GlobalConfig _globalConfig;

        public IndexViewModel(GlobalConfig globalConfig)
        {
            _globalConfig = globalConfig;
            _globalConfig.InitPlcServer();
            _globalConfig.StartCollectionAsync();

            //采集完后，启动个实时读取 将globalConfig.ReadDataDic绑定到scadaReadDataModel
            //可以通过Reflect，因为ScadaReadDataModel属性名和字典key一模一样
            InitReadData2ScadaReadDataLoop();
        }

        private void InitReadData2ScadaReadDataLoop()
        {
            Task.Run(async() => {
                while (!_cts.Token.IsCancellationRequested) {
                    var properties = typeof(ScadaReadDataModel)
                               .GetProperties()
                               .Where(p => p.PropertyType == typeof(float) || p.PropertyType == typeof(bool));

                    while (!_cts.Token.IsCancellationRequested)
                    {
                        foreach (var property in properties)
                        {
                            try
                            {
                                if (property.PropertyType == typeof(float))
                                {
                                    var value = _globalConfig.GetValue<float>(property.Name);
                                    property.SetValue(_scadaReadData, value);
                                }
                                else if (property.PropertyType == typeof(bool))
                                {
                                    var value = _globalConfig.GetValue<bool>(property.Name);
                                    property.SetValue(_scadaReadData, value);
                                }
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine(e);
                            }
                        }

                        await Task.Delay(100);
                    }
                }
            });
        }
    }
}
