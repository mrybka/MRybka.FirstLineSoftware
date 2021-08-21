using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRybka.FirstLineSoftware.Common
{
    public interface ICalculator
    {
        decimal GetDiscountedPrice(int amount, decimal unitPrice, int? discountedAmount = null, decimal? discountedPrice = null);
    }
}
