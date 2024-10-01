using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebBuilding_50080.Models;
using Newtonsoft.Json;
namespace WebBuilding_50080.Controllers
{
    public class LoginController : Controller
    {
        private readonly SqlConnection _db;
        public LoginController(SqlConnection db)
        {
            _db = db;
        }
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


            _db.Open();
            SqlCommand cmdQ = new SqlCommand("SELECT userID, firstName, lastName, email, pass,NULL AS cardName,CAST(NULL AS INT) " +
                "AS cardNum, CAST(NULL AS DATE) AS cardDate, NULL AS points, 'Manager' AS userType FROM Manager " +
                "WHERE email = @Email AND pass = @Password " +
                "UNION ALL SELECT cusID, firstName, lastName, email, pass, cardName, cardNum, cardDate, points, 'Customer' AS userType FROM Customer " +
                "WHERE email = @Email AND pass = @Password " +
                "UNION ALL SELECT staffID, firstName, lastName, email, pass,NULL AS cardName,CAST(NULL AS INT) AS cardNum, CAST(NULL AS DATE) AS cardDate, NULL AS points," +
                "'Staff' AS userType FROM Staff", _db);

            cmdQ.Parameters.AddWithValue("@Email", email);
            cmdQ.Parameters.AddWithValue("@Password", pass);

            SqlDataReader reader = cmdQ.ExecuteReader();
            if (reader.Read())
            {
                // Redirect to a success page or handle successful login
                string userType = reader["userType"].ToString();
                User user = null;
                if (userType == "Manager" || userType == "Staff")
                {
                    
                    user = new Manager
                    {
                        userID = Convert.ToInt32(reader["userID"]),
                        firstName = reader["firstName"].ToString(),
                        lastName = reader["lastName"].ToString(),
                        email = reader["email"].ToString(),
                        pass = reader["pass"].ToString(),


                    };
                    if (userType == "Manager")
                    {
                        ViewBag.loginStatus = 2;
                    }
                    else
                    {
                        ViewBag.loginStatus = 3;
                    }
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
                        points = reader["points"] == DBNull.Value ? default : Convert.ToInt32(reader["points"]),


                        cardDate = reader["cardDate"] == DBNull.Value ? default : DateOnly.FromDateTime(Convert.ToDateTime(reader["cardDate"])),
                        cardName = reader["cardName"] == DBNull.Value ? null : reader["cardName"].ToString(),
                        cardNum = reader["cardNum"] == DBNull.Value ? default : Convert.ToInt32(reader["cardNum"])
                     };


                
                }


                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
       
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


            _db.Open();
            SqlCommand cmdQ = new SqlCommand("INSERT INTO Customer (firstName, lastName, email, pass) VALUES (@firstName, @lastName, @email, @pass)", _db);

            cmdQ.Parameters.AddWithValue("@firstName", firstName);
            cmdQ.Parameters.AddWithValue("@lastName", lastName);
            cmdQ.Parameters.AddWithValue("@email", email);
            cmdQ.Parameters.AddWithValue("@pass", pass);

            int rowsAffected = cmdQ.ExecuteNonQuery();
            Console.WriteLine(rowsAffected);
            _db.Close();
            return View("Index");
        }
    }
}
            
        
    

