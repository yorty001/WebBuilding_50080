using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Diagnostics;
using WebBuilding_50080.Models;

    public class MenuController : Controller
    {
    private readonly SqlConnection _db;
    public MenuController(SqlConnection db)
    {
        _db = db;
    }

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
                        Price = reader["itemCost"] != DBNull.Value ? Convert.ToDecimal(reader["itemCost"]) : 0M

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
    }

