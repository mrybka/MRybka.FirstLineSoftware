using MRybka.FirstLineSoftware.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRybka.FirstLineSoftware.Business.Basket.Models
{
    public record BasketItemModel
    {
        public int ProductId { get; set; }
        public string ProductName { get;set;}
        public int Amount { get; set; }
    }

    public class BasketItemModelProfile : AutoMapper.Profile
    {
        public BasketItemModelProfile()
        {
            CreateMap<BasketItem, BasketItemModel>()
                .ForMember(x => x.ProductName, y=>y.MapFrom(z => z.Product.Name));
        }
    }
}
