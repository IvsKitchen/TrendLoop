﻿using Microsoft.AspNetCore.Mvc;
using TrendLoop.Data.Models;
using TrendLoop.Services.Data.Interfaces;
using TrendLoop.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using TrendLoop.Web.ViewModels.User;

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
            var productsForSale = await productService.GetProductsForSaleByUserAsync(userGuid);

            var model = new UserProductsViewModel
            {
                BoughtProducts = boughtProducts,
                ProductsForSale = productsForSale
            };

            return View(model);
        }
    }
}

