using System;
using System.Collections.Generic;

namespace Pioneer.Blog.Model
{
    /// <summary>
    /// Identifies position in previous, current and 
    /// next collection when getting a single post with meta
    /// </summary>
    public enum PreviousCurrentNextPosition
    {
        Previous = 0,
        Current,
        Next
    }

    public class Post
    {
        public Post()
        {
            Tags = new List<Tag>();
            Category = new Category();
            Article = new Article();
            Excerpt = new Excerpt();
        }

        public int PostId { get; set; }
        public string Title { get; set; }
        public string Meta { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string SmallImage { get; set; }
        public string IconImage { get; set; }
        public string Url { get; set; }
        public string Link { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public Article Article { get; set; }
        public Category Category { get; set; }
        public Excerpt Excerpt { get; set; }
    }
}
