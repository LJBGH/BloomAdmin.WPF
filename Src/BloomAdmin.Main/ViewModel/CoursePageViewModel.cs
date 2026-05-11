using BloomAdmin.Main.Common;
using BloomAdmin.Main.Expansion;
using BloomAdmin.Main.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace BloomAdmin.Main.ViewModel
{
    public class CoursePageViewModel
    {
        public ObservableCollection<CategoryItemModel> CourseCategory { get; set; }
        public ObservableCollection<CategoryItemModel> TechnologyCategory { get; set; }
        public ObservableCollection<CategoryItemModel> TeacherCategory { get; set; }


        // 课程列表
        public ObservableCollection<CourseModel> CourseItems { get; set; }

        private List<CourseModel> AllCourseList { get; set; } = new List<CourseModel>();

        // 分类切换事件
        public CommandBase CategorySwitchCommand { get; set; }

        // 课程跳转事件
        public CommandBase OpenCourseLinkCommand { get; set; }

        public CoursePageViewModel()
        {
            CategorySwitchCommand = new CommandBase
            {
                DoExecute = obj => DoCategorySwitch(obj),
                DoCanExecute = new Func<object, bool>((obj) => { return true; })
            };

            OpenCourseLinkCommand = new CommandBase
            {
                DoExecute = obj => DoOpenCourseUrl(obj),
                DoCanExecute = new Func<object, bool>((obj) => { return true; })
            };

            InitCategory();
            InitCourseData();

            DoCategorySwitch("全部");
        }

        // 打开链接
        private void DoOpenCourseUrl(object obj)
        {
            Process.Start(new ProcessStartInfo(obj.ToString()) { UseShellExecute = true });
        }

        /// <summary>
        /// 切换类目
        /// </summary>
        /// <param name="obj"></param>
        private void DoCategorySwitch(object obj)
        {
            CourseItems.Clear();
            for (int i = 0; i < 10; i++)
            {
                CourseItems.Add(new CourseModel { IsShowSkeleton = true });
            }

            try
            {
                Task.Run(async () =>
                {
                    var category = obj.ToString();
                    if (category.IsNotNull())
                    {

                        List<CourseModel> courses = new List<CourseModel>();
                        if (category == "全部")
                        {
                            courses = AllCourseList.ToList();
                        }
                        else
                        {
                            courses = AllCourseList.Where(x => x.CourseName.Contains(category) || x.Teachers.Contains(category)).ToList();
                        }

                        await Task.Delay(3000);
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            CourseItems.Clear();
                            foreach (var course in courses)
                            {
                                CourseItems.Add(course);
                            }
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 初始化课程列表
        /// </summary>
        private void InitCourseData()
        {
            CourseItems = new ObservableCollection<CourseModel>();
            CourseItems.Add(new CourseModel
            {
                CourseName = "C#入门课程",
                CourseUrl = "www.baidu.com",
                CourseDescription = "朝夕教育，专注于IT类在线教育，注重服务与口碑，解决升职与加薪难题。课程体系覆盖C#/.NET零基础就业、进阶高级开发、蜕变架构师全周期，以及上位机、AutoCAD、Java进阶等，致力于打造高品质在线教育，赋能IT人。升职加薪，只争朝夕，以梦为马，不负韶华！",
                Teachers = new List<string> { "老师1", "老师2" },
                CoverUrl = "/Assets/Images/Img3.jpg"
            });
            CourseItems.Add(new CourseModel
            {
                CourseName = "VUE实战课程",
                CourseUrl = "www.baidu.com",
                CourseDescription = "深入讲解VUE框架的实战课程，适合有一定前端基础的开发者。",
                Teachers = new List<string> { "老师1" },
                CoverUrl = "/Assets/Images/Img2.jpg"
            });
            CourseItems.Add(new CourseModel
            {
                CourseName = "ASP.NET核心课程",
                CourseUrl = "www.baidu.com",
                CourseDescription = "全面介绍ASP.NET核心框架的课程，适合有一定C#基础的开发者。",
                Teachers = new List<string> { "老师2", "老师3" },
                CoverUrl = "/Assets/Images/Img1.jpg"
            });
            CourseItems.Add(new CourseModel
            {
                CourseName = "公开课：软件开发趋势",
                CourseUrl = "www.baidu.com",
                CourseDescription = "朝夕教育，专注于IT类在线教育，注重服务与口碑，解决升职与加薪难题。课程体系覆盖C#/.NET零基础就业、进阶高级开发、蜕变架构师全周期，以及上位机、AutoCAD、Java进阶等，致力于打造高品质在线教育，赋能IT人。升职加薪，只争朝夕，以梦为马，不负韶华！",
                Teachers = new List<string> { "老师1", "老师2" },
                CoverUrl = "/Assets/Images/Img2.jpg"
            });
            CourseItems.Add(new CourseModel
            {
                CourseName = "VIP课程：高级编程技巧",
                CourseUrl = "www.baidu.com",
                CourseDescription = "VIP课程，分享高级编程技巧和最佳实践，适合有丰富开发经验的开发者。",
                Teachers = new List<string> { "老师1" },
                CoverUrl = "/Assets/Images/Img3.jpg"
            });
            CourseItems.Add(new CourseModel
            {
                CourseName = "公开课：云计算基础",
                CourseUrl = "www.baidu.com",
                CourseDescription = "公开课，介绍云计算的基本概念和常用服务。",
                Teachers = new List<string> { "老师3" },
                CoverUrl = "/Assets/Images/Img1.jpg"
            });


            AllCourseList = CourseItems.ToList();
        }

        /// <summary>
        /// 初始化课程分类
        /// </summary>
        private void InitCategory()
        {
            CourseCategory = new ObservableCollection<CategoryItemModel>();
            CourseCategory.Add(new CategoryItemModel(true, "全部"));
            CourseCategory.Add(new CategoryItemModel(false, "公开课"));
            CourseCategory.Add(new CategoryItemModel(false, "VIP课程"));

            TechnologyCategory = new ObservableCollection<CategoryItemModel>();
            TechnologyCategory.Add(new CategoryItemModel(true, "全部"));
            TechnologyCategory.Add(new CategoryItemModel(false, "C#"));
            TechnologyCategory.Add(new CategoryItemModel(false, ".NET"));
            TechnologyCategory.Add(new CategoryItemModel(false, "VUE"));
            TechnologyCategory.Add(new CategoryItemModel(false, "微服务"));
            TechnologyCategory.Add(new CategoryItemModel(false, "上位机"));

            TeacherCategory = new ObservableCollection<CategoryItemModel>();
            TeacherCategory.Add(new CategoryItemModel(false, "全部"));
            TeacherCategory.Add(new CategoryItemModel(true, "老师1"));
            TeacherCategory.Add(new CategoryItemModel(false, "老师2"));
            TeacherCategory.Add(new CategoryItemModel(false, "老师3"));
        }
    }
}
