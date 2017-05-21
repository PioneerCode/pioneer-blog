using System.Collections.Generic;

namespace Pioneer.Blog.Model
{
    public class SearchResults
    {
        public SearchResults()
        {
            Posts = new List<Post>();
            // TODO: This should be set to 0 by default.  There currently is a bug in the Pioneer.Pagination service. 
            TotalMatchingPosts = 1;
        }

        public List<Post> Posts { get; set; }
        public int TotalMatchingPosts { get; set; }
    }
}
