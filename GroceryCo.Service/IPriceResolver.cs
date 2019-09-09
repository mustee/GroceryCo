using GroceryCo.Data.Models;

namespace GroceryCo.Service
{
    public interface IPriceResolver
    {
        decimal Resolve(string productName, decimal productPrice, int quantity, PricingStrategy pricingStrategy);
    }
}
