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
        public IActionResult Edit(string firstName, string lastName, string email, string pass)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\costa\\OneDrive\\Documents\\01 - University\\WebBuilding_50080\\WebBuilding_50080\\App_Data\\UTRDB.mdf;Integrated Security=True;Connect Timeout=30";

            SqlConnection db = new SqlConnection(connectionString);

            db.Open();

            var userJson = HttpContext.Session.GetString("User");

            var user = JsonConvert.DeserializeObject<User>(userJson);

            SqlCommand cmdQ = new SqlCommand("UPDATE @table SET firstName = '@firstName', lastName = '@lastName'," +
                "email = '@email', pass = '@pass', cardName = '@cardName', cardNum = @cardNum, cardDate = '@cardDate' WHERE @idName = @id", db);
            string tableName;
            string idName;
            if (ViewBag.loginStatus == 1)
            {
                tableName = "Manager";
                idName = "userID";
            }
            else
            {
                tableName = "Customer";
                idName = "cusID";
            }
            cmdQ.Parameters.AddWithValue("@table", tableName);
            cmdQ.Parameters.AddWithValue("@idName", idName);
            cmdQ.Parameters.AddWithValue("@id", user.userID);
            cmdQ.Parameters.AddWithValue("@firstName", firstName);
            cmdQ.Parameters.AddWithValue("@lastName", lastName);
            cmdQ.Parameters.AddWithValue("@email", email);
            cmdQ.Parameters.AddWithValue("@pass", pass);


            db.Close();
            return RedirectToAction("Index", "Login", new { ViewBag.loginStatus });


        }

    }
}

