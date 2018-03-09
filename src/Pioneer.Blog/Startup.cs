using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Pioneer.Blog.Entity;
using Pioneer.Blog.Model;
using Pioneer.Blog.Repository;
using Pioneer.Blog.Service;
using Pioneer.Pagination;
using Swashbuckle.AspNetCore.Swagger;

namespace Pioneer.Blog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public  IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BlogDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<UserEntity, IdentityRole>()
                .AddEntityFrameworkStores<BlogDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<AppConfiguration>(Configuration.GetSection("AppConfiguration"));

            ConfigureServicesCookies(services);
            ConfigureServicesJwt(services, Configuration);
            ConfigureServicesSwagger(services);
            RegisterDependencies(services);

            services.AddCors();

            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("isSuperUser", p => p.RequireClaim("isSuperUser", "true"));
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ServiceMapperConfig.Config();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseAuthentication();
            app.UseStaticFiles();

            ConfigureCors(app);
            ConfigureSwagger(app);
            ConfigureMvc(app);
        }

        private static void ConfigureServicesCookies(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Events =
                    new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = ctx =>
                        {
                            // If we were redirect to login from api..
                            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                            {
                                ctx.Response.StatusCode = 401;
                                return Task.FromResult<object>(null);
                            }

                            ctx.Response.Redirect(ctx.RedirectUri);
                            return Task.FromResult<object>(null);
                        },
                        OnRedirectToAccessDenied = ctx =>
                        {
                            // If access is denied from api...
                            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                            {
                                ctx.Response.StatusCode = 403;
                                return Task.FromResult<object>(null);
                            }

                            ctx.Response.Redirect(ctx.RedirectUri);
                            return Task.FromResult<object>(null);
                        }
                    };
            });
        }

        private static void ConfigureServicesJwt(IServiceCollection services, IConfiguration configureation)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configureation.GetSection("AppConfiguration:SiteUrl").Value,
                        ValidAudience = configureation.GetSection("AppConfiguration:SiteUrl").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configureation.GetSection("AppConfiguration:Key").Value))
                    };
                });
        }

        private static void ConfigureServicesSwagger(IServiceCollection services)
        {
#if DEBUG
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Pioneer Blog API", Version = "v1" });
                c.OperationFilter<AddAuthTokenHeaderParameter>();
            });
#endif
        }

        private static void RegisterDependencies(IServiceCollection services)
        {
            // Third-party
            services.AddTransient<IPaginatedMetaService, PaginatedMetaService>();

            // Repositories
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IPostTagRepository, PostTagRepository>();

            // Services
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IPostTagService, PostTagService>();
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<ICommunicationService, CommunicationService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<ISiteMapService, SiteMapService>();
            services.AddTransient<ApplicationEnvironment>();
            services.AddTransient<IRssService, RssService>();
        }

        private static void ConfigureCors(IApplicationBuilder app)
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

        private static void ConfigureSwagger(IApplicationBuilder app)
        {
#if DEBUG
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pioneer Blog API V1");
                c.DocumentTitle = "Pionerr Blog API";
            });
#endif
        }

        private static void ConfigureMvc(IApplicationBuilder app)
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
