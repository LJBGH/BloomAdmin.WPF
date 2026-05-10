using LiveCharts;

namespace BloomAdmin.Main.Model
{
    /// <summary>
    /// 课程概览
    /// </summary>
    public class CourseOverViewModel
    {
        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 饼图数据，LiveCharts只能绑定SeriesCollection
        /// </summary>
        public SeriesCollection PieSeriesCollection { get; set; }
        
        /// <summary>
        /// 平台增长状态
        /// </summary>
        public List<PlatformStat> PlatformStats { get; set; }
    }

    /// <summary>
    /// 平台数据
    /// </summary>
    public class PlatformStat
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public int Status { get; set; }
        public string Percent { get; set; }
    }
}
