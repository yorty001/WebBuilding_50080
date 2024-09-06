using Microsoft.AspNetCore.Mvc;

namespace WebBuilding_50080.Controllers
{
    public class PetrolStationsController : Controller
    {
        public IActionResult Index(int isLoggedIn = 0)
        {
            ViewBag.isLoggedIn = isLoggedIn;
            return View();
        }

        public IActionResult UpdateFuelPrice(int isLoggedIn = 0)
        {
            ViewBag.isLoggedIn = isLoggedIn;
            return View();
        }
    }
}
