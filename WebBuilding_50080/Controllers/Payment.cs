using Microsoft.AspNetCore.Mvc;

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

            [HttpPost]
            public IActionResult OrderSummary()
            {
                return View();
            }


        }

    }
}
