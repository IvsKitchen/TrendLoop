using Microsoft.AspNetCore.Mvc;
using TrendLoop.Services.Data.Interfaces;
using TrendLoop.Web.ViewModels;

namespace TrendLoop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllProductsIndexViewModel> allProducts =
                await this.productService.GetAllProductsAsync();

            return this.View(allProducts);
        }
    }
}
