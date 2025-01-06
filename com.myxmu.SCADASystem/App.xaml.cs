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

            // 注入视图和视图模型
            RegisterViewsAndViewModels(services);

            // 注入服务
            services.AddSingleton<UserSession>();

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// 注册视图和视图模型
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterViewsAndViewModels(ServiceCollection services)
        {
            try
            {
                // 获取所有视图和视图模型类型
                var viewNamespace = typeof(App).Namespace + ".Views";
                var viewModelNamespace = typeof(App).Namespace + ".ViewModels";

                var viewTypes = typeof(App).Assembly.GetTypes()
                    .Where(t => t.Namespace == viewNamespace && t.Name.EndsWith("View"));
                var viewModelTypes = typeof(App).Assembly.GetTypes()
                    .Where(t => t.Namespace == viewModelNamespace && t.Name.EndsWith("ViewModel"));

                // 注入视图和视图模型
                foreach (var viewType in viewTypes)
                {
                    services.AddSingleton(viewType);
                }

                foreach (var viewModelType in viewModelTypes)
                {
                    services.AddSingleton(viewModelType);
                }
            }
            catch (Exception ex)
            {
                // 记录异常日志
                Console.WriteLine($"Error registering views and view models: {ex.Message}");
                throw;
            }
        }
    }
}
