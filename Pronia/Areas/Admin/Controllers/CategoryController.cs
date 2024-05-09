using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService CategoryCategory)
        {
            _categoryService = CategoryCategory;
        }
        public IActionResult Index()
        {
            var future = _categoryService.GetAllCategorys();

            return View(future);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category Category)
        {
            await _categoryService.AddCategory(Category);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var future = _categoryService.GetCategory(x => x.Id == id && x.DeletedDate == null);
            if (future == null)
            {
                throw new NullReferenceException("Bele bir Model yoxdur!!");
            }
            else
            {
                _categoryService.DeleteCategory(id);
                future.DeletedDate = DateTime.UtcNow.AddHours(4);
            }

            return RedirectToAction("Index");
        }


        public IActionResult Update(int id)
        {
            var category = _categoryService.GetCategory(x => x.Id == id && x.DeletedDate == null);
            if (category == null)
            {
                throw new NullReferenceException("Bele bir User yoxdur!!");
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category newCategory)
        {
            _categoryService.UpdateCategory(newCategory.Id, newCategory);
            return RedirectToAction("Index");
        }
    }
}
