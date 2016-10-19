using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public HomeController(RoleManager<IdentityRole> roleManager) { _roleManager = roleManager; createRoles(); }
        private async void createRoles()
        {
            if (!await _roleManager.RoleExistsAsync("Administrator"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Administrator"));
            }

            if (!await _roleManager.RoleExistsAsync("MCI User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("MCI User"));
            }

            if (!await _roleManager.RoleExistsAsync("MCI Manager"))
            {
                await _roleManager.CreateAsync(new IdentityRole("MCI Manager"));
            }
        }

        public IActionResult Index()
        {
            return View();
        }

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
            return View();
        }
    }
}
