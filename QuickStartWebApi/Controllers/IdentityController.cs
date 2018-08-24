using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuickStartWebApi.Controllers
{
    [Route("identity")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            var users = from claim in User.Claims
                        select new
                        {
                            claim.Type,
                            claim.Value
                        };

            return new JsonResult(users);
        }
    }
}