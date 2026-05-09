using System.Windows;
using System.Windows.Controls;

namespace BloomAdmin.Main.View.CustomControls
{
    /// <summary>
    /// CourseOverviewItemView.xaml 的交互逻辑
    /// </summary>
    public partial class CourseOverviewItemView : UserControl
    {
        public CourseOverviewItemView()
        {
            InitializeComponent();
        }

        // 名字
        public string ItemName
        {
            get => (string)GetValue(ItemNameProperty);
            set => SetValue(ItemNameProperty, value);
        }
        public static readonly DependencyProperty ItemNameProperty =
            DependencyProperty.Register("ItemName", typeof(string), typeof(CourseOverviewItemView), new PropertyMetadata(string.Empty));


        // 数量
        public int ItemCount
        {
            get => (int)GetValue(ItemCountProperty);
            set => SetValue(ItemCountProperty, value);
        }
        public static readonly DependencyProperty ItemCountProperty =
            DependencyProperty.Register("ItemCount", typeof(int), typeof(CourseOverviewItemView), new PropertyMetadata(default(int)));


        // 数量
        public int ItemStatus
        {
            get => (int)GetValue(ItemStatusProperty);
            set => SetValue(ItemStatusProperty, value);
        }
        public static readonly DependencyProperty ItemStatusProperty =
            DependencyProperty.Register("ItemStatus", typeof(int), typeof(CourseOverviewItemView), new PropertyMetadata(default(int)));

        // 百分比
        public string ItemPercent
        {
            get => (string)GetValue(ItemPercentProperty);
            set => SetValue(ItemPercentProperty, value);
        }
        public static readonly DependencyProperty ItemPercentProperty =
            DependencyProperty.Register("ItemPercent", typeof(string), typeof(CourseOverviewItemView), new PropertyMetadata("0%"));
    }
}
