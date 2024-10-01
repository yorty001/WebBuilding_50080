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
        private const string accountSid = "AC3b978dd513c6f6c36d9e6f5707109476";
        private const string authToken = "536aeca0445546d3959a687b00172af1";
        private const string twilioPhoneNumber = "+13342928698";
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

            var customerPhoneNumber = new PhoneNumber("+61493634676");

            var message = $"Hey! Your UTR order with ID {order.OrderId} is now ready for pickup! Total Price: ${order.TotalPrice}";

            var messageResponse = MessageResource.Create(
                body: message,
                from: new PhoneNumber(twilioPhoneNumber),
                to: customerPhoneNumber
                );
        }
    }
}
