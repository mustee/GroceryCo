using System;
using System.Linq;
using GroceryCo.Data.Models;
using GroceryCo.Data.Repositories;
using GroceryCo.Service.Exceptions;
using GroceryCo.Service.Promotions;

namespace GroceryCo.Service.Impl
{
    public class PriceResolver : IPriceResolver
    {
        private readonly IPromotionRepository promotionRepository;

        public PriceResolver(IPromotionRepository promotionRepository)
        {
            this.promotionRepository = promotionRepository;
        }

        public decimal Resolve(string productName, decimal productPrice, int quantity, PricingStrategy pricingStrategy)
        {
            if (string.IsNullOrWhiteSpace(productName)) throw new ArgumentException(nameof(productName));

            if(productPrice == default) throw new ArgumentException(nameof(productPrice));

            var price = PromotionPrice(productName, productPrice, quantity, pricingStrategy);

            return price ?? (productPrice * quantity);
        }

        private decimal? PromotionPrice(string productName, decimal productPrice, int quantity, PricingStrategy pricingStrategy)
        {
            var promotions = promotionRepository.FindByProduct(productName);
            if (!promotions.Any()) return null;

            decimal? price = null;
            foreach (var promotion in promotions)
            {
                IApplyPromotion applyPromotion = null;

                switch (promotion.PromotionType)
                {
                    case PromotionType.OnSale:
                        applyPromotion = new ApplyOnSalePromotion(promotion.Price.Value);
                        break;
                    case PromotionType.GroupSale:
                        applyPromotion = new ApplyGroupSalePromotion(promotion.Quantity.Value, promotion.Price.Value, productPrice);
                        break;
                    case PromotionType.AdditionalSale:
                        applyPromotion = new ApplyAdditionalSalePromotion(promotion.Quantity.Value, promotion.Discount.Value, productPrice);
                        break;
                    default:
                        throw new PromotionTypeUnhandledException(promotion.PromotionType);
                }

                var promotionPrice = applyPromotion.Apply(quantity);
                if (!promotionPrice.HasValue) continue;

                price = !price.HasValue
                    ? promotionPrice
                    : (pricingStrategy == PricingStrategy.Lowest && promotionPrice < price
                        ? promotionPrice
                        : (pricingStrategy == PricingStrategy.Highest && promotionPrice > price 
                            ? promotionPrice : price));
            }

            return price;
        }
        
    }
}
