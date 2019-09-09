using GroceryCo.Service.Models;

namespace GroceryCo.Service
{
    public interface ISaleService
    {
        Sale Checkout(string[] productNames, PricingStrategy pricingStrategy = PricingStrategy.Lowest);
    }
}
