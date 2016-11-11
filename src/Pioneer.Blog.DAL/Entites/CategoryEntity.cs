using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pioneer.Blog.DAL.Entites
{
    [Table("Categories")]
    public class CategoryEntity
    {
        [Key]
        public int CategoryId { get; set; }

        [MaxLength(100)]
        [StringLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        [StringLength(100)]
        public string Url { get; set; }

        public ICollection<PostEntity> Posts { get; set; }
    }
}
