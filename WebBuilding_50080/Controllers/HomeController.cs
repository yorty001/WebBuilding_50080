using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebBuilding_50080.Models;
using Newtonsoft.Json;

namespace WebBuilding_50080.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int loginStatus = 0)
        {
            if (loginStatus != 0)
            {
                ViewBag.loginStatus = loginStatus;
            }

            var userJson = HttpContext.Session.GetString("User");
            if (userJson != null)
            {
                var user = JsonConvert.DeserializeObject<User>(userJson);
                return View(user); // Pass the user model to the view
            }

            // If no user data in session, just return the view without user data
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
