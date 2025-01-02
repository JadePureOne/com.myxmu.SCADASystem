using com.myxmu.SCADASystem.ViewModels;
using com.myxmu.SCADASystem.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

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
        public IServiceProvider Service { get; set; }

        public App()
        {
            Service = ConfigureServices();
            //this.InitializeComponent();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Service.GetService<ShellView>()?.Show();
        }

        /// <summary>
        /// 配置服务,做所有依赖注入相关的配置
        /// </summary>
        /// <returns></returns>
        private static IServiceProvider ConfigureServices()
        {

            ServiceCollection services = new();

            // 注册视图和视图模型
            services.AddSingleton<ShellView>();
            services.AddSingleton<ShellViewModel>();

            services.AddSingleton<LoginView>();
            services.AddSingleton<LoginViewModel>();

            return services.BuildServiceProvider();

        }
    }

}
