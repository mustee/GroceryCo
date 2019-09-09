using System;

namespace GroceryCo.Service.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string productName)
        {
            ProductName = productName;
        }

        public string ProductName { get; }
    }
}
