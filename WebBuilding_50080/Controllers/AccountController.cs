using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using WebBuilding_50080.Models;

namespace WebBuilding_50080.Controllers
{
    public class AccountController : Controller
    {
        private readonly SqlConnection _db;
        public AccountController(SqlConnection db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

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


            _db.Open();

            DateTime cardDateTime = cardDate.ToDateTime(new TimeOnly(0, 0));


            var userJson = HttpContext.Session.GetString("User");

            var user = JsonConvert.DeserializeObject<User>(userJson);

            SqlCommand cmdQ;

            if (user.loginStatus == 2)
            {

                cmdQ = new SqlCommand("UPDATE Manager SET firstName = @firstName, lastName = @lastName," +
                "email = @email, pass = @pass WHERE userID = @id", _db);
                

            }
            else if (user.loginStatus == 3)
            {
                cmdQ = new SqlCommand("UPDATE Staff SET firstName = @firstName, lastName = @lastName," +
                "email = @email, pass = @pass WHERE staffID = @id", _db);
            }
            else
            {
                cmdQ = new SqlCommand("UPDATE Customer SET firstName = @firstName, lastName = @lastName," +
                "email = @email, pass = @pass, cardName = @cardName, cardNum = @cardNum, cardDate = @cardDate WHERE cusID = @id", _db);
                cmdQ.Parameters.AddWithValue("@cardName", cardName ?? (object)DBNull.Value);
                cmdQ.Parameters.AddWithValue("@cardNum", cardNum);
                cmdQ.Parameters.AddWithValue("@cardDate", cardDateTime.Month + "/" + cardDateTime.Day + "/" + cardDateTime.Year);
                Customer user1 = user as Customer;
                if (user1 != null)
                {
                    user = new Customer
                    {
                        userID = user.userID,
                        firstName = user.firstName,
                        lastName = user.lastName,
                        email = user.email,
                        pass = user.pass,
                        points = user.points,


                        cardDate = user1.cardDate ?? default(DateOnly),
                        cardName = user1.cardName ?? "Unknown",
                        cardNum = user1.cardNum ?? default(int),


                        loginStatus = 1
                    };
                }
         
              

            }
            cmdQ.Parameters.AddWithValue("@id", user.userID);
            cmdQ.Parameters.AddWithValue("@firstName", firstName);
            cmdQ.Parameters.AddWithValue("@lastName", lastName);
            cmdQ.Parameters.AddWithValue("@email", email);
            cmdQ.Parameters.AddWithValue("@pass", pass);
            user.firstName = firstName;
            user.lastName = lastName;
            user.email = email;
            user.pass = pass;


            int rowsAffected = cmdQ.ExecuteNonQuery();

            _db.Close();
            HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
            return RedirectToAction("Index", "Home");


        }
        public IActionResult CreateStaff()
        {
            return View();  
        }
        public IActionResult AdminCreate(string firstName, string lastName, string email, string pass)
        {


            _db.Open();
            SqlCommand cmdQ = new SqlCommand("INSERT INTO Staff (firstName, lastName, email, pass) VALUES (@firstName, @lastName, @email, @pass)", _db);

            cmdQ.Parameters.AddWithValue("@firstName", firstName);
            cmdQ.Parameters.AddWithValue("@lastName", lastName);
            cmdQ.Parameters.AddWithValue("@email", email);
            cmdQ.Parameters.AddWithValue("@pass", pass);

            int rowsAffected = cmdQ.ExecuteNonQuery();
            Console.WriteLine(rowsAffected);
            _db.Close();
            return RedirectToAction("Index");

        }
    }
}


