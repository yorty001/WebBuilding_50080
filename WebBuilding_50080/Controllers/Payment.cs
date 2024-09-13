using Microsoft.AspNetCore.Mvc;
using WebBuilding_50080.Models;
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
            public IActionResult Index()
            {
                return View();
            }

            public IActionResult PaymentDetails()
            {
                ViewBag.CardName = HttpContext.Session.GetString("CardName");
                ViewBag.CardNumber = HttpContext.Session.GetString("CardNumber");
                ViewBag.ExpMonth = HttpContext.Session.GetString("ExpMonth");
                ViewBag.ExpYear = HttpContext.Session.GetString("ExpYear");
                ViewBag.CVV = HttpContext.Session.GetString("CVV");

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
