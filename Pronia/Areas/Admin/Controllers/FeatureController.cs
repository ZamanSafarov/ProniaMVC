using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Data.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "SuperAdmin")]
	public class FeatureController : Controller
    {
        private readonly IFeatureService _featureService;
        public FeatureController(IFeatureService featureService)
        {
            _featureService= featureService;
        }
        public IActionResult Index()
        {
           var future =  _featureService.GetAllFeatures();

            return View(future);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Feature feature)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _featureService.AddFeature(feature);
            }
            catch (DuplicateEntityException ex)
            {
                ModelState.AddModelError("Title",ex.Message);
                return View();
            }
       
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var future = _featureService.GetFeature(x=>x.Id == id && x.DeletedDate==null);
            try
            {
                _featureService.DeleteFeature(id);
            }
            catch (EntityNotFoundException ex)
            {
               return NotFound();
            }
         
           
            return RedirectToAction("Index");
        }
    

        public IActionResult Update(int id)
        {
            var future = _featureService.GetFeature(x => x.Id == id && x.DeletedDate == null);

            if (future is null)
            {
                return NotFound();
            }
       
            return View(future);
        }

        [HttpPost]
        public IActionResult Update(Feature newFeature)
        {
            if (newFeature is null)
            {
                return NotFound();
            }
            else if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _featureService.UpdateFeature(newFeature.Id, newFeature);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch(DuplicateEntityException ex) {
                ModelState.AddModelError("Title",ex.Message);
                return View(ex);
            }
            return RedirectToAction("Index");
        }


    }
}
