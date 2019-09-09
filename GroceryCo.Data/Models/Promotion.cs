using System;

namespace GroceryCo.Data.Models
{
    public class Promotion : IHasName
    {
        public Promotion(string name, string productName, PromotionType promotionType, int? quantity, decimal? price, float? discount, DateTime startDate, 
            DateTime endDate, string description)
        {
            Name = name;
            ProductName = productName;
            PromotionType = promotionType;
            Quantity = quantity;
            Price = price;
            Discount = discount;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }

        public string Name { get; }

        public string ProductName { get; }

        public PromotionType PromotionType { get; }

        public int? Quantity { get; }

        public decimal? Price { get; }

        public float? Discount { get; }

        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        public string Description { get; }
    }

    public enum PromotionType
    {
        OnSale,
        GroupSale,
        AdditionalSale
    }
}
