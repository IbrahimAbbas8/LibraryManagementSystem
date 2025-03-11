using LibraryManagementSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Infrastructure.Data.Config
{
    public class IdentitySeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Ibrahim",
                    Email = "ibrahim@gmail.com",
                    UserName = "ibrahim@gmail.com"
                };
                await userManager.CreateAsync(user, "P@$$w0rd");
            }
        }
    }
}
