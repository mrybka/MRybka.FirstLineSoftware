using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRybka.FirstLineSoftware.Business.Basket.Models
{
    public record BasketModel(IEnumerable<BasketItemModel> Items, decimal Sum)
    {
    }
}
