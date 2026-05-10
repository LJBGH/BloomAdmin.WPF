using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Windows;
using BloomAdmin.Main.DataAccess;
using BloomAdmin.Main.View;
using BloomAdmin.Main.Config;

namespace BloomAdmin.Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        private IHost _host;

        protected override void OnStartup(StartupEventArgs e)
        {
            var hostBuilder = Host.CreateDefaultBuilder(e.Args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("Config/appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    // 从配置读取连接字符串
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                    AppConfig.DbCoonString  = connectionString;

                    // 工厂：每次 CreateDbContext() 都是新实例，避免长生命周期里读到旧缓存（库外用 SSMS 改完立刻生效）
                    services.AddDbContextFactory<AppDbContext>(options => options.UseSqlServer(connectionString));

                    // 添加验证码服务
                    services.AddCaptcha();


                    // 注册其他服务（如 ViewModel）
                    services.AddTransient<LoginView>();
                    services.AddTransient<MainView>();
                });

            _host = hostBuilder.Build();
            ServiceProvider = _host.Services;

            // 启动主窗口
            var mainWindow = _host.Services.GetRequiredService<LoginView>();
            mainWindow.Show();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host?.Dispose();
            base.OnExit(e);
        }
    }
}
