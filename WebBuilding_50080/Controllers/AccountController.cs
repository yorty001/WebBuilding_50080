using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebBuilding_50080.Models;

namespace WebBuilding_50080.Controllers
{
    public class AccountController : Controller
    {
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
                return View();
            }
        }
    }

