using FluentAssertions;
using IndFusion.Exxerpro.Models;
using Microsoft.Extensions.Time.Testing;
using Xunit;

namespace IndFusion.Exxerpro.Tests;

public class OeeStateTests
{
    private readonly FakeTimeProvider _fakeTimeProvider;
    private readonly IDateTimeMachine _dateTimeMachine;
    private readonly OeeState _oeeState;

    public OeeStateTests()
    {
        _fakeTimeProvider = new FakeTimeProvider();
        _dateTimeMachine = new DateTimeMachine(_fakeTimeProvider);

        // Initialize OeeState with controlled time
        _oeeState = new OeeState(_dateTimeMachine);
    }

    [Fact]
    public void OeeState_ShouldInitializeWithHistoricalData()
    {
        // Arrange
        var initialTime = DateTimeOffset.Now;
        _fakeTimeProvider.SetUtcNow(initialTime);

        // Act
        var oeeState = new OeeState(_dateTimeMachine);

        // Assert
        oeeState.Machines.Should().NotBeEmpty();
        oeeState.Machines.Values.First().HistoricData.First().StartTime.Should().BeCloseTo(initialTime.AddHours(-8).DateTime, TimeSpan.FromMinutes(1));
    }





    [Fact]
    public void OeeState_ShouldGenerateInitialDataPoints()
    {
        // Arrange
        var initialTime = DateTimeOffset.Now;
        _fakeTimeProvider.SetUtcNow(initialTime);

        // Act
        var oeeState = new OeeState(_dateTimeMachine);

        // Assert
        oeeState.Machines["Power Puncher"].HistoricData.Should().HaveCountGreaterThan(0);
        oeeState.Machines["Power Puncher"].HistoricData.First().StartTime.Should().BeCloseTo(initialTime.AddHours(-8).DateTime, TimeSpan.FromMinutes(5));
    }
}
