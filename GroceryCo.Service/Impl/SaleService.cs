using System.Linq;
using System.Collections.Generic;
using GroceryCo.Data.Repositories;
using GroceryCo.Service.Exceptions;
using GroceryCo.Service.Models;
using System;

namespace GroceryCo.Service.Impl
{
    public class SaleService : ISaleService
    {
        private readonly IProductRepository productRepository;
        private readonly IPriceResolver priceResolver;

        public SaleService(IProductRepository productRepository, IPriceResolver priceResolver)
        {
            this.productRepository = productRepository;
            this.priceResolver = priceResolver;
        }


        public Sale Checkout(string[] productNames, PricingStrategy pricingStrategy = PricingStrategy.Lowest)
        {
            if(productNames == null || !productNames.Any()) throw new ArgumentException(nameof(productNames))
            
            var saleItems = new List<SaleItem>();
            foreach (var productName in productNames.GroupBy(x => x))
            {
                var product = productRepository.Find(productName.Key);
                if (product == null) throw new ProductNotFoundException(productName.Key);

                var price = priceResolver.Resolve(product.Name, product.Price, productName.Count(), pricingStrategy);
                saleItems.Add(new SaleItem(productName.Key, productName.Count(), price));
            }

            return new Sale(saleItems);
            
        }
    }
}
