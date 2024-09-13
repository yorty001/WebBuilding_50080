using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Data.SqlClient;
using System.Diagnostics;

namespace server.Controllers
{

    namespace WebBuilding_50080.Controllers
    {
        public class Payment : Controller
        {
            private readonly SqlConnection _db;

            public Payment(SqlConnection db)
            {
                _db = db;
            }
            public IActionResult Index([FromQuery] string name, [FromQuery] decimal price)
            {
                ViewBag.Name = name;
                ViewBag.Price = price;
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
