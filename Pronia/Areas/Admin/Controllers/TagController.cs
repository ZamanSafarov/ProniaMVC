using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "SuperAdmin")]
	public class TagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
