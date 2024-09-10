using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
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
        public IActionResult Edit(string firstName, string lastName, string email, string pass, string cardName, int cardNum, DateOnly cardDate)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\costa\\OneDrive\\Documents\\01 - University\\WebBuilding_50080\\WebBuilding_50080\\App_Data\\UTRDB.mdf;Integrated Security=True;Connect Timeout=30";

            SqlConnection db = new SqlConnection(connectionString);

            db.Open();

            DateTime cardDateTime = cardDate.ToDateTime(new TimeOnly(0, 0));


            var userJson = HttpContext.Session.GetString("User");

            var user = JsonConvert.DeserializeObject<User>(userJson);

            SqlCommand cmdQ;

            if (ViewBag.loginStatus == 2)
            {
                cmdQ = new SqlCommand("UPDATE Manager SET firstName = '@firstName', lastName = '@lastName'," +
                "email = '@email', pass = '@pass'WHERE userID = @id", db);
            }
            else
            {
                cmdQ = new SqlCommand("UPDATE Customer SET firstName = '@firstName', lastName = '@lastName'," +
                "email = '@email', pass = '@pass', cardName = '@cardName', cardNum = @cardNum, cardDate = '@cardDate' WHERE cusID = @id", db);
                cmdQ.Parameters.AddWithValue("@cardName", cardName ?? (object)DBNull.Value);
                cmdQ.Parameters.AddWithValue("@cardNum", cardNum);
                cmdQ.Parameters.AddWithValue("@cardDate", cardDateTime.Month + "/"+ cardDateTime.Day +"/" + cardDateTime.Year);
            }
            Console.WriteLine(cardDateTime.Month + "/" + cardDateTime.Year);
            cmdQ.Parameters.AddWithValue("@id", user.userID);
            cmdQ.Parameters.AddWithValue("@firstName", firstName);
            cmdQ.Parameters.AddWithValue("@lastName", lastName);
            cmdQ.Parameters.AddWithValue("@email", email);
            cmdQ.Parameters.AddWithValue("@pass", pass);

            
            int rowsAffected = cmdQ.ExecuteNonQuery();

            db.Close();
            return RedirectToAction("Index", "Home", new { ViewBag.loginStatus });


        }

    }
}

