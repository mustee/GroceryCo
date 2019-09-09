namespace GroceryCo.Service.Promotions
{
    public class ApplyOnSalePromotion : IApplyPromotion
    {
        private readonly decimal onSalePrice;

        public ApplyOnSalePromotion(decimal onSalePrice)
        {
            this.onSalePrice = onSalePrice;
        }
    

        public decimal? Apply(int quantity)
        {
            return onSalePrice * quantity;
        }
    }
}
