using Microsoft.AspNetCore.Mvc;

namespace WebBuilding_50080.Controllers
{
    public class PetrolStationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

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

    }
    public class FuelPriceModel
    {
        public string FuelType { get; set; }
        public double CurrentPrice { get; set; }
        public string UpdatedPrice { get; set; }
    }
}
