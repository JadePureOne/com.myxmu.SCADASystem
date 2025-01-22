using Model;
using com.myxmu.SCADASystem.Services;
using com.myxmu.SCADASystem.Views;
using Common.Helpers;
using ControlzEx.Theming;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System.Data;
using System.Windows;
using System.Windows.Media;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace com.myxmu.SCADASystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// 向外暴露，Gets the Current <see cref="App"/> instance in use
        /// </summary>
        public static App Current = (App)Application.Current;

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

            //注入configuration
            var configuration = InjectConfigure(services);            

            // 配置Log
            ConfigureLog(services, configuration);

            //配置DBParams，改造SqlSugar
            ConfigureDB(services, configuration);

            //系统参数
            ConfigureSys(services, configuration);

            // 注入视图和视图模型
            RegisterViewsAndViewModels(services);

            //配置globalCOnfig
            services.AddSingleton<GlobalConfig>();

            // 注入服务
            services.AddSingleton<UserSession>();

            return services.BuildServiceProvider();
        }

        #region ParamsSetting

        private static void ConfigureSys(ServiceCollection services, IConfiguration configuration)
        {
            //get via IOptionsSnapshot<RootParam>.Value
            services.AddOptions()
                .Configure<AppSetting>(e => configuration.Bind(e))
                .Configure<SqlParam>(e => configuration.GetSection("SqlParam").Bind(e))
                .Configure<SystemParam>(e => configuration.GetSection("SystemParam").Bind(e))
                .Configure<PlcParam>(e => configuration.GetSection("PlcParam").Bind(e));
        }

        private static void ConfigureDB(ServiceCollection services, IConfiguration configuration)
        {
            configuration.GetSection("SqlParam");

            var parseResult = Enum.TryParse<SqlSugar.DbType>(configuration["SqlParam:DbType"], out var dbType);
            var connectionString = configuration["SqlParam:ConnectionString"];

            if (parseResult)
            {
                SqlSugarHelper.AddSqlSugarSetup(dbType, connectionString);
            }
        }

        private static void ConfigureLog(ServiceCollection services, IConfiguration configuration)
        {
            //实现单例注入和日志
            services.AddLogging(log =>
            {
                log.ClearProviders();
                log.SetMinimumLevel(LogLevel.Trace);
                log.AddNLog();
            });

            //NLog配置
            // 1. 获取日志参数 ILogger<T> T：需要记录的父类
            var nLogConfig = configuration.GetSection("NLog");
            // 2. 设置NLog配置
            LogManager.Configuration = new NLogLoggingConfiguration(nLogConfig);
        }

        /// <summary>
        /// 将 Json 文件里面的内容映射到 AppSetting 类上
        /// </summary>
        private static IConfiguration InjectConfigure(ServiceCollection services)
        {
            var cgjBuilder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory + "\\Configs")
                .AddJsonFile("appsetting.json", false, true);
            var configuration = cgjBuilder.Build();

            //单例注入
            services.AddSingleton<IConfiguration>(configuration);

            return configuration; // 返回 IConfiguration
        }

        #endregion ParamsSetting

        #region 注册视图和视图模型
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
        #endregion
    }
}