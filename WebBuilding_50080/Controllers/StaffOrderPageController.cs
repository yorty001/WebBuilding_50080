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

            }

            return RedirectToAction("Index", "StaffOrderPage");
        }

    }
}
