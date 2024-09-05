using Microsoft.AspNetCore.Mvc;

namespace WebBuilding_50080.Controllers
{
    public class PetrolStationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
