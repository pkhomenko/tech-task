using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using IdentityDemo.Data;

namespace IdentityDemo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext _applicationDbContext;

        public HomeController(
            ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            var peopleAccounts = _applicationDbContext
                .PeopleAccounts
                .Include(x => x.Tags)
                .ToList();

            var vm = peopleAccounts.Select(peopleAccount => new PeopleAccountViewModel()
            {
                Id = peopleAccount.Id,
                Company = peopleAccount.Company,
                Email = peopleAccount.Email,
                Name = peopleAccount.Name,
                Phone = peopleAccount.Phone,
                Tags = peopleAccount.Tags.Select(tag => tag.Value).ToList()
            });

            return View(vm);
        }

        public IActionResult UsersWithTag(string tagName)
        {
            var peopleAccounts = _applicationDbContext
                .PeopleAccounts
                .Include(x => x.Tags)
                .Where(x => x.Tags.Any(tag => tag.Value == tagName))
                .ToList();

            var vm = peopleAccounts.Select(peopleAccount => new PeopleAccountViewModel()
            {
                Id = peopleAccount.Id,
                Company = peopleAccount.Company,
                Email = peopleAccount.Email,
                Name = peopleAccount.Name,
                Phone = peopleAccount.Phone,
                Tags = peopleAccount.Tags.Select(tag => tag.Value).ToList()
            });

            ViewData["IsTagFiltered"] = true;

            return View("Index", vm);
        }

        public IActionResult UserDetails(Guid id)
        {
            var user = _applicationDbContext
                .PeopleAccounts
                .Include(x => x.Tags)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (user != null)
            {
                var vm = new PeopleAccountViewModel()
                {
                    Id = user.Id,
                    Company = user.Company,
                    Email = user.Email,
                    Name = user.Name,
                    Phone = user.Phone,
                    About = user.About,
                    Address = user.Address,
                    Gender = user.Gender,
                    Picture = user.Picture,
                    Tags = user.Tags.Select(tag => tag.Value).ToList()
                };

                return View(vm);
            }

            return View(null);
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
