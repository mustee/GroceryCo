namespace GroceryCo.Data.Models
{
    public class Product : IHasName
    {
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; }

        public decimal Price { get; }
    }
}
