using System.Collections.Generic;

namespace Pioneer.Blog.Model
{
    public class SearchResults
    {
        public SearchResults()
        {
            Posts = new List<Post>();
            TotalMatchingPosts = 0;
        }

        public List<Post> Posts { get; set; }
        public int TotalMatchingPosts { get; set; }
    }
}
