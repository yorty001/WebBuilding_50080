using Microsoft.AspNetCore.Mvc;
using WebBuilding_50080.Models;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;


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

            public IActionResult SuccessfulPage()
            {
                return View();
            }
            [HttpPost]
            public IActionResult OrderSummary(double price)
            {
                int points = (int)Math.Floor(price);

                var userJson = HttpContext.Session.GetString("User");
                if (userJson != null)
                {
                    var user = JsonConvert.DeserializeObject<User>(userJson);

                    _db.Open();
                    SqlCommand cmdQ = new SqlCommand("UPDATE Customer SET points = @Points WHERE cusID = @CusID", _db);
                    cmdQ.Parameters.AddWithValue("@Points", points);
                    cmdQ.Parameters.AddWithValue("@CusID", user.userID);
                    try
                    {
                        // Execute the command
                        cmdQ.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Handle the error (optional)
                        // Log the exception or show an error message
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        // Close the database connection
                        _db.Close();
                    }
                }
                return View();
            
            }

        }

    }
}


