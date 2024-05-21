using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "SuperAdmin")]
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
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                await _categoryService.AddCategory(Category);
            }
            catch (DuplicateEntityException ex)
            {
                ModelState.AddModelError("Name",ex.Message);
                return View();
            }
           
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var future = _categoryService.GetCategory(x => x.Id == id && x.DeletedDate == null);

            if (future == null)
            {
                return  NotFound();
            }
            else
            {
                _categoryService.DeleteCategory(id);
            }

            return RedirectToAction("Index");
        }


        public IActionResult Update(int id)
        {
            var category = _categoryService.GetCategory(x => x.Id == id && x.DeletedDate == null);
            if (category == null)
            {
               return  NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category newCategory)
        {
            if (newCategory is null)
            {
                return NotFound();
            }
            else if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _categoryService.UpdateCategory(newCategory.Id, newCategory);
            }
            catch (DuplicateEntityException ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View();
            }
            catch (EntityNotFoundException ex) 
            {
                return NotFound();
            }
           
            return RedirectToAction("Index");
        }
    }
}
