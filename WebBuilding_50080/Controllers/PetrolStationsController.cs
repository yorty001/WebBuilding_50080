using Microsoft.AspNetCore.Mvc;

namespace WebBuilding_50080.Controllers
{
    public class PetrolStationsController : Controller
    {
        public IActionResult Index(bool isLoggedIn = false)
        {
            ViewBag.isLoggedIn = isLoggedIn;
            return View();
        }

        public IActionResult UpdateFuelPrice(bool isLoggedIn = false)
        {
            ViewBag.isLoggedIn = isLoggedIn;
            return View();
        }
    }
}
