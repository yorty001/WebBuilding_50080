using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebBuilding_50080.Models;

namespace WebBuilding_50080.Controllers
{
    public class PetrolStationsController : Controller
    {
        private readonly SqlConnection _db;
        public PetrolStationsController(SqlConnection db)
        {
            _db = db;
        }
        private static List<FuelPriceModel> fuelPrices = new List<FuelPriceModel>
        {
            new FuelPriceModel { FuelType = "Diesel", CurrentPrice = 1.50 },
            new FuelPriceModel { FuelType = "Unleaded", CurrentPrice = 1.35 },
            new FuelPriceModel { FuelType = "Premium", CurrentPrice = 1.70 }
        };

        
        [HttpGet]
        public IActionResult UpdateFuelPrice()
        {
            _db.Open();
            SqlCommand cmdQ = new SqlCommand("SELECT fuelType, pricePL FROM Fuel", _db);



            SqlDataReader reader = cmdQ.ExecuteReader();
            List<Fuel> fuelList = new List<Fuel>();

            while (reader.Read())
            {
                Fuel fuel = new Fuel
                {
                    fuelType = reader["fuelType"].ToString(),
                    pricePL = reader["pricePL"].ToString()
                };

                fuelList.Add(fuel);
            }

            _db.Close();
            return View(fuelList);

        }

        
        [HttpPost]
        public IActionResult UpdateFuelPrice(List<FuelPriceModel> updatedFuelPrices)
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

        
        public IActionResult Index()
        {
            return View(fuelPrices); 
        }

        public IActionResult GPS()
        {
            return View();
        }


    }

    
    public class FuelPriceModel
    {
        public string FuelType { get; set; } = string.Empty;
        public double CurrentPrice { get; set; }
        public double? UpdatedPrice { get; set; }
    }
}
