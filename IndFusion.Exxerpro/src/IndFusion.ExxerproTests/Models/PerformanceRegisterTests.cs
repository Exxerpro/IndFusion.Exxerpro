using FluentAssertions;
using IndFusion.Exxerpro.Models;
using Xunit;

namespace IndFusion.Exxerpro.Tests;

public class PerformanceRegisterTests
{
    [Fact]
    public void PerformanceData_ShouldInitializeWithCorrectName()
    {
        // Arrange
        var machineName = "Power Puncher";

        // Act
        var machine = new PerformanceData(machineName, "Test description", 60);

        // Assert
        machine.Name.Should().Be(machineName);
    }

    [Fact]
    public void SetInitialCondition_ShouldSetCorrectValues()
    {
        // Arrange
        var machine = new PerformanceData("Test Machine", "Test description", 60);

        // Act
        machine.UpdateData(new ProductionData(500, 50, 450, 30, DateTime.Now, DateTime.Now.AddMinutes(5)));

        // Assert
        var currentData = machine.ProductionData;
        currentData.ProducedPieces.Should().Be(500);
        currentData.RejectedPieces.Should().Be(50);
        currentData.RunningTime.Should().Be(450);
        currentData.StoppingTime.Should().Be(30);
    }

    [Fact]
    public void DefectiveRate_ShouldBeCalculatedCorrectly()
    {
        // Arrange
        var machine = new PerformanceData("Test Machine", "Test description", 60);

        // Act
        machine.UpdateData(new ProductionData(500, 50, 450, 30, DateTime.Now, DateTime.Now.AddMinutes(5)));
        var defectiveRate = machine.Indicator.Quality;

        // Assert
        defectiveRate.Should().BeApproximately(90.0, 0.01);
    }

    [Fact]
    public void UpdateInfo_ShouldUpdateCorrectValues()
    {
        // Arrange
        var machine = new PerformanceData("Test Machine", "Test description", 60);
        machine.UpdateData(new ProductionData(500, 50, 450, 30, DateTime.Now, DateTime.Now.AddMinutes(5)));

        // Act
        machine.UpdateData(new ProductionData(100, 10, 50, 10, DateTime.Now, DateTime.Now.AddMinutes(5)));

        // Assert
        var currentData = machine.ProductionData;
        currentData.ProducedPieces.Should().Be(600);
        currentData.RejectedPieces.Should().Be(60);
        currentData.RunningTime.Should().Be(500);
        currentData.StoppingTime.Should().Be(40);
    }

    [Fact]
    public void Metrics_ShouldBeCalculatedCorrectly()
    {
        // Arrange
        var machine = new PerformanceData("Test Machine", "Test description", 60);
        machine.UpdateData(new ProductionData(500, 50, 450, 30, DateTime.Now, DateTime.Now.AddMinutes(5)));

        // Act
        var availability = machine.Indicator.Availability;
        var performance = machine.Indicator.Performance;
        var quality = machine.Indicator.Quality;
        var oee = machine.Indicator.Oee;

        // Assert
        availability.Should().BeApproximately((450.0 / (450.0 + 30.0)) * 100, 0.01);
        performance.Should().BeApproximately((500.0 / (450.0 * 60.0)) * 100, 0.01);
        quality.Should().BeApproximately((450.0 / 500.0) * 100, 0.01);
        oee.Should().BeApproximately((availability * performance * quality) / 10_000, 0.01);
    }

    [Fact]
    public void ProducedPieces_ShouldBeNonNegative()
    {
        // Arrange
        var machine = new PerformanceData("Test Machine", "Test description", 60);

        // Act
        machine.UpdateData(new ProductionData(-100, 50, 450, 30, DateTime.Now, DateTime.Now.AddMinutes(5)));

        // Assert
        var currentData = machine.ProductionData;
        currentData.ProducedPieces.Should().Be(0);
    }

    [Fact]
    public void RunningTime_ShouldBeNonNegative()
    {
        // Arrange
        var machine = new PerformanceData("Test Machine", "Test description", 60);

        // Act
        machine.UpdateData(new ProductionData(500, 50, -450, 30, DateTime.Now, DateTime.Now.AddMinutes(5)));

        // Assert
        var currentData = machine.ProductionData;
        currentData.RunningTime.Should().Be(0);
    }

    [Fact]
    public void StoppingTime_ShouldBeNonNegative()
    {
        // Arrange
        var machine = new PerformanceData("Test Machine", "Test description", 60);

        // Act
        machine.UpdateData(new ProductionData(500, 50, 450, -30, DateTime.Now, DateTime.Now.AddMinutes(5)));

        // Assert
        var currentData = machine.ProductionData;
        currentData.StoppingTime.Should().Be(0);
    }


}
