using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuickStartMvcClient.Controllers
{
    public class UserController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public async Task LogOut()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }
    }
}