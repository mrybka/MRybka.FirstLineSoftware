using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MRybka.FirstLineSoftware.Business.Basket;
using MRybka.FirstLineSoftware.Business.Basket.Command;
using MRybka.FirstLineSoftware.Business.Basket.Models;
using MRybka.FirstLineSoftware.Business.Basket.Query;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MRybka.FirstLineSoftware.Tests.Business.Basket
{
    public class BasketControllerTest : IDisposable
    {
        public void Dispose()
        {
        }

        [Fact]
        public async Task GetBasket_Succeeded_ReturnsOk()
        {
            var mediator = Substitute.For<IMediator>();
            mediator.Send(new GetBasketQuery())
                .Returns(new BasketModel(new List<BasketItemModel>(), 10));


            var sut = new BasketController(mediator);


            var result = await sut.GetBasket();

            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
        }

        [Fact]
        public async Task AddOrUpdateBasketItem_Failed_ReturnsBadRequest()
        {
            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Any<AddOrUpdateBasketItemCommand>(), Arg.Any<CancellationToken>())
                .Returns((BasketItemModel)null);


            var sut = new BasketController(mediator);


            var result = await sut.AddOrUpdateBasketItem(Arg.Any<AddOrUpdateBasketItemCommand>());

            var badRequest = result.Result as BadRequestResult;
            badRequest.Should().NotBeNull();
        }

        [Fact]
        public async Task AddOrUpdateBasketItem_Succeeded_ReturnsOk()
        {
            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Any<AddOrUpdateBasketItemCommand>())
                .Returns(new BasketItemModel(){ Amount = 1, ProductId = 1, ProductName = "Test"});


            var sut = new BasketController(mediator);


            var result = await sut.AddOrUpdateBasketItem(new AddOrUpdateBasketItemCommand());

            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
        }

        [Fact]
        public async Task RemoveBasketItem_Failed_ReturnsBadRequest()
        {
            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Any<RemoveBasketItemCommand>(), Arg.Any<CancellationToken>())
                .Returns(false);


            var sut = new BasketController(mediator);


            var result = await sut.RemoveBasketItem(new RemoveBasketItemCommand(10));

            var badRequest = result as BadRequestResult;
            badRequest.Should().NotBeNull();
        }

        [Fact]
        public async Task RemoveBasketItem_Succeeded_ReturnsOk()
        {
            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Any<RemoveBasketItemCommand>(), Arg.Any<CancellationToken>())
                .Returns(true);


            var sut = new BasketController(mediator);


            var result = await sut.RemoveBasketItem(new RemoveBasketItemCommand(10));

            var badRequest = result as OkResult;
            badRequest.Should().NotBeNull();
        }
    }
}
