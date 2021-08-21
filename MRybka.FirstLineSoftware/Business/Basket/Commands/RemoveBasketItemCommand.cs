using AutoMapper;
using FluentValidation;
using MediatR;
using MRybka.FirstLineSoftware.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MRybka.FirstLineSoftware.Business.Basket.Command
{

    public record RemoveBasketItemCommand(int ProductId) : IRequest<bool>
    {
    }

    public class RemoveBasketItemHandler : IRequestHandler<RemoveBasketItemCommand, bool>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public RemoveBasketItemHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<bool> Handle(RemoveBasketItemCommand request, CancellationToken cancellationToken)
        {

            var basketItem = await _dataContext.BasketItems.FindAsync(request.ProductId);

            if(basketItem is null)
            {
                return false;
            }

            _dataContext.Remove(basketItem);

            await _dataContext.SaveChangesAsync(cancellationToken);

            return true;

        }
    }
}
