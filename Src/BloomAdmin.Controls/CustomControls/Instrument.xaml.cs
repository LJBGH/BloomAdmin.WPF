using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace BloomAdmin.Controls
{
    /// <summary>
    /// Instrument.xaml 的交互逻辑
    /// </summary>
    public partial class Instrument : UserControl
    {

        // 依赖属性：Value，表示仪表盘的当前值
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(int),
                typeof(Instrument),
                new PropertyMetadata(default(int), new PropertyChangedCallback(OnValueChanged)));



        public int Minimm
        {
            get { return (int)GetValue(MinimmProperty); }
            set { SetValue(MinimmProperty, value); }
        }

        public static readonly DependencyProperty MinimmProperty =
            DependencyProperty.Register("Minimm", typeof(int),
                 typeof(Instrument),
                new PropertyMetadata(default(int), new PropertyChangedCallback(OnValueChanged)));


        public int Maximm
        {
            get { return (int)GetValue(MaximmProperty); }
            set { SetValue(MaximmProperty, value); }
        }
        public static readonly DependencyProperty MaximmProperty =
            DependencyProperty.Register("Maximm", typeof(int),
                 typeof(Instrument),
                 new PropertyMetadata(100, new PropertyChangedCallback(OnValueChanged)));


        public int Interval
        {
            get { return (int)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(int), typeof(Instrument),
                 new PropertyMetadata(10, new PropertyChangedCallback(OnValueChanged)));


        /// <summary>
        /// 背景颜色
        /// </summary>
        public Brush PlateBackground
        {
            get { return (Brush)GetValue(PlateBackgroundProperty); }
            set { SetValue(PlateBackgroundProperty, value); }
        }

        public static readonly DependencyProperty PlateBackgroundProperty =
            DependencyProperty.Register(nameof(PlateBackground), typeof(Brush), typeof(Instrument),
                new PropertyMetadata(Brushes.Orange));


        /// <summary>
        /// 指针颜色
        /// </summary>
        public Brush PointerBrush
        {
            get { return (Brush)GetValue(PointerBrushProperty); }
            set { SetValue(PointerBrushProperty, value); }
        }

        public static readonly DependencyProperty PointerBrushProperty =
            DependencyProperty.Register(nameof(PointerBrush), typeof(Brush), typeof(Instrument),
                 new PropertyMetadata(Brushes.Green));


        /// <summary>
        /// 刻度颜色
        /// </summary>
        public Brush ScaleBrush
        {
            get { return (Brush)GetValue(ScaleBrushProperty); }
            set { SetValue(ScaleBrushProperty, value); }
        }
        public static readonly DependencyProperty ScaleBrushProperty =
            DependencyProperty.Register(nameof(ScaleBrush), typeof(Brush), typeof(Instrument),
                new PropertyMetadata(Brushes.White));


        /// <summary>
        /// 圆心颜色
        /// </summary>
        public Brush DotBrush
        {
            get { return (Brush)GetValue(DotBrushProperty); }
            set { SetValue(DotBrushProperty, value); }
        }

        public static readonly DependencyProperty DotBrushProperty =
            DependencyProperty.Register(nameof(DotBrush), typeof(Brush), typeof(Instrument),
                    new PropertyMetadata(Brushes.White));




        /// <summary>
        /// 刷新表盘数据
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        public static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Instrument instrument)
            {
                instrument.Refresh();
            }
        }

        public Instrument()
        {
            InitializeComponent();
            this.SizeChanged += Instrument_SizeChanged;
        }

        /// <summary>
        /// 大小改变时，调整背景圆的大小，使其始终保持为正方形，并且适应控件的大小
        /// </summary>
        private void Instrument_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double minSize = Math.Min(this.RenderSize.Width, this.RenderSize.Height);
            this.backEllipse.Width = minSize;
            this.backEllipse.Height = minSize;
            // 首次 OnValueChanged 往往在椭圆宽高仍为 NaN 时触发，此处尺寸已就绪需重绘
            if (minSize > 0)
                Refresh();
        }

        private void Refresh()
        {
            double radius = this.backEllipse.Width / 2;
            if (double.IsNaN(radius) || radius <= 0)
                return;
            if (Maximm == Minimm)
                return;
            this.minCanvas.Children.Clear();

            double step = 270.0 / (Maximm - Minimm);
            //double scaleAreaCount = 10;

            for (int i = 0; i < Maximm - Minimm; i++)
            {
                Line lineScale = new Line();
                lineScale.X1 = radius - (radius - 15) * Math.Cos((i * step - 45) * Math.PI / 180);
                lineScale.Y1 = radius - (radius - 15) * Math.Sin((i * step - 45) * Math.PI / 180);
                lineScale.X2 = radius - (radius - 10) * Math.Cos((i * step - 45) * Math.PI / 180);
                lineScale.Y2 = radius - (radius - 10) * Math.Sin((i * step - 45) * Math.PI / 180);

                lineScale.Stroke = ScaleBrush;
                lineScale.StrokeThickness = 2;
                this.minCanvas.Children.Add(lineScale);
            }

            step = 270.0 / Interval;
            int scaleText = (int)Minimm;
            for (int i = 0; i <= Interval; i++)
            {
                Line lineScale = new Line();
                lineScale.X1 = radius - (radius - 20) * Math.Cos((i * step - 45) * Math.PI / 180);
                lineScale.Y1 = radius - (radius - 20) * Math.Sin((i * step - 45) * Math.PI / 180);
                lineScale.X2 = radius - (radius - 10) * Math.Cos((i * step - 45) * Math.PI / 180);
                lineScale.Y2 = radius - (radius - 10) * Math.Sin((i * step - 45) * Math.PI / 180);

                lineScale.Stroke = ScaleBrush;
                lineScale.StrokeThickness = 1;
                this.minCanvas.Children.Add(lineScale);

                TextBlock textScale = new TextBlock();
                textScale.FontSize = 14;
                textScale.Width = 30;
                textScale.TextAlignment = TextAlignment.Center;
                textScale.Foreground = ScaleBrush;

                textScale.Text = (scaleText + (Maximm - Minimm) / Interval * i).ToString();

                Canvas.SetLeft(textScale, radius - (radius - 36) * Math.Cos((i * step - 45) * Math.PI / 180) - 15);
                Canvas.SetTop(textScale, radius - (radius - 36) * Math.Sin((i * step - 45) * Math.PI / 180) - 7);
                this.minCanvas.Children.Add(textScale);
            }

            string sData = "M{0} {1} A{0} {0} 0 1 1 {1} {2}";
            sData = string.Format(sData, radius / 2, radius, radius * 1.5);
            var converter = TypeDescriptor.GetConverter(typeof(Geometry));
            this.circle.Data = (Geometry)converter.ConvertFrom(sData);


            step = 270.0 / (Maximm - Minimm);
            this.rtPointer.Angle = this.Value * step - 45;


            DoubleAnimation da = new DoubleAnimation((int)((this.Value - this.Minimm) * step) - 45,
                new Duration(TimeSpan.FromMilliseconds(200)));
            this.rtPointer.BeginAnimation(RotateTransform.AngleProperty, da);

            sData = "M{0} {1}, {1} {2}, {1} {3}";
            sData = string.Format(sData, radius * 0.3, radius, radius - 8, radius + 8);
            this.pointer.Data = (Geometry)converter.ConvertFrom(sData);
        }
    }
}
