using GroceryCo.Data.Models;
using System;

namespace GroceryCo.Service.Exceptions
{
    public class PromotionTypeUnhandledException : Exception
    {
        public PromotionTypeUnhandledException(PromotionType promotionType)
        {
            PromotionType = promotionType;
        }

        public PromotionType PromotionType { get; }
    }
}
