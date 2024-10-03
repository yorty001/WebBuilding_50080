using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Diagnostics;
using WebBuilding_50080.Models;
using System.Collections.Generic;

public class MenuController : Controller
{
    private readonly SqlConnection _db;

    public MenuController(SqlConnection db)
    {
        _db = db;
    }

    // Method to display the products in the menu
    public IActionResult Index()
    {
        var productList = new List<Product>();

        try
        {
            _db.Open();
            SqlCommand cmdQ = new SqlCommand("SELECT ItemId, itemName, itemDescription, itemImage, itemCost, IsAvailable FROM ConvenienceStoreItems", _db);
            SqlDataReader reader = cmdQ.ExecuteReader();

            while (reader.Read())
            {
                var product = new Product
                {
                    Id = Convert.ToInt32(reader["ItemId"]),
                    Name = reader["itemName"].ToString(),
                    Description = reader["itemDescription"].ToString(),
                    Image = reader["itemImage"].ToString(),
                    Price = reader["itemCost"] != DBNull.Value ? Convert.ToDecimal(reader["itemCost"]) : 0M,
                    IsAvailable = reader["IsAvailable"] != DBNull.Value && Convert.ToBoolean(reader["IsAvailable"])
                };

                productList.Add(product);
            }
            _db.Close();
        }
        catch (Exception e)
        {
            Debug.WriteLine("Error fetching products: " + e.Message);
        }

        var userJson = HttpContext.Session.GetString("User");
        User user = null;
        if (userJson != null)
        {
            user = JsonConvert.DeserializeObject<User>(userJson);
        }

        var viewModel = new ProductsViewModel
        {
            Products = productList,
            User = user
        };

        return View(viewModel);
    }

    // Method to handle changing product availability
    [HttpPost]
    public IActionResult ChangeAvailability(int productId)
    {
        _db.Open();
        SqlCommand cmd = new SqlCommand("UPDATE ConvenienceStoreItems SET IsAvailable = ~IsAvailable WHERE ItemId = @ItemId", _db);
        cmd.Parameters.AddWithValue("@ItemId", productId);

        int rowsAffected = cmd.ExecuteNonQuery(); // You can log rowsAffected to ensure the update happens
        Console.WriteLine($"Rows affected: {rowsAffected}");

        _db.Close();
        return RedirectToAction("FoodAvailibility");
    }



    // Method to display availability page
    public IActionResult FoodAvailibility()
        {
        var products = new List<Product>
    {
        new Product { Name = "Prime Hydration Blue Raspberry", Description = "PRIME was developed to fill the void...", Image = "https://assets.woolworths.com.au/images/1005/275804.jpg?impolicy=wowsmkqiema&w=600&h=600", Price = 4.00M, IsAvailable = true },
        new Product { Name = "Pura Full Cream Milk 2l", Description = "PURA Full Cream milk is Australian...", Image = "https://assets.woolworths.com.au/images/1005/62636.jpg?impolicy=wowsmkqiema&w=600&h=600", Price = 5.50M, IsAvailable = true },
        // Add more products as needed...
    };

        var viewModel = new ProductsViewModel
        {
            Products = products
        };

        return View(viewModel);
        }
}
