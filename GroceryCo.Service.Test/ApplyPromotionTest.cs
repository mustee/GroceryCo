using GroceryCo.Service.Promotions;
using Xunit;

namespace GroceryCo.Service.Test
{
    public class ApplyPromotionTest
    {
        [Theory]
        [InlineData(1, 1.0, 1.0)]
        [InlineData(2, 2.5, 5.0)]
        public void OnSalePromotionTest(int quantity, decimal onSalePrice,  decimal expectedPrice)
        {
            var promotion = new ApplyOnSalePromotion(onSalePrice);
            var promotionPrice = promotion.Apply(quantity);

            Assert.Equal(expectedPrice, promotionPrice);
        }

        [Theory]
        [InlineData(2, 2, 1.5, 2, 3)]
        [InlineData(3, 2, 1.5, 2, 5)]
        [InlineData(1, 2, 1.5, 2, null)]
        public void GroupPromotionTest(int quantity, int groupQuantity, decimal groupPrice, decimal actualPrice, double? expectedPrice)
        {
            var promotion = new ApplyGroupSalePromotion(groupQuantity, groupPrice, actualPrice);
            var promotionPrice = promotion.Apply(quantity);

            Assert.Equal((decimal?)expectedPrice, promotionPrice);
        }

        [Theory]
        [InlineData(2, 1, 0.5, 2, 3.0)]
        [InlineData(3, 1, 0.5, 2, 5.0)]
        [InlineData(3, 2, 0.5, 2, 5.0)]
        [InlineData(2, 2, 0.5, 2, null)]
        public void AdditionalSalePromotionTest(int quantity, int groupQuantity, float discount, decimal actualPrice, double? expectedPrice)
        {
            var promotion = new ApplyAdditionalSalePromotion(groupQuantity, discount, actualPrice);
            var promotionPrice = promotion.Apply(quantity);

            Assert.Equal((decimal?)expectedPrice, promotionPrice);
        }
    }
}
