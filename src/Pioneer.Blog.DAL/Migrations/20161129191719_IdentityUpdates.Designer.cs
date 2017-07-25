using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Pioneer.Blog.DAL;

namespace Pioneer.Blog.DAL.Migrations
{
    [DbContext(typeof(BlogContext))]
    [Migration("20161129191719_IdentityUpdates")]
    partial class IdentityUpdates
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Pioneer.Blog.DAL.Entites.ArticleEntity", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.HasKey("ArticleId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("Pioneer.Blog.DAL.Entites.CategoryEntity", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Url")
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Pioneer.Blog.DAL.Entites.ContactEntity", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 254);

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("ContactId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Pioneer.Blog.DAL.Entites.ExcerptEntity", b =>
                {
                    b.Property<int>("ExcerptId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .HasAnnotation("MaxLength", 500);

                    b.HasKey("ExcerptId");

                    b.ToTable("Excerpts");
                });

            modelBuilder.Entity("Pioneer.Blog.DAL.Entites.PostEntity", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ArticleId");

                    b.Property<int?>("CategoryId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 300);

                    b.Property<int?>("ExcerptId");

                    b.Property<string>("IconImage")
                        .HasAnnotation("MaxLength", 250);

                    b.Property<string>("Image")
                        .HasAnnotation("MaxLength", 250);

                    b.Property<string>("Link")
                        .HasAnnotation("MaxLength", 220);

                    b.Property<string>("Meta")
                        .HasAnnotation("MaxLength", 250);

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<DateTime?>("PostedOn");

                    b.Property<bool>("Published");

                    b.Property<string>("SmallImage")
                        .HasAnnotation("MaxLength", 250);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 250);

                    b.Property<string>("Url")
                        .HasAnnotation("MaxLength", 220);

                    b.HasKey("PostId");

                    b.HasIndex("ArticleId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ExcerptId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Pioneer.Blog.DAL.Entites.PostTagEntity", b =>
                {
                    b.Property<int>("PostTagId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PostId");

                    b.Property<int>("TagId");

                    b.HasKey("PostTagId");

                    b.HasIndex("PostId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTags");
                });

            modelBuilder.Entity("Pioneer.Blog.DAL.Entites.TagEntity", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Url")
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Pioneer.Blog.DAL.Entites.UserEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Pioneer.Blog.DAL.Entites.UserEntity")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Pioneer.Blog.DAL.Entites.UserEntity")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Pioneer.Blog.DAL.Entites.UserEntity")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Pioneer.Blog.DAL.Entites.PostEntity", b =>
                {
                    b.HasOne("Pioneer.Blog.DAL.Entites.ArticleEntity", "Article")
                        .WithMany()
                        .HasForeignKey("ArticleId");

                    b.HasOne("Pioneer.Blog.DAL.Entites.CategoryEntity", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Pioneer.Blog.DAL.Entites.ExcerptEntity", "Excerpt")
                        .WithMany()
                        .HasForeignKey("ExcerptId");
                });

            modelBuilder.Entity("Pioneer.Blog.DAL.Entites.PostTagEntity", b =>
                {
                    b.HasOne("Pioneer.Blog.DAL.Entites.PostEntity", "Post")
                        .WithMany("PostTags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Pioneer.Blog.DAL.Entites.TagEntity", "Tag")
                        .WithMany("Posts")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
