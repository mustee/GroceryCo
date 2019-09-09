using GroceryCo.Data.FileReaders;
using GroceryCo.Data.Models;
using GroceryCo.Data.Repositories;
using GroceryCo.Data.Repositories.Impl;
using GroceryCo.Data.Serialization;
using GroceryCo.Service.Impl;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace GroceryCo.Service.Test
{
    public class PriceResolverTest
    {

        [Theory]
        [InlineData("Apple", 3, 1, PricingStrategy.Lowest, 2.5)]
        [InlineData("Apple", 3, 2, PricingStrategy.Lowest, 4)]
        [InlineData("Apple", 3, 2, PricingStrategy.Highest, 5.0)]
        public void Resolve(string productName, decimal productPrice, int quantity, PricingStrategy pricingStrategy, double? expectedPrice)
        {
            var promotionRepository = new Mock<IPromotionRepository>();
            promotionRepository.Setup(p => p.FindByProduct("Apple")).Returns(
                new List<Promotion>
                {
                    new Promotion("Test", "Apple", PromotionType.OnSale, null, 2.5m, null, DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(1), null),
                    new Promotion("Test 2", "Apple", PromotionType.GroupSale, 2, 2.0m, null, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1), null),
                    new Promotion("Test 3", "Apple", PromotionType.AdditionalSale, 1, null, 0.5f, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1), null)
                });

            var priceResolver = new PriceResolver(promotionRepository.Object);
            var price = priceResolver.Resolve(productName, productPrice, quantity, pricingStrategy);

            Assert.Equal((decimal?)expectedPrice, price);
        }

        [Theory]
        [InlineData(2, 1, 2)]
        [InlineData(2, 2, 4)]
        public void ResolveNoPromotions(decimal productPrice, int quantity, decimal expectedPrice)
        {
            var promotionRepository = new Mock<IPromotionRepository>();
            promotionRepository.Setup(p => p.FindByProduct("Apple")).Returns(new List<Promotion>());

            var priceResolver = new PriceResolver(promotionRepository.Object);
            var price = priceResolver.Resolve("Test", productPrice, quantity, PricingStrategy.Lowest);

            Assert.Equal(expectedPrice, price);
        }
    }
}
