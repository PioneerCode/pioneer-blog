﻿using Microsoft.AspNetCore.Builder;

namespace Pioneer.Blog.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureCors(this IApplicationBuilder app)
        {
            app.UseCors(builder =>
            {
                // Matches the url and port coming from app-admin
                // TODO: Review AllowCredntials
                // https://docs.microsoft.com/en-us/aspnet/core/security/cors#credentials-in-cross-origin-requests#credentials-in-cross-origin-requests
                builder.WithOrigins("http://localhost:4200")
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        }

        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
#if DEBUG
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pioneer Blog API V1");
            //    c.DocumentTitle = "Pionerr Blog API";
            //});
#endif
        }

        public static void ConfigureRoutes(this IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Post",
                    template: "post/{id}",
                    defaults: new { controller = "Post", action = "Index" });

                routes.MapRoute(
                    name: "BlogPost",
                    template: "post",
                    defaults: new { controller = "blog", action = "Index" });

                routes.MapRoute(
                    name: "BlogTag",
                    template: "tag",
                    defaults: new { controller = "blog", action = "Index" });

                routes.MapRoute(
                    name: "BlogCategory",
                    template: "category",
                    defaults: new { controller = "blog", action = "Index" });

                routes.MapRoute(
                    name: "Category",
                    template: "category/{id}/{page?}",
                    defaults: new { controller = "blog", action = "Category" });

                routes.MapRoute(
                    name: "Tag",
                    template: "tag/{id}/{page?}",
                    defaults: new { controller = "blog", action = "Tag" });

                routes.MapRoute(
                    name: "BlogFlat",
                    template: "blog",
                    defaults: new { controller = "blog", action = "Index" });

                routes.MapRoute(
                    name: "Blog",
                    template: "blog/{page?}",
                    defaults: new { controller = "blog", action = "Index" });

#if (DEBUG)
                routes.MapRoute(
                    name: "Admin",
                    template: "admin",
                    defaults: new { controller = "admin", action = "Index" });
#endif

                routes.MapRoute(
                    name: "SiteMap",
                    template: "sitemap.xml",
                    defaults: new { controller = "home", action = "SiteMap" });

                routes.MapRoute(
                    name: "RssFeed",
                    template: "rss.xml",
                    defaults: new { controller = "home", action = "RssFeed" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}