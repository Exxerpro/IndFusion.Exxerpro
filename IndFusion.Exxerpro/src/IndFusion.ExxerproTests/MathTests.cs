namespace IndFusion.ExxerproTests
{
    using FluentAssertions;
    using IndFusion.Exxerpro.Domain.Models.Mahts;
    using Xunit;

    public class AdvancedTrapezoidFunctionTests
    {
        [Fact]
        public void Evaluate_ShouldReturnZero_BeforeStartRise()
        {
            // Arrange
            var function = new AdvancedTrapezoidFunction(1, 3, 5, 7, 10, 0.1, 0.5);

            // Act
            double result = function.Evaluate(0.5);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void Evaluate_ShouldIncreaseDuringRisePhase()
        {
            // Arrange
            var function = new AdvancedTrapezoidFunction(1, 3, 5, 7, 10, 0.1, 0.5);
            double previousValue = function.Evaluate(1);

            // Act & Assert
            for (double x = 1.1; x <= 3; x += 0.1)
            {
                double currentValue = function.Evaluate(x);
                currentValue.Should().BeGreaterThan(previousValue);
                previousValue = currentValue;
            }
        }

        [Fact]
        public void Evaluate_ShouldBeStableWithMinorFluctuations()
        {
            // Arrange
            var function = new AdvancedTrapezoidFunction(1, 3, 5, 7, 10, 0.1, 0.5);
            double baseValue = 10;  // Peak value without noise

            // Act & Assert
            for (double x = 3.1; x < 5; x += 0.1)
            {
                double result = function.Evaluate(x);
                result.Should().BeInRange(baseValue - 0.5, baseValue + 0.5);  // Expecting minor fluctuations around the peak
            }
        }

        [Fact]
        public void Evaluate_ShouldDecreaseDuringFallPhase()
        {
            // Arrange
            var function = new AdvancedTrapezoidFunction(1, 3, 5, 7, 10, 0.1, 0.5);
            double previousValue = function.Evaluate(5);

            // Act & Assert
            for (double x = 5.1; x <= 7; x += 0.1)
            {
                double currentValue = function.Evaluate(x);
                currentValue.Should().BeLessThan(previousValue);
                previousValue = currentValue;
            }
        }

        [Fact]
        public void Evaluate_ShouldReturnZero_AfterEndFall()
        {
            // Arrange
            var function = new AdvancedTrapezoidFunction(1, 3, 5, 7, 10, 0.1, 0.5);

            // Act
            double result = function.Evaluate(7.5);

            // Assert
            result.Should().Be(0);
        }
    }

}
