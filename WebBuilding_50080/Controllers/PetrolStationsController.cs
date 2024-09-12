using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebBuilding_50080.Controllers
{
    public class PetrolStationsController : Controller
    {
        
        private static List<FuelPriceModel> fuelPrices = new List<FuelPriceModel>
        {
            new FuelPriceModel { FuelType = "Diesel", CurrentPrice = 1.50 },
            new FuelPriceModel { FuelType = "Unleaded", CurrentPrice = 1.35 },
            new FuelPriceModel { FuelType = "Premium", CurrentPrice = 1.70 }
        };

        
        [HttpGet]
        public IActionResult UpdateFuelPrice()
        {
            return View(fuelPrices);
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

        public IActionResult PurchaseFuel()
        {
            return View();
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
