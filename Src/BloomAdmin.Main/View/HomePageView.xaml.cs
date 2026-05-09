using BloomAdmin.Main.ViewModel;
using LiveCharts;
using System.Windows.Controls;

namespace BloomAdmin.Main.View
{
    /// <summary>
    /// HomePageView.xaml 的交互逻辑
    /// </summary>
    public partial class HomePageView : UserControl, IDisposable
    {

        List<Task> taskLsit;
        private bool isStart;
        Random rand = new Random();
        HomePageViewModel homePageViewModel;
        public HomePageView()
        {
            InitializeComponent();
            homePageViewModel = new HomePageViewModel();
            DataContext = homePageViewModel;
            taskLsit = new List<Task>();
            isStart = true;
            MetricStart();
        }

        private void MetricStart()
        {
            var task1 = Task.Run(async () =>
            {
                while (isStart)
                {
                    homePageViewModel.DefaultData1 = RandomDatas();
                    await Task.Delay(10000);
                }
            });

            var task2 = Task.Run(async () =>
            {
                while (isStart)
                {
                    homePageViewModel.DefaultData2 = RandomDatas();
                    await Task.Delay(9000);
                }
            });

            var task3 = Task.Run(async () =>
            {
                while (isStart)
                {
                    homePageViewModel.DefaultData3 = RandomDatas();
                    await Task.Delay(8000);
                }
            });

            var task4 = Task.Run(async () =>
            {
                while (isStart)
                {
                    homePageViewModel.DefaultData4 = RandomDatas();
                    await Task.Delay(5000);
                }
            });

            taskLsit.Add(task1);
            taskLsit.Add(task2);
            taskLsit.Add(task3);
            taskLsit.Add(task4);
        }

        void IDisposable.Dispose()
        {
            Task.WaitAll(taskLsit.ToArray());
            taskLsit.Clear();
        }

        private ChartValues<double> RandomDatas()
        {
            ChartValues<double> doubles = new ChartValues<double>();
            for (int i = 0; i < 5; i++)
            {
                int randomInt = rand.Next(1, 101);
                doubles.Add(randomInt);
            }

            return doubles;
        }
    }
}
