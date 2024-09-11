using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using WebBuilding_50080.Models;

    public class MenuController : Controller
    {
        string connectionString = "Data Source=FRANK\\SQLEXPRESS;Initial Catalog=UTRDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

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

