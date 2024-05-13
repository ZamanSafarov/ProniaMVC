using Business.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Pronia.Controllers
{
    public class ShopController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public ShopController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategorys();
            return View(categories);
        }

        public IActionResult Detail()
        {
            var products =  _productService.GetAllProducts();
            return View(products);
        }
    }
}
