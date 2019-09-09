namespace GroceryCo.Service.Promotions
{
    public class ApplyAdditionalSalePromotion : IApplyPromotion
    {
        private readonly int discountQuantity;
        private readonly float discount;
        private readonly decimal price;

        public ApplyAdditionalSalePromotion(int discountQuantity, float discount, decimal price) 
        {
            this.discountQuantity = discountQuantity;
            this.discount = discount;
            this.price = price;
        }

        public decimal? Apply(int quantity)
        {

            if (quantity <= discountQuantity) return null;

            return (quantity / (discountQuantity + 1) * discountQuantity * price) 
                + (quantity / (discountQuantity + 1) * (decimal)discount * price) 
                + (quantity % (discountQuantity + 1) * price);
        }
    }
}
