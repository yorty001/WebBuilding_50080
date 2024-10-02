using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

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
                orderID = 1,
                firstName = "John",
                totalPrice = 3.45,
                items = "Milk, chocolate, chips",
                status = "Pending"
            };
            var order2 = new Orders
            {
                orderID = 2,
                firstName = "Jane",
                totalPrice = 31.45,
                items = "Milk, chocolate, chips, milkshake",
                status = "Pending"
            };
            var order3 = new Orders
            {
                orderID = 3,
                firstName = "Jude",
                totalPrice = 1.45,
                items = "Milk",
                status = "Pending"
            };
            var order4 = new Orders
            {
                orderID = 4,
                firstName = "Gavin",
                totalPrice = 8.99,
                items = "2L Coke, 4L icecream",
                status = "Pending"
            };
            var order5 = new Orders
            {
                orderID = 5,
                firstName = "Barry",
                totalPrice = 3.20,
                items = "Beef jerky",
                status = "Pending"
            };
            orderList.Add(order1);
            orderList.Add(order2);  
            orderList.Add(order3);
            orderList.Add(order4);
            orderList.Add(order5);
            string jstring = JsonConvert.SerializeObject(orderList);
            HttpContext.Session.SetString("Orders", jstring);


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



        }
}
