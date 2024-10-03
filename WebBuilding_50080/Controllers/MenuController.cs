using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using WebBuilding_50080.Models;
using System.Collections.Generic;

public class MenuController : Controller
{
    string connectionString = "Data Source=LAPTOP-DCARNM2N\\SQLEXPRESS;Initial Catalog=UTRDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    // Fetch products for the Food Availability view
    public IActionResult FoodAvailibility()
    {
        var productList = new List<Product>();

        try
        {
            SqlConnection db = new SqlConnection(connectionString);

            db.Open();
            SqlCommand cmdQ = new SqlCommand("SELECT * FROM ConvenienceStoreItems", db);
            SqlDataReader reader = cmdQ.ExecuteReader();

            while (reader.Read())
            {
                var product = new Product
                {
                    Name = reader["itemName"].ToString(),
                    Description = reader["itemDescription"].ToString(),
                    Image = reader["itemImage"].ToString(),
                    Price = reader["itemCost"] != DBNull.Value ? Convert.ToDecimal(reader["itemCost"]) : 0M,
                    IsAvailable = reader["IsAvailable"] != DBNull.Value && Convert.ToBoolean(reader["IsAvailable"])
                };
                productList.Add(product);
            }
            db.Close();
        }
        catch (Exception e)
        {
            Debug.WriteLine("Error fetching products: " + e.Message);
        }

        return View(productList); // Pass the products to the view
    }

    // Change product availability
    [HttpPost]
    public IActionResult ChangeAvailability(string productName)
    {
        try
        {
            SqlConnection db = new SqlConnection(connectionString);
            db.Open();

            // Query to toggle availability
            SqlCommand cmd = new SqlCommand("UPDATE ConvenienceStoreItems SET IsAvailable = ~IsAvailable WHERE itemName = @productName", db);
            cmd.Parameters.AddWithValue("@productName", productName);

            cmd.ExecuteNonQuery();
            db.Close();
        }
        catch (Exception e)
        {
            Debug.WriteLine("Error updating availability: " + e.Message);
        }

        // Redirect back to the food availability page
        return RedirectToAction("FoodAvailibility");
    }

    public IActionResult Index()
    {
        var productList = new List<Product>();

        try
        {
            SqlConnection db = new SqlConnection(connectionString);

            db.Open();
            SqlCommand cmdQ = new SqlCommand("SELECT * FROM ConvenienceStoreItems", db);
            SqlDataReader reader = cmdQ.ExecuteReader();

            while (reader.Read())
            {
                var product = new Product
                {
                    Name = reader["itemName"].ToString(),
                    Description = reader["itemDescription"].ToString(),
                    Image = reader["itemImage"].ToString(),
                    Price = reader["itemCost"] != DBNull.Value ? Convert.ToDecimal(reader["itemCost"]) : 0M
                };
                productList.Add(product);
            }
            db.Close();
        }
        catch (Exception e)
        {
            Debug.WriteLine("Error fetching products: " + e.Message);
        }

        return View(productList);
    }
}
