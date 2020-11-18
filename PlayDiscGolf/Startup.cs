using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlayDiscGolf.Data;
using PlayDiscGolf.Data.Cards.Scores;
using PlayDiscGolf.Data.Courses;
using PlayDiscGolf.Data.Holes;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.Services;
using PlayDiscGolf.Services.Admin;
using PlayDiscGolf.Services.CoursePage;
using PlayDiscGolf.Services.Home;

namespace PlayDiscGolf
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var configurationSection = Configuration.GetSection("ConnectionStrings:DefaultConnection");
            services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(configurationSection.Value));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

            })
            .AddEntityFrameworkStores<DataBaseContext>().AddDefaultTokenProviders()
            .AddRoles<IdentityRole>();

            services.AddScoped<IAdminCourseService, AdminCourseService>();
            services.AddScoped<IAdminNewCountryCourseService, AdminNewCountryCourseService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<ICoursePageService, CoursePageService>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IHoleRepository,HoleRepository>();
            services.AddScoped<IScoreCardRepository, ScoreCardRepository>();


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddResponseCaching();
            services.AddHttpContextAccessor();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseResponseCaching();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
