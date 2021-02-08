using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlayDiscGolf.Core.Business.Calculations.Hole;
using PlayDiscGolf.Core.Business.Calculations.ScoreCard;
using PlayDiscGolf.Core.Business.DtoBuilder.HoleCard;
using PlayDiscGolf.Core.Business.DtoBuilder.PlayerCard;
using PlayDiscGolf.Core.Business.DtoBuilder.ScoreCard;
using PlayDiscGolf.Core.Business.Session;
using PlayDiscGolf.Core.Dtos.Cards;
using PlayDiscGolf.Core.Services.Account;
using PlayDiscGolf.Core.Services.Admin;
using PlayDiscGolf.Core.Services.CoursePage;
using PlayDiscGolf.Core.Services.Home;
using PlayDiscGolf.Core.Services.Score;
using PlayDiscGolf.Core.Services.User;
using PlayDiscGolf.Infrastructure.Repository.Generic;
using PlayDiscGolf.Infrastructure.Repository.Specific;
using PlayDiscGolf.Infrastructure.Repository.Specific.Interface;
using PlayDiscGolf.Infrastructure.UnitOfWork;
using PlayDiscGolf.Models.Models.DataModels;
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

            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IHomeService, HomeService>();            
            services.AddScoped<ICoursePageService, CoursePageService>();
            services.AddScoped<IScoreCardService, ScoreCardService>();
            services.AddScoped<IUserService, UserService>();


            services.AddScoped<ISessionStorage<ScoreCardDto>, SessionStorageScoreCardDto>();

            services.AddScoped<IScoreCardDtoBuilder, ScoreCardDtoBuilder>();
            services.AddScoped<IHoleCardDtoBuilder, HoleCardDtoBuilder>();
            services.AddScoped<IPlayerCardDtoBuilder, PlayerCardDtoBuilder>();
            services.AddScoped<IUnitOfwork, UnitOfWork>();

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
