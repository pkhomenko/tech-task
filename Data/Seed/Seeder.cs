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
            if (!userManager.Users.Any())
            {
                var users = JsonConvert.DeserializeObject<List<UserDto>>(jsonUsers);

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
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                if (!context.PeopleAccounts.Any())
                {
                    var accounts = JsonConvert.DeserializeObject<List<PeopleAccountDto>>(jsonPeople);

                    foreach (var account in accounts)
                    {
                        var peopleAccount = new PeopleAccount()
                        {
                            Id = account.Guid,
                            About = account.About,
                            Address = account.Address,
                            Company = account.Company,
                            Email = account.Email,
                            Gender = account.Gender,
                            Name = account.Name,
                            Phone = account.Phone,
                            Picture = account.Picture,
                            Registered = account.Registered
                        };

                        peopleAccount.Tags = account.Tags.Select(
                            x => new Tag()
                            {
                                Id = Guid.NewGuid(),
                                PeopleAccount = peopleAccount,
                                PeopleAccountId = peopleAccount.Id,
                                Value = x
                            }).ToList();

                        context.PeopleAccounts.Add(peopleAccount);
                    }

                    await context.SaveChangesAsync();
                }
            }

            return;
        }
    }
}
