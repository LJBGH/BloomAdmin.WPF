using BloomAdmin.Main.Common;
using LiveCharts;

namespace BloomAdmin.Main.ViewModel
{
    public class HomePageViewModel : NotifyBase
    {


        private ChartValues<double> _defaultData1;

        public ChartValues<double> DefaultData1
        {
            get { return _defaultData1; }
            set { _defaultData1 = value; this.DoNotify(); }
        }

        private ChartValues<double> _defaultData2;

        public ChartValues<double> DefaultData2
        {
            get { return _defaultData2; }
            set { _defaultData2 = value; this.DoNotify(); }
        }

        private ChartValues<double> _defaultData3;

        public ChartValues<double> DefaultData3
        {
            get { return _defaultData3; }
            set { _defaultData3 = value; this.DoNotify(); }
        }

        private ChartValues<double> _defaultData4;
        public ChartValues<double> DefaultData4
        {
            get { return _defaultData4; }
            set { _defaultData4 = value; this.DoNotify(); }
        }


        public HomePageViewModel()
        {
            DefaultData1 = new ChartValues<double> { 56, 20, 30, 24, 51 };
            DefaultData2 = new ChartValues<double> { 25, 15, 36, 12, 31 };
            DefaultData3 = new ChartValues<double> { 57, 16, 30, 65, 52 };
            DefaultData4 = new ChartValues<double> { 22, 25, 8, 32, 41 };
        }
    }
}