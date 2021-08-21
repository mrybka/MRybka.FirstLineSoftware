using AutoMapper;
using FluentAssertions;
using MediatR;
using MRybka.FirstLineSoftware.Common;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MRybka.FirstLineSoftware.Tests.Common
{
    public class CalculatorTest : IDisposable
    {
        public CalculatorTest()
        {
        }

        public void Dispose()
        {
        }

        [Fact]
        public void GetDiscountedPrice_OnlyDiscountedAmount_ThrowsArgumentException()
        {
            var sut = new Calculator();


            Action act =  () => sut.GetDiscountedPrice(Arg.Any<int>(), Arg.Any<int>(), 10, null);


            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GetDiscountedPrice_OnlyDiscountedPrice_ThrowsArgumentException()
        {
            var sut = new Calculator();


            Action act = () => sut.GetDiscountedPrice(Arg.Any<int>(), Arg.Any<int>(), null, 10);


            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GetDiscountedPrice_DiscountedPriceIsZero_ThrowsArgumentException()
        {
            var sut = new Calculator();


            Action act = () => sut.GetDiscountedPrice(Arg.Any<int>(), Arg.Any<int>(), 0, 10);


            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GetDiscountedPrice_OnlyPriceAndAmountArguments_ReturnsPrice()
        {
            var sut = new Calculator();


            var result = sut.GetDiscountedPrice(5, 10);


            result.Should().Be(50);
        }

        [Fact]
        public void GetDiscountedPrice_WithDiscount_ReturnsPrice()
        {
            var sut = new Calculator();


            var result = sut.GetDiscountedPrice(3, 10, 2, 15);


            result.Should().Be(25);
        }

        [Fact]
        public void GetDiscountedPrice_AmountTooSmallForDiscount_ReturnsPrice()
        {
            var sut = new Calculator();


            var result = sut.GetDiscountedPrice(1, 10, 2, 15);


            result.Should().Be(10);
        }
    }
}
