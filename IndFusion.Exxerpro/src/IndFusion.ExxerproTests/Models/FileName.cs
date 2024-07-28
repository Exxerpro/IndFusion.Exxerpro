using FluentAssertions;
using IndFusion.Exxerpro.Models;
using Xunit;

namespace IndFusion.Exxerpro.Tests;

public class OeeStateTests2
{
    private readonly IDateTimeMachine _dateTimeMachine;
    private readonly OeeState _oeeState;

    public OeeStateTests2()
    {
        _dateTimeMachine = new DateTimeMachine(); // Assuming you have a fake implementation for testing
        _oeeState = new OeeState(_dateTimeMachine);
    }

    [Fact]

    public void GetMachineHistoricalData_ShouldReturnCorrectData()
    {
        // Arrange
        var machineName = "Power Puncher";
        var now = _dateTimeMachine.Now;
        var expectedData = new List<(DateTime Timestamp, MachineMetrics Metrics)>
        {
            (now.AddMinutes(-15), new MachineMetrics(60, 90, 85, 80)),
            (now.AddMinutes(-10), new MachineMetrics(62, 91, 86, 81)),
            (now.AddMinutes(-5), new MachineMetrics(63, 92, 87, 82))
        };
        // Act
        var result = _oeeState.GetMachineHistoricalData(machineName);

        // Assert
        var resultList = result.ToList();
        for (int i = 0; i < expectedData.Count; i++)
        {
            resultList[i].Timestamp.Should().Be(expectedData[i].Timestamp);
            resultList[i].Metrics.Oee.Should().BeApproximately(expectedData[i].Metrics.Oee, 0.01);
            resultList[i].Metrics.Availability.Should().BeApproximately(expectedData[i].Metrics.Availability, 0.01);
            resultList[i].Metrics.Performance.Should().BeApproximately(expectedData[i].Metrics.Performance, 0.01);
            resultList[i].Metrics.Quality.Should().BeApproximately(expectedData[i].Metrics.Quality, 0.01);
        }
    }
}