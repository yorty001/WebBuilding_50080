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

            using (SqlConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                SqlCommand cmdQ = new SqlCommand(@"
                    SELECT email, pass FROM Manager WHERE email = @Email AND pass = @Password", db);

                cmdQ.Parameters.AddWithValue("@Email", email);
                cmdQ.Parameters.AddWithValue("@Password", pass);

                SqlDataReader reader = cmdQ.ExecuteReader();
                if (reader.HasRows)
                {
                    // Redirect to a success page or handle successful login
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    // Handle login failure (e.g., display an error message)
                    ViewData["LoginError"] = "Invalid email or password.";
                    return View("Index");
                }
                
            }
        }

        public IActionResult Success()
        {
            // A page to show after a successful login
            return View();
        }
    }
}
            
        
    

