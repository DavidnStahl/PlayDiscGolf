using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlayDiscGolf.Business.Calculations.Hole;
using PlayDiscGolf.Business.Calculations.ScoreCard;
using PlayDiscGolf.Business.Session;
using PlayDiscGolf.Business.ViewModelBuilder.HoleCard;
using PlayDiscGolf.Business.ViewModelBuilder.PlayerCard;
using PlayDiscGolf.Business.ViewModelBuilder.ScoreCard;
using PlayDiscGolf.Data;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.Services;
using PlayDiscGolf.Services.Admin;
using PlayDiscGolf.Services.CoursePage;
using PlayDiscGolf.Services.Home;
using PlayDiscGolf.Services.Score;
using PlayDiscGolf.Services.User;
using PlayDiscGolf.ViewModels.ScoreCard;

namespace PlayDiscGolf
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            IConfigurationSection  configurationSection = Configuration.GetSection("ConnectionStrings:DefaultConnection");
            services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(configurationSection.Value));

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

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

            services.AddTransient<IAdminCourseService, AdminCourseService>();
            services.AddTransient<IAdminNewCountryCourseService, AdminNewCountryCourseService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IHomeService, HomeService>();            
            services.AddTransient<ICoursePageService, CoursePageService>();
            services.AddTransient<IScoreCardService, ScoreCardService>();
            services.AddTransient<IUserService, UserService>();


            services.AddScoped<ISessionStorage<ScoreCardViewModel>, SessionStorageScoreCardViewModel>();

            services.AddScoped<IScoreCardViewModelBuilder, ScoreCardViewModelBuilder>();
            services.AddScoped<IHoleCardViewModelBuilder, HoleCardViewModelBuilder>();
            services.AddScoped<IPlayerCardViewModelBuilder, PlayerCardViewModelBuilder>();

            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));

            services.AddScoped<IScoreCardCalculation, ScoreCardCalculation>();
            services.AddScoped<ICreateHolesCalculation, CreateHolesCalculation>();


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
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
