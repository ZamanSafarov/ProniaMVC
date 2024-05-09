using Business.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Pronia.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFeatureService _featureService;
        public HomeController(IFeatureService featureService)
        {
            _featureService = featureService;
        }
        public IActionResult Index()
        {
            var future = _featureService.GetAllFeatures();
            return View(future);
        }
    }
}
