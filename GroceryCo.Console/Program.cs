using GroceryCo.Data.FileReaders;
using GroceryCo.Data.Repositories.Impl;
using GroceryCo.Data.Serialization;
using GroceryCo.Service.Impl;
using System.Linq;

namespace GroceryCo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //var promotions = new Promotion[]
            //{
            //    new Promotion("Additional Sale test", "Orange", PromotionType.AdditionalSale, 2, 0, 0.5f, DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(1), null)
            //};

            //var json = JsonConvert.SerializeObject(promotions);
            //System.Console.WriteLine(json);

            var products = new FileReader("Files/items.txt")
                .Read()
                .Split("\n")
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.Trim());

            var productRepository = new ProductRepository(new FileReader("Files/products.json"), new JsonTextSerializer());
            var promotionRepository = new PromotionRepository(new FileReader("Files/promotions.json"), new JsonTextSerializer());

            var saleService = new SaleService(productRepository, new PriceResolver(promotionRepository));
            var sale = saleService.Checkout(products.ToArray());

            var receiptService = new ReceiptService();
            receiptService.Print(sale);

            System.Console.Read();
        }
    }
}
