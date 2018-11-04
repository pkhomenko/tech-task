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
                Company = peopleAccount.Company,
                Email = peopleAccount.Email,
                Name = peopleAccount.Name,
                Phone = peopleAccount.Phone,
                Tags = peopleAccount.Tags.Select(tag => tag.Value).ToList()
            });

            return View(vm);
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
