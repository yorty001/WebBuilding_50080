using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebBuilding_50080.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace WebBuilding_50080.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
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

        [HttpPost]
        public IActionResult Login(string email, string pass)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\costa\\OneDrive\\Documents\\01 - University\\WebBuilding_50080\\WebBuilding_50080\\App_Data\\UTRDB.mdf;Integrated Security=True;Connect Timeout=30";

            SqlConnection db = new SqlConnection(connectionString);

            db.Open();
            SqlCommand cmdQ = new SqlCommand("SELECT userID, firstName, lastName, email, pass,NULL AS cardName,CAST(NULL AS INT) " +
                "AS cardNum, CAST(NULL AS DATE) AS cardDate,'Manager' AS userType FROM Manager " +
                "WHERE email = @Email AND pass = @Password " +
                "UNION ALL SELECT cusID, firstName, lastName, email, pass, cardName, cardNum, cardDate, 'Customer' AS userType FROM Customer " +
                "WHERE email = @Email AND pass = @Password", db);

            cmdQ.Parameters.AddWithValue("@Email", email);
            cmdQ.Parameters.AddWithValue("@Password", pass);

            SqlDataReader reader = cmdQ.ExecuteReader();
            if (reader.Read())
            {
                // Redirect to a success page or handle successful login
                string userType = reader["userType"].ToString();
                User user = null;
                if (userType == "Manager")
                {
                    ViewBag.loginStatus = 2;
                    user = new Manager
                    {
                        userID = Convert.ToInt32(reader["userID"]),
                        firstName = reader["firstName"].ToString(),
                        lastName = reader["lastName"].ToString(),
                        email = reader["email"].ToString(),
                        pass = reader["pass"].ToString(),


                    };
                }
                else if (userType == "Customer")
                {
                    ViewBag.loginStatus = 1;
                    user = new Customer
                    {
                        userID = Convert.ToInt32(reader["userID"]),
                        firstName = reader["firstName"].ToString(),
                        lastName = reader["lastName"].ToString(),
                        email = reader["email"].ToString(),
                        pass = reader["pass"].ToString(),


                        cardDate = reader["cardDate"] == DBNull.Value ? default : DateOnly.FromDateTime(Convert.ToDateTime(reader["cardDate"])),
                        cardName = reader["cardName"] == DBNull.Value ? null : reader["cardName"].ToString(),
                        cardNum = reader["cardNum"] == DBNull.Value ? default : Convert.ToInt32(reader["cardNum"])
                     };


                
                }


                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
                var userJson = HttpContext.Session.GetString("User");
       
                return RedirectToAction("Index", "Home", new { ViewBag.loginStatus });
                
            }
            else
            {
                // Handle login failure (e.g., display an error message)
                ViewData["LoginError"] = "Invalid email or password.";
                return View("Index");
            }

        }


        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(string firstName, string lastName, string email, string pass)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\costa\\OneDrive\\Documents\\01 - University\\WebBuilding_50080\\WebBuilding_50080\\App_Data\\UTRDB.mdf;Integrated Security=True;Connect Timeout=30";

            SqlConnection db = new SqlConnection(connectionString);

            db.Open();
            SqlCommand cmdQ = new SqlCommand("INSERT INTO Customer (firstName, lastName, email, pass) VALUES (@firstName, @lastName, @email, @pass)", db);

            cmdQ.Parameters.AddWithValue("@firstName", firstName);
            cmdQ.Parameters.AddWithValue("@lastName", lastName);
            cmdQ.Parameters.AddWithValue("@email", email);
            cmdQ.Parameters.AddWithValue("@pass", pass);

            int rowsAffected = cmdQ.ExecuteNonQuery();
            Console.WriteLine(rowsAffected);
            return View("Index");
        }
    }
}
            
        
    

