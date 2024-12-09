using Microsoft.AspNetCore.Mvc;
using TrendLoop.Data.Models;
using TrendLoop.Services.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using TrendLoop.Web.ViewModels.Product;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Index = Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor.Index;

namespace TrendLoop.Controllers
{
    public class PurchaseController : BaseController
    {
        private readonly IPurchaseService purchaseService;

        public PurchaseController(IPurchaseService purchaseService, UserManager<ApplicationUser> userManager) : base(userManager)
        {
            this.purchaseService = purchaseService;

        }

        [HttpGet]
        public async Task<IActionResult> Buy(string Id)
        {
            // Check Product ID
            Guid productGuid = Guid.Empty;
            if (!this.IsGuidValid(Id, ref productGuid))
            {
                // if Product GUID is not valid redirect to all products page
                return this.RedirectToAction(nameof(Index), "Product");
            }

            // Check User ID
            Guid userGuid = Guid.Empty;
            if (!this.IsGuidValid(userManager.GetUserId(User), ref userGuid))
            {
                // if user GUID is not valid redirect to all products page
                return this.RedirectToAction(nameof(Index), "Product");
            }

            BuyProductViewModel? model = await this.purchaseService.GetProductToBuyAsync(productGuid);

            if (model == null)
            {
                return this.RedirectToAction(nameof(Details), "Product", new { id = productGuid });
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Buy(BuyProductViewModel model)
        {
            // Check Product ID is valid
            Guid productGuid = Guid.Empty;
            if (!this.IsGuidValid(model.Id, ref productGuid))
            {
                return this.RedirectToAction(nameof(Index), "Product");
            }

            // Check User ID is valid
            Guid buyerGuid = Guid.Empty;
            if (!this.IsGuidValid(userManager.GetUserId(User), ref buyerGuid))
            {
                return this.RedirectToAction(nameof(Details), "Product", new { id = model.Id });
            }

            bool isBought = await purchaseService.ExecuteBuyProductAsync(productGuid, buyerGuid);

            if (!isBought)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while buying the product! Please contact administrator");
                return this.View(model);
            }

            return this.RedirectToAction(nameof(Index), "Product", new { id = model.Id });
        }
    }
}


