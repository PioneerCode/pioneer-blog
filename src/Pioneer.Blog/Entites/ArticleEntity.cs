﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pioneer.Blog.Entites
{
    [Table("Articles")]
    public class ArticleEntity
    {
        [Key]
        public int ArticleId { get; set; }

        [Display(Name = "Article")]
        public string Content { get; set; }
    }
}
