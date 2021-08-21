using MediatR;
using Microsoft.AspNetCore.Mvc;
using MRybka.FirstLineSoftware.Business.Basket.Command;
using MRybka.FirstLineSoftware.Business.Basket.Models;
using MRybka.FirstLineSoftware.Business.Basket.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRybka.FirstLineSoftware.Business.Basket
{
    [ApiController]
    [Route("basket")]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<BasketItemModel>> GetBasket()
        {
            var result = await _mediator.Send(new GetBasketQuery());

            return Ok(result);
        }

        [HttpPost]
        [Route("items")]
        public async Task<ActionResult<BasketItemModel>> AddOrUpdateBasketItem(AddOrUpdateBasketItemCommand request)
        {
            var result = await _mediator.Send(request);

            if(result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("items")]
        public async Task<ActionResult> RemoveBasketItem(RemoveBasketItemCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
