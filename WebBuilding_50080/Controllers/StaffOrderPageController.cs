using Microsoft.AspNetCore.Mvc;
using WebBuilding_50080.Models;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace WebBuilding_50080.Controllers
{
    public class StaffOrderPageController : Controller
    {
        private readonly string accountSid = Environment.GetEnvironmentVariable("TwilioAccSID");
        private readonly string authToken = Environment.GetEnvironmentVariable("AccountToken");
        private readonly string twilioPhoneNumber = Environment.GetEnvironmentVariable("Twilio ph No");
        public IActionResult Index()
        {
            var orders = CartController.Orders;
            return View(orders);
        }

        [HttpPost]
        public IActionResult MarkAsReady(int orderId)
        {
            var order = CartController.Orders.Find(o => o.OrderId == orderId);
            if (order != null)
            {
                order.IsReady = true;

                SendSmsNotification(order);
            }

            return RedirectToAction("Index", "StaffOrderPage");
        }

        private void SendSmsNotification(Order order)
        {
            TwilioClient.Init(accountSid, authToken);

            var customerPhoneNumber = new PhoneNumber("+MyPhoneNumber");

            var message = $"Hey! Your UTR order with ID {order.OrderId} is now ready for pickup! Total Price: ${order.TotalPrice}";

            var messageResponse = MessageResource.Create(
                body: message,
                from: new PhoneNumber(twilioPhoneNumber),
                to: customerPhoneNumber
                );
        }
    }
}
