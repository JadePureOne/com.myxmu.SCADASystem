using HslCommunication;
using HslCommunication.Profinet.Siemens;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiniExcelLibs;
using Model;
using System.Collections.Concurrent;
using System.IO;

namespace Common.Helpers
{
    public class GlobalConfig : IDisposable
    {
        public bool PlcConnected { get; set; }
        public SiemensS7Net Plc { get; set; }

        /// <summary>
        /// 公开一个字典，所有的取数都从这个字典里面进行获取
        /// </summary>
        public ConcurrentDictionary<string, object> ReadDataDic { get; set; } = new();

        public List<ReadEntity> ReadEntities { get; set; } = new();
        public List<WriteEntity> WriteEntities { get; set; } = new();

        private readonly IOptionsSnapshot<AppSetting> _options;
        private readonly ILogger<GlobalConfig> _logger;

        private CancellationTokenSource _cts = new();

        public GlobalConfig(IOptionsSnapshot<AppSetting> options, ILogger<GlobalConfig> logger)
        {
            _options = options;
            _logger = logger;
            InitPlc();
            InitExcelsAddress();
            //InitDequeue();
        }

        private void InitExcelsAddress()
        {
            var rootPath = AppDomain.CurrentDomain.BaseDirectory + "Configs\\";
            var readPath = rootPath + "TulingRead.xlsx";
            var writePath = rootPath + "TulingWrite.xlsx";

            if (!File.Exists(readPath) || !File.Exists(writePath))
            {
                _logger.LogError($"找不到读/写配置文件 xlsx{readPath}\n{writePath}");

                return;
            }

            try
            {
                ReadEntities = MiniExcel.Query<ReadEntity>(readPath)
                    .Where(x => !string.IsNullOrEmpty(x.Address))
                    .ToList();
                WriteEntities = MiniExcel.Query<WriteEntity>(writePath)
                    .Where(x => !string.IsNullOrEmpty(x.Address))
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"MiniExcel 读取文件异常{e.Message}");
            }
        }

        private void InitPlc()
        {
            Plc = new SiemensS7Net(SiemensPLCS.S1200, _options.Value.PlcParam.PlcIp);
            Plc.Port = _options.Value.PlcParam.PlcPort;
            Plc.Slot = _options.Value.PlcParam.PlcSlot;
            Plc.Rack = _options.Value.PlcParam.PlcRack;
            Plc.ConnectTimeOut = _options.Value.PlcParam.PlcConnectTimeOut;
        }

        /// <summary>
        /// 做PLC连接操作
        /// </summary>
        public async Task InitPlcServer()
        {
            try
            {
                var opResult = await Plc.ConnectServerAsync();
                PlcConnected = opResult.IsSuccess;

                if (!opResult.IsSuccess)
                {
                    _logger.LogError($"PLC 连接失败:{_options.Value.PlcParam.PlcIp}:{_options.Value.PlcParam.PlcPort}");
                }
            }
            catch (Exception e)
            {
                PlcConnected = false;
                _logger.LogError($"PLC 连接异常：{e.Message}");
            }
        }

        /// <summary>
        /// 开始采集，写入读取字典 ReadDataDic
        /// </summary>
        /// <param name="externalToken"></param>
        /// <returns></returns>
        public Task StartCollectionAsync(CancellationToken? externalToken = null)
        {
            StopCollection();

            // 令牌创建
            _cts = CancellationTokenSource.CreateLinkedTokenSource(
                externalToken ?? CancellationToken.None);

            return Task.Run(async () =>
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    try
                    {
                        // 批量读取赋值给字典
                        await UpdateControlData();
                        await UpdateMonitorData();
                        await UpdateProcessData();

                        await Task.Delay(_options.Value.PlcParam.PlcCycleInterval, _cts.Token);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message);
                    }
                }
            }, _cts.Token);
        }

        private async Task UpdateProcessData()
        {
            await UpdatePlcToReadDataDic<float>("Data", "DBD");
        }

        private async Task UpdateMonitorData()
        {
            await UpdatePlcToReadDataDic<bool>("Monitor", "DBX");
        }

        private async Task UpdateControlData()
        {
            await UpdatePlcToReadDataDic<bool>("Control", "DBX");
        }

        private async Task UpdatePlcToReadDataDic<T>(string module, string addressType)
        {
            try
            {
                var addresses = ReadEntities.Where(x => x.Module == module
                && x.Address.Contains(addressType)).ToList();

                if ((addresses.Count < 1))
                {
                    return;
                }

                OperateResult<T[]> result;

                if (typeof(T) == typeof(bool))
                {
                    result = await Plc
                        .ReadBoolAsync(addresses
                        .First().Address, (ushort)addresses.Count) as OperateResult<T[]>;
                }
                else if (typeof(T) == typeof(float))
                {
                    result = await Plc
                        .ReadFloatAsync(addresses
                            .First().Address, (ushort)addresses.Count) as OperateResult<T[]>;
                }
                else
                {
                    _logger.LogError("不支持传入的类型");

                    return;
                }

                // 将 result 结果放入到字典中
                if (!result.IsSuccess)
                {
                    _logger.LogError("数据读取失败");

                    return;
                }

                for (int i = 0; i < addresses.Count; i++)
                {
                    if (ReadDataDic.ContainsKey(addresses[i].En))
                    {
                        ReadDataDic[addresses[i].En] = result.Content[i];
                    }
                    else
                    {
                        ReadDataDic.TryAdd(addresses[i].En, result.Content[i]);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public void StopCollection()
        {
            try
            {
                if (_cts != null)
                {
                    _cts.Cancel();
                    _cts.Dispose();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }


        /// <summary>
        /// 获取实时字典数值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetValue<T>(string key)
        {
            if (ReadDataDic.TryGetValue(key, out object value))
            {
                return (T)value;
            }

            return default;
        }

        public void Dispose()
        {
        }
    }
}