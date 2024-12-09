using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrendLoop.Data.Models;

namespace TrendLoop.Controllers
{
    public class BaseController : Controller
    {
        protected readonly UserManager<ApplicationUser> userManager;

        public BaseController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }


        protected async Task<bool> IsUserLoggedInAsync()
        {
            return userManager.GetUserId(User) == null ? false : true;
        }

        protected bool IsGuidValid(string? id, ref Guid parsedGuid)
        {
            // Non-existing parameter in the URL
            if (String.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            // Invalid parameter in the URL
            bool isGuidValid = Guid.TryParse(id, out parsedGuid);
            if (!isGuidValid)
            {
                return false;
            }

            return true;
        }
    }
}
