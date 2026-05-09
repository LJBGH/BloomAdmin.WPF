using Lazy.Captcha.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using BloomAdmin.Main.Common;
using BloomAdmin.Main.DataAccess;
using BloomAdmin.Main.Expansion;
using BloomAdmin.Main.Model;
using BloomAdmin.Main.View;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
namespace BloomAdmin.Main.ViewModel
{
    public class LoginViewModel : NotifyBase
    {
        public LoginModel LoginModel { get; set; }
        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase LoginCommand { get; set; }
        public CommandBase CaptchaCommand { get; set; }


        private readonly IDbContextFactory<AppDbContext> _dbContext;
        private readonly IServiceProvider _services;
        private readonly Window _loginView;
        private readonly ICaptcha _captcha;

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                DoNotify();
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                DoNotify();
                DoNotify(nameof(ErrorMessageColor)); // 计算属性依赖 ErrorMessage，须单独通知 Foreground 绑定
            }
        }

        /// <summary>须用 WPF 的 Media.Brush；System.Drawing 的 Brush 不能用于 Foreground 绑定。</summary>
        public Brush ErrorMessageColor => string.IsNullOrEmpty(ErrorMessage)
            ? Brushes.Transparent
            : (ErrorMessage.Contains("成功") ? Brushes.Green : Brushes.Red);

        public LoginViewModel(IDbContextFactory<AppDbContext> dbContext, IServiceProvider services, Window loginWindow,
            ICaptcha captcha)
        {
            this._dbContext = dbContext;
            _services = services;
            _loginView = loginWindow;
            _captcha = captcha;
            this.LoginModel = new LoginModel();
            ErrorMessage = "";

            // 关闭事件绑定
            this.CloseWindowCommand = new CommandBase
            {
                DoExecute = new Action<object>((o) =>
                {
                    if (o is Window window)
                    {
                        window.Close();
                    }
                }),
                DoCanExecute = new Func<object, bool>((o) => { return true; })
            };

            // 登录事件绑定
            this.LoginCommand = new CommandBase
            {
                DoExecute = async obj => await DoLoginAsync(obj),
                DoCanExecute = new Func<object, bool>((obj) => { return true; })
            };

            this.CaptchaCommand = new CommandBase
            {
                DoExecute = obj => DoCaptcha(obj),
                DoCanExecute = new Func<object, bool>((obj) => { return true; })
            };
        }

        /// <summary>
        /// 刷新验证码
        /// </summary>
        /// <param name="obj"></param>
        private void DoCaptcha(object obj)
        {
            var captchaData = _captcha.Generate(new Guid().ToString());
            Console.WriteLine($"验证码：data:image/png;base64,{captchaData.Base64}");
            this.LoginModel.CaptchaImage = MemeryToBitmapImage(captchaData.Bytes);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        private async Task DoLoginAsync(object obj)
        {
            ErrorMessage = string.Empty;
            IsLoading = true;

            if (LoginModel.Account.IsNull() || LoginModel.Password.IsNull())
            {
                IsLoading = false;
                ErrorMessage = "请输入用户名和密码！";
                return;
            }

            if (LoginModel.CaptchaCode.IsNull())
            {
                IsLoading = false;
                ErrorMessage = "请输入验证码！";
                return;
            }

            // 每次登录新建 DbContext；在后台线程执行查询，避免长时间占住 UI 消息循环
            using var db = _dbContext.CreateDbContext();
            var user = await db.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Account == LoginModel.Account);

            if (user == null)
            {
                IsLoading = false;
                ErrorMessage = "用户不存在！";
                return;
            }

            var pwd = SecurityHelper.MD5Encrypt(LoginModel.Password);
            if (user.Password != pwd)
            {
                IsLoading = false;
                ErrorMessage = "用户密码错误！";
                return;
            }

            GlobalValues.UserInfo = user;
            await Task.Delay(1000); // 模拟登录后续耗时；在 UI 线程 await，不阻塞渲染与动画

            IsLoading = false;
            var mainView = _services.GetRequiredService<MainView>();
            mainView.Show();
            if (Application.Current != null)
                Application.Current.MainWindow = mainView;
            _loginView.Close();
        }

        #region 帮助方法
        private BitmapImage MemeryToBitmapImage(byte[] buffer)
        {
            using (var stream = new MemoryStream(buffer))
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad; // 确保流可以关闭
                bitmap.StreamSource = stream;
                bitmap.EndInit();
                bitmap.Freeze(); // 可选：冻结以提高性能并允许跨线程访问
                return bitmap;
            }
        }
        #endregion
    }
}
