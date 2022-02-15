using Microsoft.Extensions.DependencyInjection;
using SynetecAssessmentApi.Services;
using System;
using Xunit;

namespace SynetecAssessmentApi.Tests
{
    public class BonusPoolCalculatorTests : IClassFixture<Fixture>
    {
        private readonly IBonusPoolCalculator _bonusPoolCalculator;
        public BonusPoolCalculatorTests(Fixture fixture)
        {
            var serviceProvider = fixture.ServiceProvider;
            _bonusPoolCalculator = serviceProvider.GetRequiredService<IBonusPoolCalculator>();
        }

        [Fact]
        public void BonusPoolCalculatorTests_Expect_Success()
        {
            var result = _bonusPoolCalculator.CalculateBonusAllocation(10000, 1000, 100000);
            Assert.Equal(100, result);
        }

        [Fact]
        public void BonusPoolCalculatorTests_Expect_Exception()
        {
            Assert.Throws<DivideByZeroException>(() =>
            {
               _bonusPoolCalculator.CalculateBonusAllocation(10000, 1000, 0);
            });
        }
    }

}
