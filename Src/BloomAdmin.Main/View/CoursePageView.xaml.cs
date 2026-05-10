using BloomAdmin.Main.ViewModel;
using System.Windows.Controls;

namespace BloomAdmin.Main.View
{
    /// <summary>
    /// CoursePageView.xaml 的交互逻辑
    /// </summary>
    public partial class CoursePageView : UserControl
    {
        public CoursePageView()
        {
            InitializeComponent();
            DataContext = new CoursePageViewModel();
        }
    }
}
