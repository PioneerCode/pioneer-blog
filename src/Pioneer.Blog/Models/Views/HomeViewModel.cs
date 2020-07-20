using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

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
            Description = "Hi, my name is Chad Ramos. I am a Chicago-based software developer with a strong passion for .NET, C#, The Web, Open Source, Programming and more. Brought to you by Pioneer Code.";
            PopularPosts = new List<Post>();
            LatestPosts = new List<Post>();
        }
    }
}
