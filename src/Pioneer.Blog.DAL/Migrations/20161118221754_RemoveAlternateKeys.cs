using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pioneer.Blog.DAL.Migrations
{
    public partial class RemoveAlternateKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AlternateKey_TagUrl",
                table: "Tags");

            migrationBuilder.DropUniqueConstraint(
                name: "AlternateKey_PostUrl",
                table: "Posts");

            migrationBuilder.DropUniqueConstraint(
                name: "AlternateKey_CategoryUrl",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Tags",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Posts",
                maxLength: 220,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Categories",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Tags",
                maxLength: 100,
                nullable: false);

            migrationBuilder.AddUniqueConstraint(
                name: "AlternateKey_TagUrl",
                table: "Tags",
                column: "Url");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Posts",
                maxLength: 220,
                nullable: false);

            migrationBuilder.AddUniqueConstraint(
                name: "AlternateKey_PostUrl",
                table: "Posts",
                column: "Url");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Categories",
                maxLength: 100,
                nullable: false);

            migrationBuilder.AddUniqueConstraint(
                name: "AlternateKey_CategoryUrl",
                table: "Categories",
                column: "Url");
        }
    }
}
