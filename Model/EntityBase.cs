using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SqlSugar;

namespace Model
{
    public class EntityBase:ObservableObject
    {
        private int _id;

        [SugarColumn(IsPrimaryKey = true,IsIdentity = true)]
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private DateTime _createTime=DateTime.Now;

        public DateTime CreateDateTime
        {
            get => _createTime;
            set => SetProperty(ref _createTime, value);
        }

        private DateTime _upDateTime = DateTime.Now;

        public DateTime UpDateTime
        {
            get => _upDateTime;
            set => SetProperty(ref _upDateTime, value);
        }

    }
}
