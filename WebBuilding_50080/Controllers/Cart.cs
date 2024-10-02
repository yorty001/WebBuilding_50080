using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using WebBuilding_50080.Models;
using Newtonsoft.Json;

namespace WebBuilding_50080.Controllers
{
    public class CartController : Controller
    {
        private static List<Product> cartProducts = new List<Product>();
        public static List<Order> Orders = new List<Order>();

        public IActionResult Index(string productName, decimal productPrice)
        {
            if (!string.IsNullOrEmpty(productName))
            {
                var product = new Product
                {
                    Name = productName,
                    Price = productPrice 
                };

                cartProducts.Add(product);
            }

            var cartViewModel = new CartViewModel
            {
                Products = cartProducts
            };
            HttpContext.Session.SetString("cartProduct", JsonConvert.SerializeObject(cartViewModel));

            var cartJson = HttpContext.Session.GetString("cartProduct");
            var cart = JsonConvert.DeserializeObject<CartViewModel>(cartJson);


            return View(cart);
        }

        [HttpPost]
        public IActionResult OrderSummary(CartViewModel model)

        {
            if (ModelState.IsValid)
            {

                // Pass the model to the OrderSummary view
                return View(model);
            }

            // If model is null or invalid, handle it (e.g., redirect back or show an error)
            return RedirectToAction("Index");
        }




        public IActionResult Remove(string productName)
        {
            var productToRemove = cartProducts.Find(p => p.Name == productName);
            if (productToRemove != null)
            {
                cartProducts.Remove(productToRemove);
            }

            return RedirectToAction("Index");
        }

        public IActionResult PlaceOrder()
        {
            var order = new Order
            {
                OrderId = Orders.Count + 1,
                Products = new List<Product>(cartProducts),
                TotalPrice = CalculateTotalPrice(cartProducts),
                IsReady = false
            };

            Orders.Add(order);
            cartProducts.Clear();

            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
        }

        public IActionResult OrderConfirmation(int orderId)
        {
            var order = Orders.Find(o => o.OrderId == orderId);

            return View(order);
        }

        private decimal CalculateTotalPrice(List<Product> products)
        {
            decimal totalPrice = 0;
            foreach (var product in products)
            {
                totalPrice += product.Price;
            }
            return totalPrice;
        }
    }
}


