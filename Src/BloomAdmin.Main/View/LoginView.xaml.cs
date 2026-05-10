using Lazy.Captcha.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;
using BloomAdmin.Main.DataAccess;
using BloomAdmin.Main.ViewModel;

namespace BloomAdmin.Main.View
{
    public partial class LoginView : Window
    {
        public LoginView(IDbContextFactory<AppDbContext> dbContextFactory, IServiceProvider services)
        {
            InitializeComponent();
            var captcha = services.GetRequiredService<ICaptcha>();
            DataContext = new LoginViewModel(dbContextFactory, services, this, captcha);
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                // 设置焦点到用账号输入框
                AccountTextBox.Focus();
                Keyboard.Focus(AccountTextBox);
            }), System.Windows.Threading.DispatcherPriority.Input);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            this.PasswordBox.Visibility = Visibility.Hidden;
            this.PasswordTextBox.Visibility = Visibility.Visible;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            this.PasswordBox?.Visibility = Visibility.Visible;
            this.PasswordTextBox?.Visibility = Visibility.Hidden;
        }
    }
}
