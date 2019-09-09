using GroceryCo.Data.FileReaders;
using GroceryCo.Data.Models;
using GroceryCo.Data.Repositories;
using GroceryCo.Data.Repositories.Impl;
using GroceryCo.Data.Serialization;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GroceryCo.Data.Test
{
    public class ProductRepositoryTest
    {
        private readonly IProductRepository productRepository;
        public ProductRepositoryTest()
        {
            Mock<IFileReader> reader = new Mock<IFileReader>();
            reader.Setup(r => r.Read()).Returns(@"[{""Name"": ""Apple"",""Price"": 2.5}]");

            productRepository = new ProductRepository(reader.Object, new JsonTextSerializer());
        }

        [Theory]
        [InlineData("Apple")]
        [InlineData("apple")]
        public void FindByName(string name)
        {
            var product = productRepository.Find(name);
            Assert.NotNull(product);
        }

        [Theory]
        [InlineData("Apple1")]
        public void FindByNameNotFound(string name)
        {
            var promotion = productRepository.Find(name);
            Assert.Null(promotion);
        }
    }
}
