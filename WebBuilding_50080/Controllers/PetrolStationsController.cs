using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebBuilding_50080.Controllers
{
    public class PetrolStationsController : Controller
    {

        [HttpGet]
        public IActionResult UpdateFuelPrice()
        {
            var fuelPrices = new List<FuelPriceModel>
            {
                new FuelPriceModel { FuelType = "Diesel", CurrentPrice = 1.50 },
                new FuelPriceModel { FuelType = "Unleaded", CurrentPrice = 1.35 },
                new FuelPriceModel { FuelType = "Premium", CurrentPrice = 1.70 }
            };

            return View(fuelPrices);

        }

        [HttpPost]
        public IActionResult UpdateFuelPrice(List<FuelPriceModel> updatedFuelPrices)
        {
            foreach (var fuelPrice in updatedFuelPrices)
            {
                Console.WriteLine($"Updated price for {fuelPrice.FuelType}: {fuelPrice.UpdatedPrice}");
            }
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        { 
            return View();
        }
    }
    public class FuelPriceModel
    {
        public string FuelType { get; set; } = string.Empty;
        public double CurrentPrice { get; set; }
        public double UpdatedPrice { get; set; }
    }
}
