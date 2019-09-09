using System.Collections.Generic;
using System.Linq;

namespace GroceryCo.Service.Models
{
    public class Sale
    {
        public Sale(IEnumerable<SaleItem> saleItems)
        {
            SaleItems = saleItems;
        }

        public IEnumerable<SaleItem> SaleItems { get; }

        public decimal Total => SaleItems.ToList().Sum(s => s.Amount);
    }
}
