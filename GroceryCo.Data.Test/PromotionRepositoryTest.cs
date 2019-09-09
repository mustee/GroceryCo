using GroceryCo.Data.FileReaders;
using GroceryCo.Data.Models;
using GroceryCo.Data.Repositories;
using GroceryCo.Data.Repositories.Impl;
using GroceryCo.Data.Serialization;
using Moq;
using Newtonsoft.Json;
using System;
using System.Linq;
using Xunit;

namespace GroceryCo.Data.Test
{
    public class PromotionRepositoryTest
    {
        private readonly IPromotionRepository promotionRepository;

        public PromotionRepositoryTest()
        {
            var promotions = new Promotion[]
            {
                new Promotion("Test", "Apple", PromotionType.OnSale, null, 2.5m, null, DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(1), null),
                new Promotion("Test2", "Apple", PromotionType.GroupSale, 2, 2.0m, null, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1), null),
                new Promotion("Test3", "Orange", PromotionType.AdditionalSale, 2, null, 0.5f, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2), null),
            };
            Mock<IFileReader> reader = new Mock<IFileReader>();
            reader.Setup(r => r.Read()).Returns(JsonConvert.SerializeObject(promotions));

            promotionRepository = new PromotionRepository(reader.Object, new JsonTextSerializer());
        }

        [Theory]
        [InlineData("Test")]
        [InlineData("Test2")]
        [InlineData("test")]
        [InlineData("test2")]
        [InlineData("Test3")]
        public void FindByName(string name)
        {
            var promotion = promotionRepository.Find(name);
            Assert.NotNull(promotion);
        }

        [Theory]
        [InlineData("Test1")]
        [InlineData("Test11")]
        public void FindByNameNotFound(string name)
        {
            var promotion = promotionRepository.Find(name);
            Assert.Null(promotion);
        }


        [Theory]
        [InlineData("Apple")]
        [InlineData("apple")]
        public void FindByProductName(string productName)
        {
            var applePromotions = promotionRepository.FindByProduct(productName);
            Assert.True(applePromotions.Any());
        }

        [Theory]
        [InlineData("Apple1")]
        [InlineData("Orange")]
        public void FindByProductNameNotFound(string productName)
        {
            var applePromotions = promotionRepository.FindByProduct(productName);
            Assert.False(applePromotions.Any());
        }
    }
}
