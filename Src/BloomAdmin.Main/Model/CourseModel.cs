namespace BloomAdmin.Main.Model
{
    public class CourseModel
    {
        public string CourseName { get; set; }
        public string CourseUrl { get; set; }
        public string CourseDescription { get; set; }
        public List<string> Teachers { get; set; }
        public string CoverUrl { get; set; }
        public bool IsShowSkeleton { get; set; }
    }
}
