using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MRybka.FirstLineSoftware.Business.Basket.Models;
using MRybka.FirstLineSoftware.Data;
using MRybka.FirstLineSoftware.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MRybka.FirstLineSoftware.Business.Basket.Command
{

    public record AddOrUpdateBasketItemCommand : IRequest<BasketItemModel?>
    {
        public int ProductId { get;set;}
        public int Amount { get;set;}
    }


    public class AddOrUpdateBasketItemCommandValidator : AbstractValidator<AddOrUpdateBasketItemCommand>
    {
        public AddOrUpdateBasketItemCommandValidator()
        {
            RuleFor(x => x.Amount).NotEmpty();
        }
    }

    public class AddOrUpdateBasketItemHandler : IRequestHandler<AddOrUpdateBasketItemCommand, BasketItemModel?>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public AddOrUpdateBasketItemHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<BasketItemModel?> Handle(AddOrUpdateBasketItemCommand request, CancellationToken cancellationToken)
        {
            if(request.Amount == 0)
            {
                return null;
            }

            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

            if(product is null)
            {
                return null;
            }

            var basketItem = await _dataContext.BasketItems.Include(b => b.Product)
                .FirstOrDefaultAsync(x => x.ProductId == request.ProductId);

            if(basketItem is null)
            {
                basketItem = new BasketItem();
                basketItem.Product = product;
                basketItem.Amount = request.Amount;
                _dataContext.Add(basketItem);
            }
            else
            {
                basketItem.Amount = request.Amount;
            }

            await _dataContext.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<BasketItemModel>(basketItem);

            return result;
        }
    }
}
