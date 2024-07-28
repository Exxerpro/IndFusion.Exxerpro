using System;
using System.Collections.Generic;
using FluentAssertions;
using IndFusion.Exxerpro.Models;
using Xunit;

namespace IndFusion.Exxerpro.Tests
{
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
            var expectedData = new List<(DateTime Timestamp, double Oee, double Availability, double Performance, double Quality)>
            {
                (now.AddMinutes(-15), 60, 90, 85, 80),
                (now.AddMinutes(-10), 62, 91, 86, 81),
                (now.AddMinutes(-5), 63, 92, 87, 82)
            };

         
            _oeeState.HistoricalData[0].Machines[0].UpdateInfo(100, 20, 50, 10);
            _oeeState.HistoricalData[1].Machines[0].UpdateInfo(110, 18, 52, 8);
            _oeeState.HistoricalData[2].Machines[0].UpdateInfo(120, 16, 54, 6);

            // Act
            var result = _oeeState.GetMachineHistoricalData(machineName);

            // Assert
            result.Should().BeEquivalentTo(expectedData, options => options.WithStrictOrdering());
        }
    }
}
