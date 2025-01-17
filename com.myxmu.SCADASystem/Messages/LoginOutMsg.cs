using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace com.myxmu.SCADASystem.Messages
{
    public class LoginOutMsg : ValueChangedMessage<UserModel>
    {
        public LoginOutMsg(UserModel value) : base(value)
        {
        }
    }
}
