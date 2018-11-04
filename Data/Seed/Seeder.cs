using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using IdentityDemo.Models;
using IdentityDemo.Data.Seed;
using System.Threading.Tasks;

namespace IdentityDemo.Data
{
    public static class Seeder
    {
        public async static Task Seed(
            string jsonUsers, 
            string jsonPeople,
            IServiceProvider serviceProvider,
            UserManager<ApplicationUser> userManager)
        {
            // seed users
            var users = JsonConvert.DeserializeObject<List<UserDto>>(jsonUsers);
            if (!userManager.Users.Any())
            {
                foreach (var user in users)
                {
                    var applicationUser = new ApplicationUser
                    {
                        UserName = user.Login,
                        Email = user.Login + "@test.com",
                        Name = user.Name
                    };

                    var result = await userManager.CreateAsync(applicationUser, user.Password);
                }
            }

            // seed people

            return;
        }
    }
}
