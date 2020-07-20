using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pioneer.Blog.Entites;
using Pioneer.Blog.Extensions;
using Pioneer.Blog.Models;
using Pioneer.Blog.Repositories;

namespace Pioneer.Blog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddDbContext<BlogDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<UserEntity, IdentityRole>()
                .AddEntityFrameworkStores<BlogDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<AppConfiguration>(Configuration.GetSection("AppConfiguration"));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Keep public contracts clean
            services.ConfigureServicesCookies();
            services.ConfigureServicesJwt(Configuration);
            services.ConfigureServicesSwagger();
            services.RegisterDependencies();

            //services.AddCors();

            //services.AddAuthorization(cfg =>
            //{
            //    cfg.AddPolicy("isSuperUser", p => p.RequireClaim("isSuperUser", "true"));
            //});

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseAuthentication();
            //app.UseCookiePolicy();

            //app.ConfigureCors();
            app.ConfigureSwagger();
            app.PoineerUseEndpoints();
        }
    }
}
