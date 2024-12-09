using Microsoft.AspNetCore.Mvc;
using TrendLoop.Data.Models;
using TrendLoop.Services.Data.Interfaces;
using TrendLoop.Web.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace TrendLoop.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProductService productService;

        public UserController(UserManager<ApplicationUser> userManager, IProductService productService)
        {
            this.userManager = userManager;
            this.productService = productService;

        }

        [HttpGet]
        public async Task<IActionResult> Products()
        {
            var userId = userManager.GetUserId(User);

            // Check User ID
            Guid userGuid = Guid.Empty;
            if (!this.IsGuidValid(userManager.GetUserId(User), ref userGuid))
            {
                return this.RedirectToAction(nameof(Index));
            }

            var boughtProducts = await productService.GetBoughtProductsByUserAsync(userGuid);
            var productsForSale = await productService.GetSelledProductsByUserAsync(userGuid);

            var model = new UserProductsViewModel
            {
                BoughtProducts = boughtProducts,
                ProductsForSale = productsForSale
            };

            return View(model);
        }

        // TODO: move to BaseController
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


