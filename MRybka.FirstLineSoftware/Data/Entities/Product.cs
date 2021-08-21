#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRybka.FirstLineSoftware.Data.Entities
{
    public class Product
    {
        public int Id { get;set;}
        public string Name { get;set;}
        public decimal Price { get;set;}
        public int? DiscountedAmount { get;set;}
        public decimal? DiscountedPrice { get;set;}

        public virtual HashSet<BasketItem> BasketItems { get;set;}
    }
}
