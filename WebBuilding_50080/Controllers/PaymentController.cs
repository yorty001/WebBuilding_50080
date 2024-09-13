using Microsoft.AspNetCore.Mvc;


namespace server.Controllers
{

    namespace WebBuilding_50080.Controllers
    {
        public class Payment : Controller
        {


            [HttpPost]
            public IActionResult CreateCheckoutSession([FromBody] List<Cartitem> cartItems)
            {
                string sessionUrl = _paymentService.CheckoutSession(cartItems);

                Response.Headers.Add("location", sessionUrl);
                return new StatusCodeResult(303);
            }
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

            public IActionResult UpdatePaymentDetails()
            {
                return View();
            }

            [HttpPost]
            public IActionResult PaymentDetails(string CardName, string CardNumber, string ExpMonth, string ExpYear, string CVV)
            {
 
                HttpContext.Session.SetString("CardName", CardName);
                HttpContext.Session.SetString("CardNumber", CardNumber);
                HttpContext.Session.SetString("ExpMonth", ExpMonth);
                HttpContext.Session.SetString("ExpYear", ExpYear);
                HttpContext.Session.SetString("CVV", CVV);

                return RedirectToAction("PaymentDetails");
            }

        }

    }

    }


