using Business.Services.Abstracts;
using Core.Models;
using Data.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            await _featureService.AddFeature(feature);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var future = _featureService.GetFeature(x=>x.Id == id && x.DeletedDate==null);
            if (future ==null)
            {
                throw new NullReferenceException("Bele bir User yoxdur!!");
            }
            else
            {
                _featureService.DeleteFeature(id);
                future.DeletedDate = DateTime.UtcNow.AddHours(4);
            }
           
            return RedirectToAction("Index");
        }
    

        public IActionResult Update(int id)
        {
            var future = _featureService.GetFeature(x => x.Id == id && x.DeletedDate == null);
            if (future == null)
            {
                throw new NullReferenceException("Bele bir User yoxdur!!");
            }
            return View(future);
        }

        [HttpPost]
        public IActionResult Update(Feature newFeature)
        {
            _featureService.UpdateFeature(newFeature.Id, newFeature);
            return RedirectToAction("Index");
        }


    }
}
