namespace Pioneer.Blog.Model.Views
{
    public class ArticleViewModel
    {
        public ArticleViewModel()
        {
            ArticleSection = "";
            ArticlePublishedTime = "";
            ArticleModifiedTime = "";
            Image = "";
        }
        public string ArticleSection { get; set; }
        public string ArticlePublishedTime { get; set; }
        public string ArticleModifiedTime { get; set; }
        public string Image { get; set; }
    }
}
