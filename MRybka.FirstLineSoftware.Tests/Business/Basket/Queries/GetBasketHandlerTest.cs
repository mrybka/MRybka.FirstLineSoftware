using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MRybka.FirstLineSoftware.Business.Basket.Command;
using MRybka.FirstLineSoftware.Business.Basket.Models;
using MRybka.FirstLineSoftware.Business.Basket.Query;
using MRybka.FirstLineSoftware.Common;
using MRybka.FirstLineSoftware.Data;
using MRybka.FirstLineSoftware.Data.Entities;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MRybka.FirstLineSoftware.Tests.Business.Basket.Queries
{
    public class GetBasketHandlerTest : IDisposable
    {
        private readonly DataContext _dataContext;
        private readonly Mapper _mapper;

        public GetBasketHandlerTest()
        {
            _dataContext = DataContextHelper.InMemorySqlite();
            _mapper = new Mapper(new MapperConfiguration(c => 
            {
                c.AddProfile(new BasketItemModelProfile());
                c.AddProfile(new BasketItemModelProfile());
            }));
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        [Fact]
        public async Task Handle_BasketItemsInDb_ReturnsBasketWithItems()
        {
            var product1 = new Product() { Id = 10, Name = "Test", Price = 10 };
            _dataContext.BasketItems.Add(new BasketItem() { Amount = 1, ProductId = 10, Product = product1 });

            var product2 = new Product() { Id = 20, Name = "Test2", Price = 20 };
            _dataContext.BasketItems.Add(new BasketItem() { Amount = 2, ProductId = 20, Product = product2 });

            await _dataContext.SaveChangesAsync();

            var calculator = Substitute.For<ICalculator>();
            calculator.GetDiscountedPrice(1, 10).Returns(10);
            calculator.GetDiscountedPrice(2, 20).Returns(40);

            var sut = new GetBasketHandler(_dataContext, _mapper, calculator);



            var result = await sut.Handle(new GetBasketQuery(), CancellationToken.None);



            result.Items.Should().HaveCount(2);
            result.Items.Any(i => i.ProductId == 20 && i.ProductName == "Test2").Should().BeTrue();
            result.Sum.Should().Be(50);
        }
    }
}
