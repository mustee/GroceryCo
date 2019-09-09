using GroceryCo.Data.Repositories;
using GroceryCo.Service.Impl;
using Moq;
using Xunit;
using System.Linq;
using GroceryCo.Service.Exceptions;

namespace GroceryCo.Service.Test
{
    public class SaleServiceTest
    {

        [Fact]
        public void Checkout()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(m => m.Find("Apple")).Returns(new Data.Models.Product("Apple", 2));
            mockProductRepository.Setup(m => m.Find("Orange")).Returns(new Data.Models.Product("Orange", 3));

            var mockPriceResolver = new Mock<IPriceResolver>();
            mockPriceResolver.Setup(p => p.Resolve("Apple", 2, 1, PricingStrategy.Lowest)).Returns(2);
            mockPriceResolver.Setup(p => p.Resolve("Apple", 2, 2, PricingStrategy.Lowest)).Returns(4);
            mockPriceResolver.Setup(p => p.Resolve("Orange", 3, 1, PricingStrategy.Lowest)).Returns(3);

            var saleService = new SaleService(mockProductRepository.Object, mockPriceResolver.Object);
            var sale = saleService.Checkout(new string[] { "Apple" });

            Assert.NotNull(sale);
            Assert.Single(sale.SaleItems);
            Assert.Equal("Apple", sale.SaleItems.ElementAt(0).ProductName);
            Assert.Equal(2, sale.SaleItems.ElementAt(0).Amount);
            Assert.Equal(1, sale.SaleItems.ElementAt(0).Quantity);
            Assert.Equal(2, sale.Total);

            sale = saleService.Checkout(new string[] { "Apple", "Apple" });
            Assert.NotNull(sale);
            Assert.Single(sale.SaleItems);
            Assert.Equal("Apple", sale.SaleItems.ElementAt(0).ProductName);
            Assert.Equal(4, sale.SaleItems.ElementAt(0).Amount);
            Assert.Equal(2, sale.SaleItems.ElementAt(0).Quantity);
            Assert.Equal(4, sale.Total);

            sale = saleService.Checkout(new string[] { "Apple", "Apple", "Orange" });
            Assert.NotNull(sale);
            Assert.Equal(2, sale.SaleItems.Count());
            Assert.Contains(sale.SaleItems, s => s.ProductName == "Apple");
            Assert.Contains(sale.SaleItems, s => s.ProductName == "Orange");
            Assert.Equal(7, sale.Total);

            Assert.Throws<ProductNotFoundException>(() => saleService.Checkout(new string[] { "Banana" }));

        }
    }
}
