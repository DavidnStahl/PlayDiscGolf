using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayDiscGolf.Models.Models.DataModels
{
    public class DatabaseInitializer
    {
        public void Initialize(DataBaseContext context)
        {
            context.Database.Migrate();
        }

        public void InitializePlayDiscGolf(DataBaseContext context, UserManager<IdentityUser> userManager)
        {
            context.Database.Migrate();
            SeedDataIdentity(context, userManager);
        }
        private void SeedDataIdentity(DataBaseContext context, UserManager<IdentityUser> userManager)
        {
            AddRoleIfNotExists(context, "Admin");
            AddRoleIfNotExists(context, "User");

            AddIfNotExists(userManager, "DavidStahl", "Admin");

        }
        private void AddRoleIfNotExists(DataBaseContext context, string role)
        {
            if (context.Roles.Any(r => r.Name == role)) return;
            context.Roles.Add(new IdentityRole { Name = role, NormalizedName = role });
            context.SaveChanges();
        }

        private void AddIfNotExists(UserManager<IdentityUser> userManager, string user, string role)
        {
            if (userManager.FindByEmailAsync(user).Result == null)
            {
                var u = new IdentityUser
                {
                    UserName = user,
                    Email = "david.n.stahl@gmail.com"                   
                };

                var result = userManager.CreateAsync(u, "Hejsan123!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(u, role).Wait();
                }
            }
        }


    }
}
