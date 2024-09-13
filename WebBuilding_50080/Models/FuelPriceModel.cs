
using WebBuilding_50080.Models;

namespace WebBuilding_50080.Models
{
    public class FuelPriceModel
    {
        public string FuelType { get; set; } = string.Empty;
        public double CurrentPrice { get; set; }
        public double? UpdatedPrice { get; set; }
        public double totalPrice { get; set; }
    }

    public class FuelViewModel
    {
        public List<Fuel> FuelList { get; set; }

        public List<FuelPriceModel> FuelPrices { get; set; } // Adjust the type if needed
    }
}
