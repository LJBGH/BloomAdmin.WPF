namespace BloomAdmin.Main.Model
{
    public class CategoryItemModel
    {
        public bool IsChecked { get; set; }
        public string CategoryName { get; set; }

        public CategoryItemModel(bool isChecked, string categoryName)
        {
            IsChecked = isChecked;
            CategoryName = categoryName;
        }
    }
}
