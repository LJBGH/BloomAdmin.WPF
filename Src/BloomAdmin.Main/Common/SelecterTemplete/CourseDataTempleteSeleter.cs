using BloomAdmin.Main.Model;
using System.Windows;
using System.Windows.Controls;

namespace BloomAdmin.Main.Common.SelecterTemplete
{
    /// <summary>
    /// 课程列表模版选择器
    /// </summary>
    public class CourseDataTempleteSeleter : DataTemplateSelector
    {
        public DataTemplate DefaultDataTemplete { get; set; }
        public DataTemplate SkeletonDataTemplete { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            // 如果显示骨架屏为True
            if ((item as CourseModel).IsShowSkeleton)
            {
                return SkeletonDataTemplete;
            }
            return DefaultDataTemplete;
        }
    }
}
