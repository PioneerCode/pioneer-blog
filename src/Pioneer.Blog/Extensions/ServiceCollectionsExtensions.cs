using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.PlatformAbstractions;
using Pioneer.Blog.Repository;
using Pioneer.Blog.Service;
using Pioneer.Pagination;

namespace Pioneer.Blog.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static void ConfigureServicesCookies(this IServiceCollection services)
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

        public static void ConfigureServicesJwt(this IServiceCollection services, IConfiguration config)
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
                        ValidIssuer = config.GetSection("AppConfiguration:SiteUrl").Value,
                        ValidAudience = config.GetSection("AppConfiguration:SiteUrl").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppConfiguration:Key").Value))
                    };
                });
        }

        public static void ConfigureServicesSwagger(this IServiceCollection services)
        {
#if DEBUG
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info { Title = "Pioneer Blog API", Version = "v1" });
            //    c.OperationFilter<AddAuthTokenHeaderParameter>();
            //});
#endif
        }

        public static void RegisterDependencies(this IServiceCollection services)
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
    }
}
