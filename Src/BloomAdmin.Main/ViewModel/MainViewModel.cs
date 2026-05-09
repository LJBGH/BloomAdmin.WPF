using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Windows;
using BloomAdmin.Main.Common;
using BloomAdmin.Main.DataAccess;
using BloomAdmin.Main.Model;
using BloomAdmin.Main.View;

namespace BloomAdmin.Main.ViewModel
{
    public class MainViewModel : NotifyBase
    {
        public UserModel UserModel { get; set; }
        public CommandBase NavChangedCommand { get; set; }

     
        private FrameworkElement _mainContent;
        //主区域内容
        public FrameworkElement MainContent
        {
            get { return _mainContent; }
            set { _mainContent = value; this.DoNotify(); }
        }

        private string _searchContext;
        // 搜索框内容
        public string SearchContent
        {
            get { return _searchContext; }
            set { _searchContext = value; this.DoNotify(); }
        }

        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public MainViewModel(IDbContextFactory<AppDbContext> dbContextFactory, IServiceProvider services, MainView mainView)
        {
            UserModel = new UserModel
            {
                Account = GlobalValues.UserInfo.Account,
                UserName = GlobalValues.UserInfo.UserName,
                Gender = GlobalValues.UserInfo.Gender,
                Email = GlobalValues.UserInfo.Email,
                Avatar = GlobalValues.UserInfo.Avatar,
            };

            _dbContextFactory = dbContextFactory;

            // 导航栏切换事件绑定
            this.NavChangedCommand = new CommandBase
            {
                DoExecute = new Action<object>((o) => DoNavChanged(o)),
                DoCanExecute = new Func<object, bool>((o) => { return true; })
            };


            DoNavChanged("HomePageView");
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
            }
            catch (Exception ex)
            {
                // 记录日志
                MessageBox.Show($"导航失败: {ex.Message}");
            }
        }
    }
}
