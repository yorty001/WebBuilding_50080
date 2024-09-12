using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;

namespace WebBuilding_50080.Services
{
    public class PaymentService
    {
        public PaymentService()
        {
            StripeConfiguration.ApiKey = "sk_test_51PuVHZJPpXkyeWaBITMqQtb6z84fOaZvSQE50AEivL23rfCuXa2HBet2i2f6zK4mv4yKIKfimG0MFR5YF3J2JlrA00AJ3n1yPp";
        }
        public bool ProcessPayment(int customerID, decimal amount)
        {
            return true;
        }

        public String CheckoutSession(List<Cartitem> CartItems)
        {
            if (CartItems == null || CartItems.Count == 0)
            {
                throw new ArgumentException("Cart cannot be empty.");
            }
            var domain = "http://localhost:4242";


            var LineItems = new List<SessionLineItemOptions>();
            foreach (var item in CartItems) {
                LineItems.Add(new SessionLineItemOptions
                    {
                       Price = item.amount,
                       Quantity = item.quantity,    
                    });
                }
                var opt = new SessionCreateOptions
                {
                    LineItems = LineItems,
                Mode = "payment",    
                SuccessUrl = domain + "/accepted.html",   
                CancelUrl = domain + "/declined.html",    
                };

            var service = new SessionService();
            Session session = service.Create(opt);

         
            return session.Url;
        }
    }
    public class Cartitem
    {
        public string amount { get; set; }
        public int quantity { get; set; }
    }
}
