
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrendLoop.Data.Models;
using TrendLoop.Services.Data.Interfaces;
using TrendLoop.Web.ViewModels;

namespace TrendLoop.Controllers
{
    public class ProductController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IBrandService brandService;
        private readonly ICategoryService categoryService;
        private readonly ISubcategoryService subcategoryService;
        private readonly IAttributeTypeService attributeTypeService;
        private readonly IProductService productService;

        public ProductController(UserManager<ApplicationUser> userManager,
                                 IBrandService brandService,
                                 ICategoryService categoryService,
                                 ISubcategoryService subcategoryService,
                                 IAttributeTypeService attributeTypeService,
                                 IProductService productService)
        {
            this.userManager = userManager;
            this.brandService = brandService;
            this.categoryService = categoryService;
            this.subcategoryService = subcategoryService;
            this.attributeTypeService = attributeTypeService;
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllProductsIndexViewModel> allProducts = await this.productService.GetAllProductsAsync();

            return this.View(allProducts);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isLoggedIn = await IsUserLoggedInAsync();
            if (!isLoggedIn)
            {
                return this.RedirectToAction(nameof(Index));
            }

            var model = new AddProductViewModel();

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                // TODO return selected brand, category, subcategory etc.
                return this.View(model);
            }

            Guid userId = Guid.Empty;
            bool isIdValid = IsGuidValid(userManager.GetUserId(User), ref userId);
            
            if (isIdValid)
            {
                bool result = await this.productService.AddProductAsync(userId, model);
                if (result == false)
                {
                    return this.View(model);
                }
            }
            
            return this.RedirectToAction(nameof(Index));
        }

        public async Task<JsonResult> GetBrands()
        {
            return Json(await brandService.GetAllBrandsAsync());
        }

        public async Task<JsonResult> GetCategories()
        {
            return Json(await categoryService.GetAllCategoriesAsync());
        }

        public async Task<JsonResult> GetSubcategoriesByCategoryId(int categoryId)
        {
            return Json(await subcategoryService.GetSubcategoriesByCategoryIdAsync(categoryId));
        }

        public async Task<JsonResult> GetAttributeTypesBySubcategoryId(int subcategoryId)
        {
            return Json(await attributeTypeService.GetAttributeTypesBySubcategoryIdAsync(subcategoryId));
        }

        // TODO: move to BaseController
        protected async Task<bool> IsUserLoggedInAsync()
        {
            return userManager.GetUserId(User) == null ? false : true;
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


