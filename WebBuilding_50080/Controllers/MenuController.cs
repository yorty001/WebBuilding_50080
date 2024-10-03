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
            SqlCommand cmdQ = new SqlCommand("SELECT * FROM ConvenienceStoreItems", _db);
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
            cmd.ExecuteNonQuery();
            _db.Close();
        
      
        return RedirectToAction("Index"); // Redirect back to the menu after updating availability
    }

    // Method to display availability page
    public IActionResult FoodAvailibility()
        {
            return View();
        }
}
