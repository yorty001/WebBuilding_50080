namespace WebBuilding_50080.Models
{
    public class CartViewModel
    {
        public List<Product> Products { get; set; } = new List<Product>();

        public decimal TotalPrice => Products.Sum(p => p.Price);
    }
}
