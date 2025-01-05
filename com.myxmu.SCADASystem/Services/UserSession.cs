using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.myxmu.SCADASystem.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace com.myxmu.SCADASystem.Services
{
    public class UserSession:ObservableObject
    {
        private UserModel _user=new(){UserName = "test",Password = "123456"};

        public UserModel CurrentUser
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
    }
}
