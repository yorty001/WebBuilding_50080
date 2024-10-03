using Microsoft.AspNetCore.Mvc;

namespace WebBuilding_50080.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
