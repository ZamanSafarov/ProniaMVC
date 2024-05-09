using Business.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Pronia.Controllers
{
    public class ShopController : Controller
    {
        private readonly ICategoryService _categoryService;
        public ShopController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategorys();
            return View(categories);
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}
