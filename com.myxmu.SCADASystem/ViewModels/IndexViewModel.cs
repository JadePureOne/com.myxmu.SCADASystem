using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class IndexViewModel:ObservableObject
    {

        private readonly GlobalConfig _globalConfig;

        public IndexViewModel(GlobalConfig globalConfig)
        {
            _globalConfig = globalConfig;
            _globalConfig.InitPlcServer();
            _globalConfig.StartCollectionAsync();
        }
    }
}
