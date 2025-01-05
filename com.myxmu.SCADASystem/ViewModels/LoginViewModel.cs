using com.myxmu.SCADASystem.Messages;
using com.myxmu.SCADASystem.Models;
using com.myxmu.SCADASystem.Services;
using Common.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections;
using System.ComponentModel;
using System.Windows;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class LoginViewModel : ObservableObject, INotifyDataErrorInfo
    {
        public UserSession UserSession { get; }

        //容器会查找已注册的 UserSession，找到后，容器会自动创建或获取这个 UserSession 实例，并将其传递给 LoginViewModel 的构造函数。
        public LoginViewModel(UserSession userSession)
        {
            UserSession = userSession;
            // 初始化时主动触发校验
            ValidateProperty(nameof(UserName), UserName);
            ValidateProperty(nameof(Password), Password);
        }

        private string _userName = "Admin";
        private string _password = "123456";

        private readonly Dictionary<string, List<string>> _errors = new();

        public string UserName
        {
            get => _userName;
            set
            {
                SetProperty(ref _userName, value);
                ValidateProperty(nameof(UserName), value);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                ValidateProperty(nameof(Password), value);
            }
        }

        public bool CanLogin => !HasErrors;

        private void ValidateProperty(string propertyName, string value)
        {
            if (_errors.ContainsKey(propertyName))
                _errors.Remove(propertyName);

            switch (propertyName)
            {
                case nameof(UserName):
                    if (string.IsNullOrWhiteSpace(value))
                        AddError(propertyName, "必填");
                    else if (value.Length < 3)
                        AddError(propertyName, "用户名长度必须大于等于3个字符");
                    break;

                case nameof(Password):
                    if (string.IsNullOrWhiteSpace(value))
                        AddError(propertyName, "必填");
                    else if (value.Length < 6)
                        AddError(propertyName, "密码长度必须大于等于6个字符");
                    break;
            }

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(CanLogin));
        }

        private void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = new List<string>();

            _errors[propertyName].Add(error);
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return string.IsNullOrEmpty(propertyName) ? null : _errors.GetValueOrDefault(propertyName);
        }

        public bool HasErrors => _errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        [RelayCommand]
        private void Login()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("密码或用户名为空");
                return;
            }

            var userList = SqlSugarHelper.Db.Queryable<UserModel>().Where(e => e.UserName == UserName && e.Password == Password)
                .ToList();

            if (userList.Count > 0)
            {
                UserSession.CurrentUser = userList[0];

                //MessageBox.Show("login success");

                //进行页面跳转 msg notify
                WeakReferenceMessenger.Default.Send(new LoginMsg(UserSession.CurrentUser));
            }
            else
            {
                MessageBox.Show("Login false,check name or pwd");
            }
        }
    }
}