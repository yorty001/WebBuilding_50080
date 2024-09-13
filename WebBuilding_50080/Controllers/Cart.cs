using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebBuilding_50080.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index(string productName, string currentCart)
        {
            var cart = new List<string>();

            if (!string.IsNullOrEmpty(currentCart))
            {
                cart = new List<string>(currentCart.Split(','));
            }

            if (!string.IsNullOrEmpty(productName))
            {
                cart.Add(productName);
            }

            var updatedCart = string.Join(",", cart);

            ViewBag.CartItems = cart;
            ViewBag.UpdatedCart = updatedCart;  

            return View();
        }
    }
}
