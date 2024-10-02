namespace WebBuilding_50080.Models
{
    public class CartViewModel
    {
        // Existing properties
        public List<Product> Products { get; set; } = new List<Product>();

        public decimal TotalPrice => Products.Sum(p => p.Price);

        // New properties for payment details
        public string? FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        // Payment details
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpMonth { get; set; }
        public string ExpYear { get; set; }
        public string CVV { get; set; }
    }
}
