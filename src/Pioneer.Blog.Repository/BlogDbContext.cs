using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pioneer.Blog.Entity;

namespace Pioneer.Blog.Repository
{
    public class BlogDbContext : IdentityDbContext<ApplicationUser>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        public DbSet<PostEntity> Posts { get; set; }

        public DbSet<PostTagEntity> PostTags { get; set; }

        public DbSet<TagEntity> Tags { get; set; }

        public DbSet<ArticleEntity> Articles { get; set; }

        public DbSet<CategoryEntity> Categories { get; set; }

        public DbSet<ExcerptEntity> Excerpts { get; set; }

        public DbSet<ContactEntity> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        //public async void EnsureSeedData(UserManager<UserEntity> userMgr, RoleManager<IdentityRole> roleMgr)
        //{
        //    // Create roles and role claims 
        //    var user = await userMgr.FindByIdAsync("dev@dev.com");

        //    // Add user to claim and role
        //    if (user != null) return;

        //    // Create roles and role claims 
        //    var adminRole = await roleMgr.FindByNameAsync("admin");
        //    if (adminRole == null)
        //    {
        //        adminRole = new IdentityRole("admin");
        //        adminRole.Claims.Add(new IdentityRoleClaim<string> { ClaimType = "isAdmin", ClaimValue = "true" });
        //        await roleMgr.CreateAsync(adminRole);
        //    }

        //    user = new UserEntity
        //    {
        //        UserName = "dev@dev.com"
        //    };

        //    var userResult = await userMgr.CreateAsync(user, "dev");
        //    var roleResult = await userMgr.AddToRoleAsync(user, "admin");
        //    var claimResult = await userMgr.AddClaimAsync(user, new Claim("superUser", "true"));

        //    if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
        //    {
        //        throw new InvalidOperationException("Failed to build user and roles");
        //    }
        //}
    }
}
