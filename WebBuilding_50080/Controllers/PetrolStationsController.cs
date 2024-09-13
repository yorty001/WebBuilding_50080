using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebBuilding_50080.Models;

namespace WebBuilding_50080.Controllers
{
    public class PetrolStationsController : Controller
    {


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


        public IActionResult Index()
        {
            return View(fuelPrices);
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