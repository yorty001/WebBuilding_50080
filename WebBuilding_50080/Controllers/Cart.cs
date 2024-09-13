using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebBuilding_50080.Models;

namespace WebBuilding_50080.Controllers
{
    public class CartController : Controller
    {
        private static List<Product> cartProducts = new List<Product>();

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

            return View(cartViewModel);
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
    }
}