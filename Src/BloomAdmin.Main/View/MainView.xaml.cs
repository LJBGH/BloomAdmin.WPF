using Microsoft.EntityFrameworkCore;
using System.Windows;
using BloomAdmin.Main.DataAccess;
using BloomAdmin.Main.ViewModel;

namespace BloomAdmin.Main.View
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(IDbContextFactory<AppDbContext> dbContextFactory, IServiceProvider services)
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.PrimaryScreenHeight;
            DataContext = new MainViewModel(dbContextFactory, services, this); ;
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            if (this.WindowState == WindowState.Maximized)
            {
                this.Main_Header.CornerRadius = new CornerRadius(0);
            }
            else
            {
                this.Main_Header.CornerRadius = new CornerRadius(20, 20, 0, 0);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
