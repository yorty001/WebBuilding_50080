using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebBuilding_50080.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebBuilding_50080.Controllers
{
    public class HomeController : Controller
    {
        public readonly SqlConnection _db;
        private readonly ILogger<HomeController> _logger;


        public HomeController(SqlConnection db, ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
 

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

        public IActionResult feedback()
        {
      
            int? cusID = HttpContext.Session.GetInt32("cusID");
            if (cusID == null)
            {
              
                return RedirectToAction("Index", "Login");
            }

          
            ViewBag.cusID = cusID;
            return View();
        }

        public IActionResult feedbackreview()
        {
            var feedbackList = new List<Feedback>();

            try
            {
                _db.Open();
                string query = "SELECT * FROM Feedback";
                SqlCommand cmd = new SqlCommand(query, _db);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var feedback = new Feedback
                    {
                        Id = Convert.ToInt32(reader["feedbackID"]),
                        CusID = Convert.ToInt32(reader["cusID"]),
                        Type = reader["type"].ToString(),
                        Details = reader["details"].ToString(),
                        Rating = Convert.ToInt32(reader["rating"])
                    };

                    feedbackList.Add(feedback);
                }
                _db.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching feedbacks: " + ex.Message);
                _db.Close();
            }

            return View(feedbackList); // Pass the feedback list to the view
        }


        [HttpPost]
        public IActionResult SubmitFeedback(int cusID, string purpose, string text, int rating)
        {
            string insertStatement = "INSERT INTO Feedback (cusID, type, details, rating) VALUES (@cusID, @type, @details, @rating)";

            using (SqlCommand cmd = new SqlCommand(insertStatement, _db))
            {
                cmd.Parameters.AddWithValue("@cusID", cusID);
                cmd.Parameters.AddWithValue("@type", purpose);
                cmd.Parameters.AddWithValue("@details", text);
                cmd.Parameters.AddWithValue("@rating", rating);

                _db.Open();
                cmd.ExecuteNonQuery();
                _db.Close();
            }

            return RedirectToAction("feedbackSuccess");
        }

        public IActionResult feedbackSuccess()
        {
            return View();
        }
    }
}

