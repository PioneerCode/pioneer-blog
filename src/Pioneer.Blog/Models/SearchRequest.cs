using System.ComponentModel.DataAnnotations;

namespace Pioneer.Blog.Models
{
    public class SearchRequest
    {
        public SearchRequest()
        {
            Page = 1;
        }

        [Required]
        public string Query { get; set; }

        public int Page { get; set; }
    }
}
