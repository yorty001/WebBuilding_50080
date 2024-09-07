using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace WebBuilding_50080.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string pass)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\costa\\OneDrive\\Documents\\01 - University\\WebBuilding_50080\\WebBuilding_50080\\App_Data\\UTRDB.mdf;Integrated Security=True;Connect Timeout=30";

            SqlConnection db = new SqlConnection(connectionString);

            db.Open();
            SqlCommand cmdQ = new SqlCommand("SELECT email, pass, 'Manager' AS userType FROM Manager WHERE email = @Email AND pass = @Password UNION SELECT email, pass, 'Customer' AS userType FROM Customer WHERE email = @Email AND pass = @Password", db);

            cmdQ.Parameters.AddWithValue("@Email", email);
            cmdQ.Parameters.AddWithValue("@Password", pass);

            SqlDataReader reader = cmdQ.ExecuteReader();
            if (reader.Read())
            {
                // Redirect to a success page or handle successful login
                string userType = reader["userType"].ToString();
                if (userType == "Manager")
                {
                    ViewBag.loginStatus = 2;
                }
                else if (userType == "Customer")
                {
                    ViewBag.loginStatus = 1;
                }
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
            
        
    

