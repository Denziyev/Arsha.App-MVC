using Microsoft.AspNetCore.Mvc;

namespace Arsha.App.areas.Admin.Controllers
{
    public class HomeController:Controller
    {
        [Area("Admin")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
