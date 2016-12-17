using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer.Blog.Model.Views
{
    public class ArticleViewModel
    {
        public string ArticleSection { get; set; }
        public string ArticlePublishedTime { get; set; }
        public string ArticleModifiedTime { get; set; }
        public string Image { get; set; }
    }
}
