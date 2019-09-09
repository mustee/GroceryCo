using System;
using GroceryCo.Service.Models;

namespace GroceryCo.Service.Impl
{
    public class ReceiptService : IReceiptService
    {
        public void Print(Sale sale)
        {
            foreach(var saleItem in sale.SaleItems)
            {
                Console.WriteLine($"{saleItem.ProductName}\t{saleItem.Quantity}\t{saleItem.Amount}");
            }

            Console.WriteLine($"Total\t\t\t{sale.Total}");
        }
    }
}
