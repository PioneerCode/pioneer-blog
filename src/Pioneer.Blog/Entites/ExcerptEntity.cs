using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pioneer.Blog.Entites
{
    [Table("Excerpts")]
    public class ExcerptEntity
    {
        [Key]
        public int ExcerptId { get; set; }

        [Display(Name = "Excerpt")]
        [StringLength(500)]
        public string Content { get; set; }
    }
}
