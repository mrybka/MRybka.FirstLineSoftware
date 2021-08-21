using AutoMapper;
using FluentAssertions;
using MRybka.FirstLineSoftware.Business.Products.Models;
using MRybka.FirstLineSoftware.Business.Products.Queries;
using MRybka.FirstLineSoftware.Data;
using MRybka.FirstLineSoftware.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MRybka.FirstLineSoftware.Tests.Business.Products.Queries
{
    public class GetProductsTest : IDisposable
    {
        private readonly DataContext _dataContext;
        private readonly Mapper _mapper;

        public GetProductsTest()
        {
            _dataContext = DataContextHelper.InMemorySqlite();
            _mapper = new Mapper(new MapperConfiguration(c => c.AddProfile(new ProductModelProfile())));
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        [Fact]
        public async Task Handle_Succeeded_ReturnsAllProducts()
        {
            var sut = new GetProductsHandler(_dataContext, _mapper);

            var result = await sut.Handle(new GetProductsQuery(), CancellationToken.None);

            result.Should().HaveCount(3);
        }
    }
}
