using BloomAdmin.Main.Common;
using BloomAdmin.Main.DataAccess;
using BloomAdmin.Main.Model;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace BloomAdmin.Main.ViewModel
{
    public class HomePageViewModel : NotifyBase, IDisposable
    {
        public ObservableCollection<CourseOverViewModel> CourseOverViewItems { get; } = new ObservableCollection<CourseOverViewModel>(); // 课程概览列表

        public CommandBase CourseCommand { get; set; } //课程刷新事件

        private int _platformCount;

        public int PlatformCount
        {
            get { return _platformCount; }
            set { _platformCount = value; this.DoNotify(); }
        }

        private int _instrumentValue;

        public int InstrumentValue
        {
            get { return _instrumentValue; }
            set { _instrumentValue = value; this.DoNotify(); }
        }


        List<Task> taskLsit;
        private bool isStart;
        Random rand = new Random();

        public HomePageViewModel()
        {
            // 登录事件绑定
            this.CourseCommand = new CommandBase
            {
                DoExecute = async obj => await DoRefreshCourseAsync(obj),
                DoCanExecute = new Func<object, bool>((obj) => { return true; })
            };


            InitCourseData();
            taskLsit = new List<Task>();
            isStart = true;

            // 避免 MetricCard 绑定 null，在后台任务首次写入前长时间无图
            DefaultData1 = RandomDatas();
            DefaultData2 = RandomDatas();
            DefaultData3 = RandomDatas();
            DefaultData4 = RandomDatas();
            MetricStart();
        }

        /// <summary>
        /// 更新刷新课程列表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private async Task DoRefreshCourseAsync(object obj)
        {
            CourseOverViewItems.Clear();

            using var db = new AppDbContext();
            var user = await db.Users.FirstOrDefaultAsync();
            if (user != null)
            {
                UpdateCourseData();
            }
        }

        #region 动态图表相关

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

        private void MetricStart()
        {
            var task1 = Task.Run(async () =>
            {
                while (isStart)
                {
                    DefaultData1 = RandomDatas();
                    await Task.Delay(10000);
                }
            });

            var task2 = Task.Run(async () =>
            {
                while (isStart)
                {
                    DefaultData2 = RandomDatas();
                    await Task.Delay(9000);
                }
            });

            var task3 = Task.Run(async () =>
            {
                while (isStart)
                {
                    DefaultData3 = RandomDatas();
                    await Task.Delay(8000);
                }
            });

            var task4 = Task.Run(async () =>
            {
                while (isStart)
                {
                    DefaultData4 = RandomDatas();
                    await Task.Delay(5000);
                }
            });

            var task5 = Task.Run(async () =>
            {
                while (isStart)
                {
                    int instrumentValue = rand.Next(0, 100);
                    InstrumentValue = instrumentValue;
                    await Task.Delay(1000);
                }
            });

            taskLsit.Add(task1);
            taskLsit.Add(task2);
            taskLsit.Add(task3);
            taskLsit.Add(task4);
            taskLsit.Add(task5);
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
        #endregion

        private void InitCourseData()
        {
            CourseOverViewItems.Add(new CourseOverViewModel()
            {
                CourseName = "C#/.NET架构师蜕变营",
                PieSeriesCollection = new SeriesCollection()
                {
                    new PieSeries{ Title = "云课堂", Values = new ChartValues<double>{ 245 } },
                    new PieSeries{ Title = "B站", Values = new ChartValues<double>{ 80 } },
                    new PieSeries{ Title = "知乎", Values = new ChartValues<double>{ 66 } },
                    new PieSeries{ Title = "抖音", Values = new ChartValues<double>{ 305 } },
                    new PieSeries{ Title = "博客", Values = new ChartValues<double>{ 26 } },
                },
                PlatformStats = new List<PlatformStat>
                {
                    new PlatformStat{ Name = "云课堂", Count = 245, Status = 1, Percent = "120%"},
                    new PlatformStat{ Name = "B站", Count = 80, Status = 1, Percent = "15%"},
                    new PlatformStat{ Name = "知乎", Count = 66, Status = 2, Percent = "32%"},
                    new PlatformStat{ Name = "抖音", Count = 305, Status = 1, Percent = "250%"},
                    new PlatformStat{ Name = "博客", Count = 26, Status = 2, Percent = "18%"},
                }
            });
            CourseOverViewItems.Add(new CourseOverViewModel()
            {
                CourseName = "WinForm实战训练营",
                PieSeriesCollection = new SeriesCollection()
                {
                    new PieSeries{ Title = "云课堂", Values = new ChartValues<double>{ 245 } },
                    new PieSeries{ Title = "B站", Values = new ChartValues<double>{ 80 } },
                    new PieSeries{ Title = "知乎", Values = new ChartValues<double>{ 66 } },
                    new PieSeries{ Title = "抖音", Values = new ChartValues<double>{ 305 } },
                    new PieSeries{ Title = "博客", Values = new ChartValues<double>{ 26 } },
                    new PieSeries{ Title = "小红书", Values = new ChartValues<double>{ 46 } },
                },
                PlatformStats = new List<PlatformStat>
                {
                    new PlatformStat{ Name = "云课堂", Count = 245, Status = 1, Percent = "120%"},
                    new PlatformStat{ Name = "小红书", Count = 46, Status = 1, Percent = "7%"},
                    new PlatformStat{ Name = "B站", Count = 80, Status = 1, Percent = "15%"},
                    new PlatformStat{ Name = "知乎", Count = 66, Status = 2, Percent = "32%"},
                    new PlatformStat{ Name = "抖音", Count = 305, Status = 1, Percent = "250%"},
                    new PlatformStat{ Name = "博客", Count = 26, Status = 2, Percent = "18%"},
                }
            });
            CourseOverViewItems.Add(new CourseOverViewModel()
            {
                CourseName = "WPF实战训练营",
            });

            PlatformCount = CourseOverViewItems.Where(x => x.PlatformStats != null).Select(x => x.PlatformStats.Count()).Max();
            Console.WriteLine(PlatformCount);
        }

        private void UpdateCourseData()
        {
            CourseOverViewItems.Add(new CourseOverViewModel()
            {
                CourseName = "课程1",
                PieSeriesCollection = new SeriesCollection()
                {
                    new PieSeries{ Title = "云课堂", Values = new ChartValues<double>{ 245 } },
                    new PieSeries{ Title = "B站", Values = new ChartValues<double>{ 80 } },
                    new PieSeries{ Title = "知乎", Values = new ChartValues<double>{ 66 } },
                },
                PlatformStats = new List<PlatformStat>
                {
                    new PlatformStat{ Name = "云课堂", Count = 245, Status = 1, Percent = "120%"},
                    new PlatformStat{ Name = "B站", Count = 80, Status = 1, Percent = "15%"},
                    new PlatformStat{ Name = "知乎", Count = 66, Status = 2, Percent = "32%"},
                }
            });
            CourseOverViewItems.Add(new CourseOverViewModel()
            {
                CourseName = "课程2",
                PieSeriesCollection = new SeriesCollection()
                {
                    new PieSeries{ Title = "云课堂", Values = new ChartValues<double>{ 245 } },
                    new PieSeries{ Title = "B站", Values = new ChartValues<double>{ 80 } },
                    new PieSeries{ Title = "知乎", Values = new ChartValues<double>{ 66 } },
                },
                PlatformStats = new List<PlatformStat>
                {
                    new PlatformStat{ Name = "云课堂", Count = 245, Status = 1, Percent = "120%"},
                    new PlatformStat{ Name = "小红书", Count = 46, Status = 1, Percent = "7%"},
                    new PlatformStat{ Name = "B站", Count = 80, Status = 1, Percent = "15%"},
                }
            });
            CourseOverViewItems.Add(new CourseOverViewModel()
            {
                CourseName = "课程3",
            });

            PlatformCount = CourseOverViewItems.Where(x => x.PlatformStats != null).Select(x => x.PlatformStats.Count()).Max();
            Console.WriteLine(PlatformCount);
        }

        void IDisposable.Dispose()
        {
            Task.WaitAll(taskLsit.ToArray());
            taskLsit.Clear();
        }
    }
}