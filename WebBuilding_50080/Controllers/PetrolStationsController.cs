using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using WebBuilding_50080.Models;

namespace WebBuilding_50080.Controllers
{
    public class PetrolStationsController : Controller
    {
        public readonly SqlConnection _db;
        public PetrolStationsController(SqlConnection db)
        {
            _db = db;
        }
// Pass the user model to the view
    

    [HttpGet]
        public IActionResult UpdateFuelPrice()
        {


           return View();

        }

        
        [HttpPost]
        public IActionResult Update(Dictionary<string, float?> NewPrices)
        {
            
            _db.Open();

            foreach (var fuelPrice in NewPrices)
            {
                if (fuelPrice.Value != null)
                {

                    SqlCommand cmdQ = new SqlCommand("Update Fuel SET pricePL = @pricePL WHERE fuelType = @fuelType", _db);

                    cmdQ.Parameters.AddWithValue("@fuelType", fuelPrice.Key);
                    cmdQ.Parameters.Add("@pricePL", SqlDbType.Float).Value =  fuelPrice.Value;

                    int rowsAffected = cmdQ.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected);
                }
            }
            _db.Close();
            return RedirectToAction("Index");
        }

        
        public IActionResult Index()
        {
            _db.Open();
            SqlCommand cmdQ = new SqlCommand("SELECT * FROM Fuel", _db);



            SqlDataReader reader = cmdQ.ExecuteReader();
            List<Fuel> fuelList = new List<Fuel>();

            while (reader.Read())
            {
                Fuel fuel = new Fuel
                {
                    id = Convert.ToInt32(reader["fuelID"]),
                    fuelType = reader["fuelType"].ToString(),
                    pricePL = Convert.ToDecimal(reader["pricePL"])
                };

                fuelList.Add(fuel);
            }
            _db.Close();
            return View(fuelList);
        }

        public IActionResult PurchaseFuel()
        {
            return View();
        }

        public IActionResult GPS()
        {
            return View();
        }


    }

    

}
