using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuickStartMvcClient.Pages
{
    public class UserModel : PageModel
    {
        [Authorize]
        public void OnGet()
        {

        }
    }
}