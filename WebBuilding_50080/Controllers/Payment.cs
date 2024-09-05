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
            public IActionResult ProcessPayment(int customerID, decimal amount)
            {
                bool paymentSuccess = _paymentService.ProcessPayment(customerID, amount);

                if (paymentSuccess)
                {
                    return Ok(new { message = "Payment Processed successfully." });
                }
                else
                {
                    return BadRequest(new { message = "Payment processing failed." });
                }
            }
            public IActionResult Index()
            {
                return View();
            }


        }

    }
}
