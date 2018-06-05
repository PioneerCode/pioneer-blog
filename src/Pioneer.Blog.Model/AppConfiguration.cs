using System.Collections.Generic;

namespace Pioneer.Blog.Model
{
    public class AppConfiguration
    {
        public AppConfiguration()
        {
            PopularPosts = new List<int>();
        }

        public string EmailUsername { get; set; }
        public string SendEmailTo { get; set; }
        public string EmailHost { get; set; }
        public string EmailPassword { get; set; }
        public string EmailPort { get; set; }
        public string SiteTitle { get; set; }
        public string DisqusShortname { get; set; }
        public string SiteUrl { get; set; }
        public string GoogleAnalyticsId { get; set; }
        public List<int> PopularPosts { get; set; }
        public string Key { get; set; }
    }
}
