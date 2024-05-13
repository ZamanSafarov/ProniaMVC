using Business.Services.Abstracts;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Pronia.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFeatureService _featureService;
        private readonly ISliderService _sliderService;

        public HomeController(IFeatureService featureService, ISliderService sliderService)
        {
            _featureService = featureService;
            _sliderService = sliderService;
        }

        public IActionResult Index()
        {


            HomeVM vm = new HomeVM()
            {
                Sliders = _sliderService.GetAllSliders(),
                Features = _featureService.GetAllFeatures(),
            };
                
            return View(vm);
        }
    }
}
