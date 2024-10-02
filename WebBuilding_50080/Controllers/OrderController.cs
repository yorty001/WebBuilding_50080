using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using WebBuilding_50080.Models;

namespace WebBuilding_50080.Controllers
{
    public class OrderController : Controller
    {
        
        public IActionResult Index()
        {
            var orderList = new List<Orders>();
            var order1 = new Orders
            {
                firstName = "John",
                totalPrice = 3.45,
                items = "Milk, chocolate, chips"
            };
            var order2 = new Orders
            {
                firstName = "Jane",
                totalPrice = 31.45,
                items = "Milk, chocolate, chips, milkshake"
            };
            var order3 = new Orders
            {
                firstName = "Jude",
                totalPrice = 1.45,
                items = "Milk"
            };
            orderList.Add(order1);
            orderList.Add(order2);  
            orderList.Add(order3);
            HttpContext.Session.SetString("Orders", JsonConvert.SerializeObject(orderList));


            return View(orderList);
        }

    

            [HttpPost]
            public IActionResult SubmitOrders(Dictionary<int, string> orderStatus)
            {
                // Retrieve the order list from session
                var orderJson = HttpContext.Session.GetString("Orders");
                if (orderJson == null)
                {
                    return RedirectToAction("Index");
                }

                // Deserialize the list of orders
                var orders = JsonConvert.DeserializeObject<List<Orders>>(orderJson);

                // Collect IDs of orders to remove
                var idsToRemove = orderStatus
                    .Where(o => o.Value == "Completed" || o.Value == "Cancelled")
                    .Select(o => o.Key)
                    .ToList();

                // Remove orders by IDs
                orders.RemoveAll(o => idsToRemove.Contains(o.orderID));

                // Save the updated list back to session
                HttpContext.Session.SetString("Orders", JsonConvert.SerializeObject(orders));

                return RedirectToAction("Index");
            }
        }
}
