
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TrendLoop.Data.Models;
using TrendLoop.Services.Data.Interfaces;
using TrendLoop.Web.ViewModels.Product;

namespace TrendLoop.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IUserService userService;
        private readonly IBrandService brandService;
        private readonly ICategoryService categoryService;
        private readonly ISubcategoryService subcategoryService;
        private readonly IAttributeTypeService attributeTypeService;
        private readonly IProductService productService;
        private readonly IBlobService blobService;

        public ProductController(UserManager<ApplicationUser> userManager,
                                 IUserService userService,
                                 IBrandService brandService,
                                 ICategoryService categoryService,
                                 ISubcategoryService subcategoryService,
                                 IAttributeTypeService attributeTypeService,
                                 IProductService productService,
                                 IBlobService blobService) : base(userManager)
        {
            this.userService = userService;
            this.brandService = brandService;
            this.categoryService = categoryService;
            this.subcategoryService = subcategoryService;
            this.attributeTypeService = attributeTypeService;
            this.productService = productService;
            this.blobService = blobService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(ProductsViewModel model)
        {
            IEnumerable<ProductViewModel> allProducts = await this.productService.GetAllProductsAsync(model);

            model.Products = allProducts;
            model.AllBrands = await brandService.GetAllBrandsNamesAsync();
            model.AllCategories = await categoryService.GetAllCategoriesNamesAsync();
            model.AllSubcategories = await subcategoryService.GetAllSubcategoriesNamesAsync();
            model.TotalItems = await this.productService.GetAllProductsCountAsync(model);

            model.TotalPages = model.PageSize == null ? 1 : (int)Math.Ceiling((double)model.TotalItems / model.PageSize.Value);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await productService.GetProductDetailsAsync(id);
            return View(model);
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
            model.Brands = await brandService.GetAllBrandsAsync();
            model.Categories = await categoryService.GetAllCategoriesAsync();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Brands = await brandService.GetAllBrandsAsync();
                model.Categories = await categoryService.GetAllCategoriesAsync();
                return this.View(model);
            }

            Guid userId = Guid.Empty;
            bool isIdValid = IsGuidValid(userManager.GetUserId(User), ref userId);

            if (isIdValid)
            {
                // check if user has uploaded image file instead of URL
                if (model.ImageFile != null)
                {
                    // upload file in Blob service and set the new URL
                    model.ImageUrl = await blobService.UploadFileAsync(model.ImageFile);
                }

                bool result = await this.productService.AddProductAsync(userId, model);
                if (result == false)
                {
                    return this.View(model);
                }
            }

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(string? id)
        {
            // Check Product ID
            Guid productGuid = Guid.Empty;
            if (!this.IsGuidValid(id, ref productGuid))
            {
                return this.RedirectToAction(nameof(Index));
            }

            // Check User ID
            Guid userGuid = Guid.Empty;
            if (!this.IsGuidValid(userManager.GetUserId(User), ref userGuid))
            {
                return this.RedirectToAction(nameof(Index));
            }

            // Check user is the product seller
            bool isSeller = await userService.IsUserProductSeller(userGuid, productGuid);
            if (!isSeller)
            {
                return this.RedirectToAction(nameof(Details), "Product", new { id = productGuid });
            }

            EditProductViewModel? model = await this.productService
                .GetProductToEditAsync(productGuid);

            if (model == null)
            {
                return this.RedirectToAction(nameof(Details), "Product", new { id = productGuid });
            }

            model.Brands = await brandService.GetAllBrandsAsync();
            model.Categories = await categoryService.GetAllCategoriesAsync();

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            // Check Product ID
            Guid productGuid = Guid.Empty;
            if (!this.IsGuidValid(model.Id, ref productGuid))
            {
                return this.RedirectToAction(nameof(Index));
            }

            // Check User ID
            Guid userGuid = Guid.Empty;
            if (!this.IsGuidValid(userManager.GetUserId(User), ref userGuid))
            {
                return this.RedirectToAction(nameof(Index));
            }

            // Check user is the product seller
            bool isSeller = await userService.IsUserProductSeller(userGuid, productGuid);
            if (!isSeller)
            {
                this.RedirectToAction(nameof(Details), "Product", new { id = productGuid });
            }

            if (!ModelState.IsValid)
            {
                model.Brands = await brandService.GetAllBrandsAsync();
                model.Categories = await categoryService.GetAllCategoriesAsync();
                return this.View(model);
            }

            // check if user has uploaded image file instead of URL
            if (model.ImageFile != null)
            {
                // upload file in Blob service and set the new URL
                model.ImageUrl = await blobService.UploadFileAsync(model.ImageFile);
            }

            bool isUpdated = await this.productService.EditProductAsync(productGuid, model);
            if (!isUpdated)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while updating the cinema! Please contact administrator");
                return this.View(model);
            }

            return this.RedirectToAction(nameof(Details), "Product", new { id = model.Id });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(string? id)
        {
            // Check Product ID is valid
            Guid productGuid = Guid.Empty;
            if (!this.IsGuidValid(id, ref productGuid))
            {
                return this.RedirectToAction(nameof(Index));
            }

            // Check User ID is valid
            Guid userGuid = Guid.Empty;
            if (!this.IsGuidValid(userManager.GetUserId(User), ref userGuid))
            {
                return this.RedirectToAction(nameof(Index));
            }

            // Check user is the product seller
            bool isSeller = await userService.IsUserProductSeller(userGuid, productGuid);
            if (!isSeller)
            {
                return this.RedirectToAction(nameof(Index));
            }

            DeleteProductViewModel? productToDeleteViewModel =
                await this.productService.GetProductForDeleteByIdAsync(productGuid);
            
            if (productToDeleteViewModel == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(productToDeleteViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SoftDeleteConfirmed(DeleteProductViewModel model)
        {
            // Check Product ID
            Guid productGuid = Guid.Empty;
            if (!this.IsGuidValid(model.Id, ref productGuid))
            {
                return this.RedirectToAction(nameof(Index));
            }

            // Check User ID
            Guid userGuid = Guid.Empty;
            if (!this.IsGuidValid(userManager.GetUserId(User), ref userGuid))
            {
                return this.RedirectToAction(nameof(Index));
            }

            // Check user is the product seller
            bool isSeller = await userService.IsUserProductSeller(userGuid, productGuid);
            if (!isSeller)
            {
                return this.RedirectToAction(nameof(Index));
            }

            bool isDeleted = await this.productService
                .SoftDeleteProductAsync(productGuid);
            if (!isDeleted)
            {
                TempData["ErrorMessage"] =
                    "Unexpected error occurred while trying to delete the cinema! Please contact system administrator!";
                return this.RedirectToAction(nameof(Delete), new { id = model.Id });
            }

            // TODO change to wardrobe when implemented
            return this.RedirectToAction(nameof(Index));
        }

        public async Task<JsonResult> GetSubcategoriesByCategoryId(int categoryId)
        {
            return Json(await subcategoryService.GetSubcategoriesByCategoryIdAsync(categoryId));
        }

        public async Task<JsonResult> GetAttributeTypesBySubcategoryId(int subcategoryId)
        {
            return Json(await attributeTypeService.GetAttributeTypesBySubcategoryIdAsync(subcategoryId));
        }
    }
}


