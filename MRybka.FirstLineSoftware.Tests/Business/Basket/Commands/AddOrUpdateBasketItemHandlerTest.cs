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
    public class AddOrUpdateBasketItemHandlerTest : IDisposable
    {
        private readonly DataContext _dataContext;
        private readonly Mapper _mapper;

        public AddOrUpdateBasketItemHandlerTest()
        {
            _dataContext = DataContextHelper.InMemorySqlite();
            _mapper = new Mapper(new MapperConfiguration(c => c.AddProfile(new BasketItemModelProfile())));
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        [Fact]
        public async Task Handle_ProductNotFound_ReturnsNull()
        {
            var sut = new AddOrUpdateBasketItemHandler(_dataContext, _mapper);


            var result = await sut.Handle(new AddOrUpdateBasketItemCommand()
            {
                ProductId = 0,
                Amount = 10
            }, CancellationToken.None);


            result.Should().BeNull();
        }

        [Fact]
        public async Task Handle_AmountIsZero_ReturnsNull()
        {
            _dataContext.Products.Add(new Product(){ Id = 10, Name = "Test", Price = 100 });
            await _dataContext.SaveChangesAsync();
            var sut = new AddOrUpdateBasketItemHandler(_dataContext, _mapper);


            var result = await sut.Handle(new AddOrUpdateBasketItemCommand()
            {
                ProductId = 10,
                Amount = 0
            }, CancellationToken.None);


            result.Should().BeNull();
        }

        [Fact]
        public async Task Handle_NoBasketItemInDatabse_BasketItemAdded()
        {
            _dataContext.Products.Add(new Product() { Id = 10, Name = "Test", Price = 100 });
            await _dataContext.SaveChangesAsync();
            var sut = new AddOrUpdateBasketItemHandler(_dataContext, _mapper);


            var result = await sut.Handle(new AddOrUpdateBasketItemCommand()
            {
                ProductId = 10,
                Amount = 5
            }, CancellationToken.None);


            var dbResult = await _dataContext.BasketItems.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == 10);
            result.Amount.Should().Be(5);
            result.ProductId.Should().Be(10);
            dbResult.Should().NotBeNull();
            dbResult.Amount.Should().Be(5);
        }

        [Fact]
        public async Task Handle_BasketItemInDatabse_BasketItemUpdated()
        {
            _dataContext.Products.Add(new Product() { Id = 10, Name = "Test", Price = 100 });
            _dataContext.BasketItems.Add(new BasketItem(){Amount = 99, ProductId = 10 });
            await _dataContext.SaveChangesAsync();
            var sut = new AddOrUpdateBasketItemHandler(_dataContext, _mapper);


            var result = await sut.Handle(new AddOrUpdateBasketItemCommand()
            {
                ProductId = 10,
                Amount = 5
            }, CancellationToken.None);


            var dbResult = await _dataContext.BasketItems.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == 10);
            result.Amount.Should().Be(5);
            result.ProductId.Should().Be(10);
            dbResult.Should().NotBeNull();
            dbResult.Amount.Should().Be(5);
        }

    }
}
