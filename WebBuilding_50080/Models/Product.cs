namespace WebBuilding_50080.Models
{
    public class Product
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
            public decimal Price { get; set; }

        }
    public class ProductsViewModel
    {
        public List<Product> Products { get; set; }
        public User User { get; set; }
    }
}

