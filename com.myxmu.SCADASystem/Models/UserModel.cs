using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace com.myxmu.SCADASystem.Models
{
    [SugarTable("user")]
    public class UserModel:EntityBase
    {
        private string _userName;

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _passWord;

        public string Password
        {
            get => _passWord;
            set => SetProperty(ref _passWord, value);
        }

        /// <summary>
        /// 0 代表管理员，1代表普通用户
        /// </summary>
        private int _role;

        public int Role
        {
            get => _role;
            set => SetProperty(ref _role, value);
        }
    }
}
