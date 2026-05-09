using LiveCharts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BloomAdmin.Main.View.CustomControls
{
    /// <summary>
    /// MetricCard.xaml 的交互逻辑
    /// </summary>
    public partial class MetricCardView : UserControl
    {
        public MetricCardView()
        {
            InitializeComponent();
        }


        // 主颜色
        public Brush StrokeColor
        {
            get => (Brush)GetValue(StrokeColorProperty);
            set => SetValue(StrokeColorProperty, value);
        }
        public static readonly DependencyProperty StrokeColorProperty =
            DependencyProperty.Register("StrokeColor", typeof(Brush), typeof(MetricCardView), new PropertyMetadata(Brushes.Green));


        // 填充颜色
        public Brush FillColor
        {
            get => (Brush)GetValue(FillColorProperty);
            set => SetValue(FillColorProperty, value);
        }
        public static readonly DependencyProperty FillColorProperty =
            DependencyProperty.Register("FillColor", typeof(Brush), typeof(MetricCardView), new PropertyMetadata(Brushes.Gray));


        // 上方字体图标
        public string TopIcon
        {
            get => (string)GetValue(TopIconProperty);
            set => SetValue(TopIconProperty, value);
        }

        public static readonly DependencyProperty TopIconProperty =
                    DependencyProperty.Register("TopIcon", typeof(string), typeof(MetricCardView), new PropertyMetadata("\uE662"));


        // 上方字体内容
        public string TopTextContent
        {
            get => (string)GetValue(TopTextContentProperty);
            set => SetValue(TopTextContentProperty, value);
        }
        public static readonly DependencyProperty TopTextContentProperty =
            DependencyProperty.Register("TopTextContent", typeof(string), typeof(MetricCardView), new PropertyMetadata(string.Empty));


        // 底部字体图标
        public string ButtomIcon
        {
            get => (string)GetValue(ButtomIconProperty);
            set => SetValue(ButtomIconProperty, value);
        }

        public static readonly DependencyProperty ButtomIconProperty =
                    DependencyProperty.Register("ButtomIcon", typeof(string), typeof(MetricCardView), new PropertyMetadata("\uE662"));

        // 底部字体内容
        public string ButtomTextContent1
        {
            get => (string)GetValue(ButtomTextContentProperty1);
            set => SetValue(ButtomTextContentProperty1, value);
        }
        public static readonly DependencyProperty ButtomTextContentProperty1 =
            DependencyProperty.Register("ButtomTextContent1", typeof(string), typeof(MetricCardView), new PropertyMetadata(string.Empty));

        // 底部字体内容2
        public string ButtomTextContent2
        {
            get => (string)GetValue(ButtomTextContentProperty2);
            set => SetValue(ButtomTextContentProperty2, value);
        }
        public static readonly DependencyProperty ButtomTextContentProperty2 =
            DependencyProperty.Register("ButtomTextContent2", typeof(string), typeof(MetricCardView), new PropertyMetadata(string.Empty));




        public bool XShowLabels
        {
            get => (bool)GetValue(XShowLabelsProperty);
            set => SetValue(XShowLabelsProperty, value);
        }

        public static readonly DependencyProperty XShowLabelsProperty =
            DependencyProperty.Register("XShowLabels", typeof(bool), typeof(MetricCardView), new PropertyMetadata(false));

        public bool YShowLabels
        {
            get => (bool)GetValue(YShowLabelsProperty);
            set => SetValue(YShowLabelsProperty, value);
        }

        public static readonly DependencyProperty YShowLabelsProperty =
            DependencyProperty.Register("YShowLabels", typeof(bool), typeof(MetricCardView), new PropertyMetadata(false));


        // 值绑定
        public ChartValues<double> ChartValues
        {
            get => (ChartValues<double>)GetValue(ChartValuesProperty);
            set => SetValue(ChartValuesProperty, value);
        }

        public static readonly DependencyProperty ChartValuesProperty =
            DependencyProperty.Register(
                "ChartValues",
                typeof(ChartValues<double>),
                typeof(MetricCardView),
                new PropertyMetadata(new ChartValues<double> { 8, 93, 60, 5, 6, 10 }));
    }
}
