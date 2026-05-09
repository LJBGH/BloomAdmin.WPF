using BloomAdmin.Main.ViewModel;
using System.Windows.Controls;

namespace BloomAdmin.Main.View
{
    /// <summary>
    /// HomePageView.xaml 的交互逻辑
    /// </summary>
    public partial class HomePageView : UserControl
    {
        public HomePageView()
        {
            InitializeComponent();
            DataContext = new HomePageViewModel();
        }
    }
}
