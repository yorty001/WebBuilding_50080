
namespace WebBuilding_50080.Models
{
    public class FuelPriceModel
    {
        public string FuelType { get; set; } = string.Empty;
        public double CurrentPrice { get; set; }
        public double? UpdatedPrice { get; set; }
        public double totalPrice { get; set; }
    }
}
