using Microsoft.AspNetCore.Mvc;
using WebBuilding_50080.Models;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.PointsToAnalysis;


namespace server.Controllers
{

    namespace WebBuilding_50080.Controllers
    {
        public class Payment : Controller
        {
            private readonly SqlConnection _db;

            public Payment(SqlConnection db)
            {
                _db = db;
            }
            public IActionResult Index([FromQuery] string name, [FromQuery] decimal price)
            {
                ViewBag.Name = name;
                ViewBag.Price = price;
                return View();
            }
            public IActionResult OrderSummary(decimal totalPrice)
            {
                var userJson = HttpContext.Session.GetString("User");
       
                    var user = JsonConvert.DeserializeObject<User>(userJson);

                int points = (int)Math.Floor(totalPrice);
                _db.Open();
                SqlCommand cmdQ = new SqlCommand("UPDATE Customer SET points = COALESCE(points, 0) + @points WHERE cusID = @cusID", _db);

                cmdQ.Parameters.AddWithValue("@points", points);
                cmdQ.Parameters.AddWithValue("@cusID", user.userID);

                SqlDataReader reader = cmdQ.ExecuteReader();

                user.points += points;

                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
                var cartJson = HttpContext.Session.GetString("cartProduct");
                if (cartJson != null)
                {
                    var cart = JsonConvert.DeserializeObject<CartViewModel>(cartJson);

                    // Pass the model to the OrderSummary view
                    return View(cart);
                }
                return View();
            }

            public IActionResult PaymentDetails()
            {
                ViewBag.CardName = HttpContext.Session.GetString("CardName");
                ViewBag.CardNumber = HttpContext.Session.GetString("CardNumber");
                ViewBag.ExpMonth = HttpContext.Session.GetString("ExpMonth");
                ViewBag.ExpYear = HttpContext.Session.GetString("ExpYear");
                ViewBag.CVV = HttpContext.Session.GetString("CVV");

                return View();
            }
            public IActionResult UpdatePaymentDetails()
            {
                return View();
            }

            [HttpPost]
            public IActionResult PaymentDetails(string CardName, string CardNumber, string ExpMonth, string ExpYear, string CVV)
            {

                HttpContext.Session.SetString("CardName", CardName);
                HttpContext.Session.SetString("CardNumber", CardNumber);
                HttpContext.Session.SetString("ExpMonth", ExpMonth);
                HttpContext.Session.SetString("ExpYear", ExpYear);
                HttpContext.Session.SetString("CVV", CVV);

                return RedirectToAction("PaymentDetails");
            }
            public IActionResult FuelPayment()
            {
                return View();

            }
            [HttpPost]
            public IActionResult FuelPayment(string fuelType, string price, string total)
            {
                var model = new FuelPriceModel
                {
                    FuelType = fuelType,
                    CurrentPrice = double.Parse(price),
                    totalPrice = double.Parse(total)
                };
                return View(model);
            }

            public IActionResult SuccessfulPage(decimal totalPrice)
            {
                var userJson = HttpContext.Session.GetString("User");

                var user = JsonConvert.DeserializeObject<User>(userJson);

                int points = (int)Math.Floor(totalPrice);

                _db.Open();
                SqlCommand cmdQ = new SqlCommand("UPDATE Customer SET points = COALESCE(points, 0) + @points WHERE cusID = @cusID", _db);

                cmdQ.Parameters.AddWithValue("@points", points);
                cmdQ.Parameters.AddWithValue("@cusID", user.userID);

                SqlDataReader reader = cmdQ.ExecuteReader();

                user.points += points;

                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
                return View(user);
            }


        }
    }
}



