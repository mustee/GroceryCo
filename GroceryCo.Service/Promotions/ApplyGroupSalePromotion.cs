namespace GroceryCo.Service.Promotions
{
    public class ApplyGroupSalePromotion : IApplyPromotion
    {
        private readonly int groupQuantity;
        private readonly decimal groupPrice;
        private readonly decimal price;

        public ApplyGroupSalePromotion(int groupQuantity, decimal groupPrice, decimal price) 
        {
            this.groupQuantity = groupQuantity;
            this.groupPrice = groupPrice;
            this.price = price;
        }

        public decimal? Apply(int quantity)
        {
            if (quantity < groupQuantity) return null;

            return (quantity / groupQuantity * groupQuantity * groupPrice) + (quantity % groupQuantity * price);
        }
    }
}
