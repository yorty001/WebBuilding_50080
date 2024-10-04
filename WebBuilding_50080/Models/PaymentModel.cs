namespace WebBuilding_50080.Models
{
    public class PaymentModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        // Payment fields
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpMonth { get; set; }
        public string ExpYear { get; set; }
        public string CVV { get; set; }
    }
}
