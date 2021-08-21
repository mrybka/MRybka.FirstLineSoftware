using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MRybka.FirstLineSoftware.Business.Basket.Models;
using MRybka.FirstLineSoftware.Common;
using MRybka.FirstLineSoftware.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MRybka.FirstLineSoftware.Business.Basket.Query
{

    public record GetBasketQuery : IRequest<BasketModel>
    {

    }


    public class GetBasketQueryValidator : AbstractValidator<GetBasketQuery>
    {
        public GetBasketQueryValidator()
        {
        }
    }

    public class GetBasketHandler : IRequestHandler<GetBasketQuery, BasketModel>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly ICalculator _calculator;

        public GetBasketHandler(DataContext dataContext, IMapper mapper, ICalculator calculator)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _calculator = calculator;
        }

        public async Task<BasketModel> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var items = await _dataContext.BasketItems.Include(b => b.Product).AsNoTracking().ToListAsync();

            decimal sum = 0;
            foreach(var item in items)
            {
                sum += _calculator.GetDiscountedPrice(item.Amount, item.Product.Price, item.Product.DiscountedAmount, item.Product.DiscountedPrice);
            }

            var result = new BasketModel(_mapper.Map<IEnumerable<BasketItemModel>>(items), sum);
            return result;
        }
    }
}
