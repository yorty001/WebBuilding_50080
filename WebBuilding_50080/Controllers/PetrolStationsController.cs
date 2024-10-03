using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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


        private static List<Models.FuelPriceModel> fuelPrices = new List<Models.FuelPriceModel>
        {
            new Models.FuelPriceModel { FuelType = "Diesel", CurrentPrice = 1.50 },
            new Models.FuelPriceModel { FuelType = "Unleaded", CurrentPrice = 1.35 },
            new Models.FuelPriceModel { FuelType = "Premium", CurrentPrice = 1.70 }
        };


        [HttpGet]
        public IActionResult UpdateFuelPrice()
        {
            return View(fuelPrices);
        }


        [HttpPost]
        public IActionResult UpdateFuelPrice(List<Models.FuelPriceModel> updatedFuelPrices)
        {

            for (int i = 0; i < updatedFuelPrices.Count; i++)
            {
                if (updatedFuelPrices[i].UpdatedPrice.HasValue)
                {
                    fuelPrices[i].CurrentPrice = updatedFuelPrices[i].UpdatedPrice.Value;

                }
            }


            return RedirectToAction("Index");
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
                    cmdQ.Parameters.Add("@pricePL", SqlDbType.Float).Value = fuelPrice.Value;

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
            FuelViewModel viewModel = new FuelViewModel
            {
                FuelList = fuelList,
                FuelPrices = fuelPrices // Assuming fuelPrices is already defined in your method
            };

            return View(fuelList);
        }


        public IActionResult GPS()
        {
            return View();
        }

        public IActionResult PrePayFuel()
        {
            return View(fuelPrices);
        }


    }
}