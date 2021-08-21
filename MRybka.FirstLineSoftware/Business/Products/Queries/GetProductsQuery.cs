using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MRybka.FirstLineSoftware.Business.Products.Models;
using MRybka.FirstLineSoftware.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MRybka.FirstLineSoftware.Business.Products.Queries
{
    public record GetProductsQuery : IRequest<IEnumerable<ProductModel>>
    {

    }

    public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductModel>>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public GetProductsHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _dataContext.Products.ToListAsync();

            var result = _mapper.Map<IEnumerable<ProductModel>>(products);

            return result;
        }
    }
}