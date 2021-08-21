using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MRybka.FirstLineSoftware.Business.Basket.Command;
using MRybka.FirstLineSoftware.Business.Basket.Models;
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

namespace MRybka.FirstLineSoftware.Tests.Business.Basket.Commands
{
    public class RemoveBasketItemHandlerTest : IDisposable
    {
        private readonly DataContext _dataContext;
        private readonly Mapper _mapper;

        public RemoveBasketItemHandlerTest()
        {
            _dataContext = DataContextHelper.InMemorySqlite();
            _mapper = new Mapper(new MapperConfiguration(c => c.AddProfile(new BasketItemModelProfile())));
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        [Fact]
        public async Task Handle_BasketItemNotFound_ReturnsFalse()
        {
            var sut = new RemoveBasketItemHandler(_dataContext, _mapper);


            var result = await sut.Handle(new RemoveBasketItemCommand(10), CancellationToken.None);


            result.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_BasketItemRemoved_ReturnsTrue()
        {
            _dataContext.Products.Add(new Product() { Id = 10, Name = "Test", Price = 100 });
            _dataContext.BasketItems.Add(new BasketItem() { Amount = 99, ProductId = 10 });
            await _dataContext.SaveChangesAsync();
            var sut = new RemoveBasketItemHandler(_dataContext, _mapper);


            var result = await sut.Handle(new RemoveBasketItemCommand(10), CancellationToken.None);


            var dbResult = await _dataContext.BasketItems.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == 10);
            result.Should().BeTrue();
            dbResult.Should().BeNull();
        }
    }
}
