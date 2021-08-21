using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRybka.FirstLineSoftware.Common
{
    public class Calculator : ICalculator
    {
        public decimal GetDiscountedPrice(int amount, decimal unitPrice, int? discountedAmount = null, decimal? discountedPrice = null)
        {
            if(discountedAmount == 0)
            {
                throw new ArgumentException("DiscountedPrice can't be zero");
            }

            decimal result;
            if (discountedPrice is not null && discountedAmount is not null)
            {
                result = amount / discountedAmount.Value * discountedPrice.Value + amount % discountedAmount.Value * unitPrice;
            }
            else if (discountedPrice is null && discountedAmount is null)
            {
                result = amount * unitPrice;
            }
            else
            {
                throw new ArgumentException("Method can be used with either none of discountUnits and discountedPrice arguments set or both");
            }

            return result;
        }
    }
}
