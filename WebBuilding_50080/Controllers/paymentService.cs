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

        public string CheckoutSession(string priceID)
        {
            var domain = "http://localhost:4242";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                       // Price = priceID, 
                       // Quantity = 1,    
                    },
                },
                Mode = "payment",    
                SuccessUrl = domain + "/accepted.html",   
                CancelUrl = domain + "/declined.html",    
            };

            var service = new SessionService();
            Session session = service.Create(options);

         
            return session.Url;
        }
    }
}
