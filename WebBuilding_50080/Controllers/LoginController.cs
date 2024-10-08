﻿using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebBuilding_50080.Models;
using Newtonsoft.Json;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
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
        public IActionResult Index()
        {
            //if the user is logged in, pass it to the View
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
            //login SQL command
            SqlCommand cmdQ = new SqlCommand("SELECT userID, firstName, lastName, email, pass,NULL AS cardName,CAST(NULL AS INT) " +
                "AS cardNum, CAST(NULL AS DATE) AS cardDate, NULL AS points, 'Manager' AS userType FROM Manager " +
                "WHERE email = @Email AND pass = @Password " +
                "UNION ALL SELECT cusID, firstName, lastName, email, pass, cardName, cardNum, cardDate, points, 'Customer' AS userType FROM Customer " +
                "WHERE email = @Email AND pass = @Password " +
                "UNION ALL SELECT staffID, firstName, lastName, email, pass,NULL AS cardName,CAST(NULL AS INT) AS cardNum, CAST(NULL AS DATE) AS cardDate, NULL AS points," +
                "'Staff' AS userType FROM Staff WHERE email = @Email AND pass = @Password", _db);

            cmdQ.Parameters.AddWithValue("@Email", email);
            cmdQ.Parameters.AddWithValue("@Password", pass);

            SqlDataReader reader = cmdQ.ExecuteReader();

            // Redirect to a success page or handle successful login
            if (reader.Read())
            {
                string userType = reader["userType"].ToString();
                User user = null;
                //Either login as Manager, Staff, or Customer
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
                    if (userType == "Manager") {
                        user.loginStatus = 2;
                            }
                    else
                    {
                        user.loginStatus = 3;
                    }

                   }
                

                else if (userType == "Customer")
                {

                    HttpContext.Session.SetInt32("LoginStatus", 1);
                    user = new Customer
                    {
                        userID = Convert.ToInt32(reader["userID"]),
                        firstName = reader["firstName"].ToString(),
                        lastName = reader["lastName"].ToString(),
                        email = reader["email"].ToString(),
                        pass = reader["pass"].ToString(),
                        points = reader["points"] == DBNull.Value ? default : Convert.ToInt32(reader["points"]),


                        cardDate = reader["cardDate"] == DBNull.Value ? null : DateOnly.FromDateTime(Convert.ToDateTime(reader["cardDate"])),
                        cardName = reader["cardName"] == DBNull.Value ? null : reader["cardName"].ToString(),
                        cardNum = reader["cardNum"] == DBNull.Value ? null : Convert.ToInt32(reader["cardNum"]),


                        loginStatus = 1
                    };


                    HttpContext.Session.SetInt32("cusID", user.userID);  
                }


                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
       
                return RedirectToAction("Index", "Home");
                
            }
            else
            {
                // Handle login failure (e.g., display an error message)
                ViewData["LoginError"] = "Invalid email or password.";
                return View("Index");
            }

        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("User");
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Delete()
        {
            var userJson = HttpContext.Session.GetString("User");
            
            var user = JsonConvert.DeserializeObject<User>(userJson);
            _db.Open();

            // Check if the email already exists
            SqlCommand cmdQ = new SqlCommand("DELETE FROM Manager WHERE userID = @userID", _db);
            cmdQ.Parameters.AddWithValue("@userID", user.userID);
            int rowsAffected = cmdQ.ExecuteNonQuery();
            HttpContext.Session.Remove("User");

            _db.Close();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(string firstName, string lastName, string email, string pass)
        {


            _db.Open();

            // Check if the email already exists
            SqlCommand checkEmailCmd = new SqlCommand("SELECT COUNT(*) FROM Customer WHERE email = @Email", _db);
            checkEmailCmd.Parameters.AddWithValue("@Email", email);

            int emailExists = (int)checkEmailCmd.ExecuteScalar(); // Returns the number of rows with that email

            if (emailExists > 0)
            {
                // Email already exists, show an error message
                ViewData["SignUpError"] = "Email already exists. Please use a different email.";
                _db.Close();
                return View("SignUp"); // Return to the signup page
            }
            else
            {
                //Create new Customer into the database
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
}
            
        
    

