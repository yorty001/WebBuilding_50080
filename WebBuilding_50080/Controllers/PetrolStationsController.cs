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


        
        public IActionResult Index()
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
