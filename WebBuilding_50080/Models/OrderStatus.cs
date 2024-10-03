using System.Collections.Generic;

namespace WebBuilding_50080.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<Product> Products { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsReady { get; set; }
    }
}
