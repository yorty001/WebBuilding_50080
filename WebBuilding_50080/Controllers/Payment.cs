using Microsoft.AspNetCore.Mvc;
using WebBuilding_50080.Services;

namespace server.Controllers
{

    namespace WebBuilding_50080.Controllers
    {
        public class Payment : Controller
        {
            private readonly PaymentService _paymentService;

            public Payment(PaymentService paymentService)
            {
                _paymentService = paymentService;
            }

            [HttpPost]
            [Route("create-checkout-session")]
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


        }

    }
}
