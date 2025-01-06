using com.myxmu.SCADASystem.ViewModels;
using com.myxmu.SCADASystem.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Media;
using com.myxmu.SCADASystem.Services;
using ControlzEx.Theming;

namespace com.myxmu.SCADASystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 向外暴露，Gets the current <see cref="App"/> instance in use
        /// </summary>
        public static App current = (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; set; }

        public App()
        {
            Services = ConfigureServices();
            //this.InitializeComponent();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 切换主题，变为 AliceBlue
            ThemeManager.Current.ChangeTheme(this, ThemeManager.Current.AddTheme(
                RuntimeThemeGenerator.Current.GenerateRuntimeTheme("Light", Colors.AliceBlue)
            ));

            Services.GetService<ShellView>()?.Show();
        }

        /// <summary>
        /// 配置服务,做所有依赖注入相关的配置
        /// </summary>
        /// <returns></returns>
        private static IServiceProvider ConfigureServices()
        {

            ServiceCollection services = new();
            
            //inject services
            services.AddSingleton<UserSession>();

            // 注册视图和视图模型
            services.AddSingleton<ShellView>();
            services.AddSingleton<ShellViewModel>();

            services.AddSingleton<LoginView>();
            services.AddSingleton<LoginViewModel>();

            services.AddSingleton<MainView>();
            services.AddSingleton<MainViewModel>();

            services.AddSingleton<IndexView>();
            services.AddSingleton<IndexViewModel>();

            services.AddSingleton<DeviceView>();
            services.AddSingleton<DeviceViewModel>();

            return services.BuildServiceProvider();

        }
    }

}
