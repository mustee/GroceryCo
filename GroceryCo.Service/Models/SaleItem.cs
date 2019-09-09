namespace GroceryCo.Service.Models
{
    public class SaleItem
    {
        public SaleItem(string productName, int quantity, decimal amount)
        {
            ProductName = productName;
            Quantity = quantity;
            Amount = amount;
        }

        public string ProductName { get; }

        public int Quantity { get; }

        public decimal Amount { get; }
    }
}
