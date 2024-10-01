using Microsoft.AspNetCore.Mvc;
using WebBuilding_50080.Models;

namespace WebBuilding_50080.Controllers
{
    public class OrderManagementController : Controller
    {
        public IActionResult Index()
        {
            return View(CartController.Orders);
        }

        public IActionResult MarkAsReady(int orderId)
        {
            var order = CartController.Orders.Find(o => o.OrderId == orderId);
            if (order != null)
            {
                order.IsReady = true;
            }

            return RedirectToAction("Index");
        }
    }
}
