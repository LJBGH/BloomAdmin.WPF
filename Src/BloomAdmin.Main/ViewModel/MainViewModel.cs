using BloomAdmin.Main.Common;
using BloomAdmin.Main.DataAccess;
using BloomAdmin.Main.Model;
using BloomAdmin.Main.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Windows;

namespace BloomAdmin.Main.ViewModel
{
    public class MainViewModel : NotifyBase
    {
        public UserModel UserModel { get; set; }

        public CommandBase NavChangedCommand { get; set; }

        public CommandBase LoginOutCommand { get; set; }

        //主区域内容
        private FrameworkElement _mainContent;
        public FrameworkElement MainContent
        {
            get { return _mainContent; }
            set { _mainContent = value; this.DoNotify(); }
        }

        // 搜索框内容
        private string _searchContext;
        public string SearchContent
        {
            get { return _searchContext; }
            set { _searchContext = value; this.DoNotify(); }
        }

        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly IServiceProvider _services;

        public MainViewModel(IDbContextFactory<AppDbContext> dbContextFactory, IServiceProvider services, MainView mainView)
        {
            _services = services;
            _dbContextFactory = dbContextFactory;

            UserModel = new UserModel
            {
                Account = GlobalValues.UserInfo.Account,
                UserName = GlobalValues.UserInfo.UserName,
                Gender = GlobalValues.UserInfo.Gender,
                Email = GlobalValues.UserInfo.Email,
                Avatar = GlobalValues.UserInfo.Avatar,
            };

            // 导航栏切换事件绑定
            this.NavChangedCommand = new CommandBase
            {
                DoExecute = new Action<object>((o) => DoNavChanged(o)),
                DoCanExecute = new Func<object, bool>((o) => { return true; })
            };

            this.LoginOutCommand = new CommandBase
            {
                DoExecute = new Action<object>((o) => DoLoginOut(o)),
                DoCanExecute = new Func<object, bool>((o) => { return true; })
            };

            DoNavChanged("HomePageView");
        }

        private void DoLoginOut(object obj)
        {
            // 退出登录逻辑
            var loginView = _services.GetRequiredService<LoginView>();
            loginView.Show();

            // 关闭当前主窗口
            Application.Current.MainWindow.Close();
        }

        /// <summary>
        /// 导航栏切换处理
        /// </summary>
        private void DoNavChanged(object obj)
        {
            try
            {
                var viewType = Type.GetType($"BloomAdmin.Main.View.{obj}");
                if (viewType == null)
                    throw new ArgumentException($"View type '{obj}' not found");

                ConstructorInfo ctr = viewType.GetConstructor(Type.EmptyTypes);
                this.MainContent = (FrameworkElement)ctr.Invoke(null);

                // 由 DI 构造视图（支持带参构造函数，需在 App 中 AddTransient<该视图>）
                //this.MainContent = (FrameworkElement)_services.GetRequiredService(viewType);
            }
            catch (Exception ex)
            {
                // 记录日志
                MessageBox.Show($"导航失败: {ex.Message}");
            }
        }
    }
}
