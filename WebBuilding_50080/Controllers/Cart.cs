using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebBuilding_50080.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace WebBuilding_50080.Controllers
{
    public class CartController : Controller
    {
        private static List<Product> cartProducts = new List<Product>();
        public static List<Order> Orders = new List<Order>();
        private readonly SqlConnection _db;

        public CartController(SqlConnection db)
        {
            _db = db;
        }

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

            return RedirectToAction("Payment", new { orderId = order.OrderId });
        }

        public IActionResult Payment(int orderId)
        {
            var order = Orders.Find(o => o.OrderId == orderId);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        public IActionResult ConfirmPayment(int orderId, string NameOnCard, string CardNumber, string ExpiryDate, string CVV)
        {
            var order = Orders.Find(o => o.OrderId == orderId);
            if (order == null)
            {
                return NotFound();
            }

            bool isPaymentValid = ValidatePayment(CardNumber, ExpiryDate, CVV);
            if (!isPaymentValid)
            {
                ViewBag.ErrorMessage = "Payment failed. Please check your details.";
                return View("Payment", order);
            }

            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
        }

        public IActionResult OrderConfirmation(int orderId)
        {
           
            var order = Orders.Find(o => o.OrderId == orderId);
            if (order == null)
            {
                return NotFound();
            }

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

        private bool ValidatePayment(string cardNumber, string expiryDate, string cvv)
        {
            return !string.IsNullOrEmpty(cardNumber) && !string.IsNullOrEmpty(expiryDate) && !string.IsNullOrEmpty(cvv);
        }

        [HttpPost]
        public IActionResult ProcessPayment(int orderID, decimal totalPrice)
        {
            var userJson = HttpContext.Session.GetString("User");

            var user = JsonConvert.DeserializeObject<User>(userJson);

            int points = (int)Math.Floor(totalPrice);

            _db.Open();
            SqlCommand cmdQ = new SqlCommand("UPDATE Customer SET points = COALESCE(points, 0) + @points WHERE cusID = @cusID", _db);

            cmdQ.Parameters.AddWithValue("@points", points);
            cmdQ.Parameters.AddWithValue("@cusID", user.userID);

            int rows = cmdQ.ExecuteNonQuery();

            user.points += points;

            HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
            _db.Close();
            return RedirectToAction("OrderConfirmation", new { orderId = orderID });
        }

    }
}
