using System;
using System.Collections.Generic;
using FluentAssertions;
using IndFusion.Exxerpro.Models;
using Microsoft.Extensions.Time.Testing;
using Xunit;

namespace IndFusion.Exxerpro.Tests
{
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
            oeeState.HistoricalData.Should().NotBeEmpty();
            oeeState.HistoricalData.First().Timestamp.Should().BeCloseTo(initialTime.AddHours(-8).DateTime, TimeSpan.FromMinutes(1));
        }

        [Fact]
        public void OeeState_ShouldAddNewData()
        {
            // Arrange
            var initialTime = DateTimeOffset.Now;
            _fakeTimeProvider.SetUtcNow(initialTime);
            var newData = new OeeData
            {
                Timestamp = initialTime.DateTime,
                Machines = new List<MachineOee>()
            };

            // Act
            _oeeState.UpdateData(newData);

            // Assert
            _oeeState.HistoricalData.Should().ContainSingle(data => data.Timestamp == initialTime.DateTime);
        }

        [Fact]
        public void OeeState_ShouldGenerateNewDataPoint()
        {
            // Arrange
            var initialTime = DateTimeOffset.Now;
            _fakeTimeProvider.SetUtcNow(initialTime);

            // Act
            var newDataPoint = _oeeState.GenerateNewDataPoint(initialTime.DateTime);

            // Assert
            newDataPoint.Should().NotBeNull();
            newDataPoint.Timestamp.Should().Be(initialTime.DateTime);
            newDataPoint.Machines.Should().NotBeEmpty();
        }

        [Fact]
        public void OeeState_ShouldNotifyOnDataChange()
        {
            // Arrange
            bool wasNotified = false;
            _oeeState.OnChange += () => wasNotified = true;

            var initialTime = DateTimeOffset.Now;
            _fakeTimeProvider.SetUtcNow(initialTime);
            var newData = new OeeData
            {
                Timestamp = initialTime.DateTime,
                Machines = new List<MachineOee>()
            };

            // Act
            _oeeState.UpdateData(newData);

            // Assert
            wasNotified.Should().BeTrue();
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
            oeeState.HistoricalData.Should().HaveCountGreaterThan(0);
            oeeState.HistoricalData.First().Timestamp.Should().BeCloseTo(initialTime.AddHours(-8).DateTime, TimeSpan.FromMinutes(5));
        }
    }
}
