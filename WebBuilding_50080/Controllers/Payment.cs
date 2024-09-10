using Microsoft.AspNetCore.Mvc;
using WebBuilding_50080.Services;

namespace server.Controllers
{

    namespace WebBuilding_50080.Controllers
    {
        public class Payment : Controller
        {
            public IActionResult Index()
            {
                return View();
            }

            public IActionResult OrderSummary()
            {
                return View();
            }


        }

    }
}
