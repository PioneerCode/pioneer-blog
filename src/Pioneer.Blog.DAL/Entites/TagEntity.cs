using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pioneer.Blog.DAL.Entites
{
    [Table("Tags")]
    public class TagEntity
    {
        [Key]
        public int TagId { get; set; }

        [MaxLength(100)]
        [StringLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        [StringLength(100)]
        public string Url { get; set; }

        public IList<PostTagEntity> Posts { get; set; }
    }
}
