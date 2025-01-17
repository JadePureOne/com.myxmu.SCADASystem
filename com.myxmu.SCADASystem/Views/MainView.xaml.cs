using com.myxmu.SCADASystem.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Windows.Controls;
using Model;
using System.Diagnostics;

namespace com.myxmu.SCADASystem.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            InitData();
            InitRegisterMessage();
        }

        private void InitData()
        {
            this.DataContext = App.current.Services.GetService<MainViewModel>();
        }

        /// <summary>
        /// 注册跳转页面，当点击时
        /// </summary>
        private void InitRegisterMessage()
        {

            // 初始化默认页面
            Page.Content = App.current.Services.GetService<IndexView>();

            // 注册消息
            WeakReferenceMessenger.Default.Register<MenuModel>(this, (sender, arg) =>
            {
                // 获取当前程序集
                Assembly assembly = Assembly.GetExecutingAssembly();

                // 根据消息中的 View 名称动态获取类型
                var typeName = $"{assembly.GetName().Name}.Views.{arg.View}";
                var type = assembly.GetType(typeName);

                if (type != null)
                {
                    // 使用非泛型版本的 GetService 方法
                    var view = App.current.Services.GetService(type);
                    if (view != null)
                    {
                        // 设置 Page.Content 为动态加载的视图
                        Page.Content = view;
                    }
                    else
                    {
                        // 处理服务未找到的情况
                        Debug.WriteLine($"服务未找到：{typeName}");
                    }
                }
                else
                {
                    // 处理类型未找到的情况
                    Debug.WriteLine($"类型未找到：{typeName}");
                }
            });

        }
    }
}