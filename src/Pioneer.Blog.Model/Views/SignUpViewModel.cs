using System.ComponentModel.DataAnnotations;

namespace Pioneer.Blog.Model.Views
{
    public class SignUpViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [StringLength(254)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
