using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
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

        public async void EnsureSeedData(UserManager<UserEntity> userMgr, RoleManager<IdentityRole> roleMgr)
        {
            // Create roles and role claims 
            var user = await userMgr.FindByIdAsync("dev@dev.com");

            // Add user to claim and role
            if (user != null) return;

            // Create roles and role claims 
            var adminRole = await roleMgr.FindByNameAsync("admin");
            if (adminRole == null)
            {
                adminRole = new IdentityRole("admin");
                adminRole.Claims.Add(new IdentityRoleClaim<string> { ClaimType = "isAdmin", ClaimValue = "true" });
                await roleMgr.CreateAsync(adminRole);
            }

            user = new UserEntity
            {
                UserName = "dev@dev.com"
            };

            var userResult = await userMgr.CreateAsync(user, "dev");
            var roleResult = await userMgr.AddToRoleAsync(user, "admin");
            var claimResult = await userMgr.AddClaimAsync(user, new Claim("superUser", "true"));

            if (!userResult.Succeeded  || !roleResult.Succeeded || !claimResult.Succeeded)
            {
                throw new InvalidOperationException("Failed to build user and roles");
            }
        }
    }
}
