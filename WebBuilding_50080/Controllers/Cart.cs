using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebBuilding_50080.Models;

namespace WebBuilding_50080.Controllers
{
    public class Cart : Controller
    {
        private static List<int> CartItems = new List<int>();

        public IActionResult AddToCart(int name)

        {
            CartItems.Add(name);
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            return View(CartItems);
        }
    }
}
