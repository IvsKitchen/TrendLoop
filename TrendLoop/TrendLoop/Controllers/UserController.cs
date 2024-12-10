using Microsoft.AspNetCore.Mvc;
using TrendLoop.Data.Models;
using TrendLoop.Services.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using TrendLoop.Web.ViewModels.User;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace TrendLoop.Controllers
{
    public class UserController : BaseController
    {
        private readonly IProductService productService;

        public UserController(IProductService productService, UserManager<ApplicationUser> userManager) : base(userManager)
        {
            this.productService = productService;

        }

        [HttpGet]
        public async Task<IActionResult> Wardrobe()
        {
            var userId = userManager.GetUserId(User);

            // Check User ID
            Guid userGuid = Guid.Empty;
            if (!this.IsGuidValid(userManager.GetUserId(User), ref userGuid))
            {
                return this.RedirectToAction(nameof(Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor.Index), "Product");
            }

            // Retrieve products for sale
            var productsForSale = await productService.GetProductsForSaleByUserAsync(userGuid);
            // Retrieve bought products
            var boughtProducts = await productService.GetBoughtProductsByUserAsync(userGuid);
            
            var model = new UserProductsViewModel
            {
                // Assign to model
                ProductsForSale = productsForSale,
                BoughtProducts = boughtProducts
            };

            return View(model);
        }
    }
}


