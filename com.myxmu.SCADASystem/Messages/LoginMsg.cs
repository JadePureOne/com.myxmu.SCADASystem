using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.myxmu.SCADASystem.Models;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace com.myxmu.SCADASystem.Messages
{
    public class LoginMsg:ValueChangedMessage<UserModel>
    {
        public LoginMsg(UserModel value) : base(value)
        {
        }
    }
}
