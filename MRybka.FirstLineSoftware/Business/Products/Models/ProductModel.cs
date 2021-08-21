using MRybka.FirstLineSoftware.Business.Basket.Models;
using MRybka.FirstLineSoftware.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRybka.FirstLineSoftware.Business.Products.Models
{
    public record ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? DiscountedAmount { get; set; }
        public decimal? DiscountedPrice { get; set; }
    }

    public class ProductModelProfile : AutoMapper.Profile
    {
        public ProductModelProfile()
        {
            CreateMap<Product, ProductModel>();
        }
    }
}
