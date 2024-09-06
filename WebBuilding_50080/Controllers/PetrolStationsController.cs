using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebBuilding_50080.Controllers
{
    public class PetrolStationsController : Controller
    {
        // This is a Static list to hold fuel prices (simulating a database)
        private static List<FuelPriceModel> fuelPrices = new List<FuelPriceModel>
        {
            new FuelPriceModel { FuelType = "Diesel", CurrentPrice = 1.50 },
            new FuelPriceModel { FuelType = "Unleaded", CurrentPrice = 1.35 },
            new FuelPriceModel { FuelType = "Premium", CurrentPrice = 1.70 }
        };

        // GET: Show current fuel prices for update
        [HttpGet]
        public IActionResult UpdateFuelPrice()
        {
            return View(fuelPrices);
        }

        // POST: This will Update the fuel prices
        [HttpPost]
        public IActionResult UpdateFuelPrice(List<FuelPriceModel> updatedFuelPrices)
        {
            // This will Update the current prices with the new prices
            for (int i = 0; i < updatedFuelPrices.Count; i++)
            {
                fuelPrices[i].CurrentPrice = updatedFuelPrices[i].UpdatedPrice;
            }

            // This will Redirect to the Index page showing the updated prices
            return RedirectToAction("Index");
        }

        // GET: This will Display the updated fuel prices on the Index page
        public IActionResult Index()
        {
            return View(fuelPrices); // This will Pass the updated prices to the view
        }
    }

    // This is a Simple model for the Fuel Prices
    public class FuelPriceModel
    {
        public string FuelType { get; set; }
        public double CurrentPrice { get; set; }
        public double UpdatedPrice { get; set; }
    }
}
