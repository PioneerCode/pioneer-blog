using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pioneer.Blog.DAL.Entites;

namespace Pioneer.Blog.DAL
{
    public class BlogContext : IdentityDbContext<UserEntity>
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        { }

        public DbSet<PostEntity> Posts { get; set; }

        public DbSet<PostTagEntity> PostTags { get; set; }

        public DbSet<TagEntity> Tags { get; set; }

        public DbSet<ArticleEntity> Articles { get; set; }

        public DbSet<CategoryEntity> Categories { get; set; }

        public DbSet<ExcerptEntity> Excerpts { get; set; }

        public DbSet<ContactEntity> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Insure Identity Entities are accounted for.
            base.OnModelCreating(modelBuilder);
        }
    }
}
