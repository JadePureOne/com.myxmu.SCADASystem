using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace com.myxmu.SCADASystem.Models
{
    [SugarTable("menu")]
    public class MenuModel:EntityBase
    {
        private string _menuName;

        public string MenuName
        {
            get => _menuName;
            set => SetProperty(ref _menuName, value);
        }

        private string _icon;

        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private string _view;

        public string View
        {
            get => _view;
            set => SetProperty(ref _view, value);
        }

        private int _sort;

        public int Sort
        {
            get => _sort;
            set => SetProperty(ref _sort, value);
        }
    }
}
