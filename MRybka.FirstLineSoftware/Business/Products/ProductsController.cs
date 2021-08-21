using MediatR;
using Microsoft.AspNetCore.Mvc;
using MRybka.FirstLineSoftware.Business.Products.Models;
using MRybka.FirstLineSoftware.Business.Products.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRybka.FirstLineSoftware.Business.Products
{
    [ApiController]
    [Route("products")]
    public class ProductsController
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            var result = await _mediator.Send(new GetProductsQuery());

            return result;
        }
    }
}
