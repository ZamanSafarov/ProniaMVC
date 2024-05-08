using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Pronia.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
