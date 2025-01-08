using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniExcelLibs.Attributes;

namespace com.myxmu.SCADASystem.Models
{
    [SugarTable("scadaReadData")]

    public class ScadaReadDataModel : EntityBase
    {

        #region 读取参数

        private float _degreasingSprayPumpPressure;
        /// <summary>
        /// 脱脂喷淋泵压力值
        /// </summary>
        [ExcelColumnName("脱脂喷淋泵压力值")]
        public float DegreasingSprayPumpPressure
        {
            get => _degreasingSprayPumpPressure;
            set => SetProperty(ref _degreasingSprayPumpPressure, value);
        }

        private float _degreasingPhValue;
        /// <summary>
        /// 脱脂pH值
        /// </summary>
        [ExcelColumnName("脱脂pH值")]
        public float DegreasingPhValue
        {
            get => _degreasingPhValue;
            set => SetProperty(ref _degreasingPhValue, value);
        }

        private float _roughWashSprayPumpPressure;
        /// <summary>
        /// 粗洗喷淋泵压力值
        /// </summary>
        [ExcelColumnName("粗洗喷淋泵压力值")]
        public float RoughWashSprayPumpPressure
        {
            get => _roughWashSprayPumpPressure;
            set => SetProperty(ref _roughWashSprayPumpPressure, value);
        }

        private float _phosphatingSprayPumpPressure;
        /// <summary>
        /// 陶化喷淋泵压力值
        /// </summary>
        [ExcelColumnName("陶化喷淋泵压力值")]
        public float PhosphatingSprayPumpPressure
        {
            get => _phosphatingSprayPumpPressure;
            set => SetProperty(ref _phosphatingSprayPumpPressure, value);
        }

        private float _phosphatingPhValue;
        /// <summary>
        /// 陶化pH值
        /// </summary>
        [ExcelColumnName("陶化pH值")]
        public float PhosphatingPhValue
        {
            get => _phosphatingPhValue;
            set => SetProperty(ref _phosphatingPhValue, value);
        }
        private float _fineWashSprayPumpPressure;
        /// <summary>
        /// 精洗喷淋泵压力值
        /// </summary>
        [ExcelColumnName("精洗喷淋泵压力值")]
        public float FineWashSprayPumpPressure
        {
            get => _fineWashSprayPumpPressure;
            set => SetProperty(ref _fineWashSprayPumpPressure, value);
        }

        private float _moistureFurnaceTemperature;
        /// <summary>
        /// 水分炉测量温度
        /// </summary>
        [ExcelColumnName("水分炉测量温度")]
        public float MoistureFurnaceTemperature
        {
            get => _moistureFurnaceTemperature;
            set => SetProperty(ref _moistureFurnaceTemperature, value);
        }

        private float _curingFurnaceTemperature;
        /// <summary>
        /// 固化炉测量温度
        /// </summary>
        [ExcelColumnName("固化炉测量温度")]
        public float CuringFurnaceTemperature
        {
            get => _curingFurnaceTemperature;
            set => SetProperty(ref _curingFurnaceTemperature, value);
        }

        private float _factoryTemperature;
        /// <summary>
        /// 厂内温度
        /// </summary>
        [ExcelColumnName("厂内温度")]
        public float FactoryTemperature
        {
            get => _factoryTemperature;
            set => SetProperty(ref _factoryTemperature, value);
        }

        private float _factoryHumidity;
        /// <summary>
        /// 厂内湿度
        /// </summary>
        [ExcelColumnName("厂内湿度")]
        public float FactoryHumidity
        {
            get => _factoryHumidity;
            set => SetProperty(ref _factoryHumidity, value);
        }

        private float _productionCount;
        /// <summary>
        /// 生产计数
        /// </summary>
        [ExcelColumnName("生产计数")]
        public float ProductionCount
        {
            get => _productionCount;
            set => SetProperty(ref _productionCount, value);
        }

        private float _defectiveCount;
        /// <summary>
        /// 不良计数
        /// </summary>
        [ExcelColumnName("不良计数")]
        public float DefectiveCount
        {
            get => _defectiveCount;
            set => SetProperty(ref _defectiveCount, value);
        }

        private float _productionPace;
        /// <summary>
        /// 生产节拍
        /// </summary>
        [ExcelColumnName("生产节拍")]
        public float ProductionPace
        {
            get => _productionPace;
            set => SetProperty(ref _productionPace, value);
        }

        private float _accumulatedAlarms;
        /// <summary>
        /// 累计报警
        /// </summary>
        [ExcelColumnName("累计报警")]
        public float AccumulatedAlarms
        {
            get => _accumulatedAlarms;
            set => SetProperty(ref _accumulatedAlarms, value);
        }

        #endregion
    }
}
