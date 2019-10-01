using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pioneer.Blog.Models.Views
{
    public class HomeViewModel : BaseViewModel
    {
        public List<Post> PopularPosts { get; set; }

        public List<Post> LatestPosts { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(254)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public HomeViewModel()
        {
            PopularPosts = new List<Post>();
            LatestPosts = new List<Post>();
        }
    }
}
