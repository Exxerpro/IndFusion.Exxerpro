using System;
using FluentAssertions;
using IndFusion.Exxerpro.Models;
using Xunit;

namespace IndFusion.Exxerpro.Tests
{
    public class MachineOeeTests
    {
        [Fact]
        public void MachineOee_ShouldInitializeWithCorrectName()
        {
            // Arrange
            var machineName = "Power Puncher";

            // Act
            var machine = new MachineOee(machineName);

            // Assert
            machine.Name.Should().Be(machineName);
        }

        [Fact]
        public void SetInitialCondition_ShouldSetCorrectValues()
        {
            // Arrange
            var machine = new MachineOee("Test Machine", 60);

            // Act
            machine.SetInitialCondition(500, 50, 450, 30);

            // Assert
            machine.ProducedPieces.Should().Be(500);
            machine.RejectedPieces.Should().Be(50);
            machine.RunningTime.Should().Be(450);
            machine.StoppingTime.Should().Be(30);
        }

        [Fact]
        public void DefectiveRate_ShouldBeCalculatedCorrectly()
        {
            // Arrange
            var machine = new MachineOee("Test Machine", 60);

            // Act
            machine.SetInitialCondition(500, 50, 450, 30);
            var defectiveRate = machine.DefectiveRate;

            // Assert
            defectiveRate.Should().Be(50.0 / 500.0);
        }

        [Fact]
        public void UpdateInfo_ShouldUpdateCorrectValues()
        {
            // Arrange
            var machine = new MachineOee("Test Machine", 60);
            machine.SetInitialCondition(500, 50, 450, 30);

            // Act
            machine.UpdateInfo(100, 10, 50, 10);

            // Assert
            machine.ProducedPieces.Should().Be(600);
            machine.RejectedPieces.Should().Be(60);
            machine.RunningTime.Should().Be(500);
            machine.StoppingTime.Should().Be(40);
        }

        [Fact]
        public void Metrics_ShouldBeCalculatedCorrectly()
        {
            // Arrange
            var machine = new MachineOee("Test Machine", 60);
            machine.SetInitialCondition(500, 50, 450, 30);

            // Act
            var availability = machine.Availability;
            var performance = machine.Performance;
            var quality = machine.Quality;
            var oee = machine.Oee;

            // Assert
            availability.Should().BeApproximately((450.0 / (450.0 + 30.0)) * 100, 0.01);
            performance.Should().BeApproximately((500.0 / (450.0 * 60.0)) * 100, 0.01);
            quality.Should().BeApproximately((450.0 / 500.0) * 100, 0.01);
            oee.Should().BeApproximately(((availability / 100) * (performance / 100) * (quality / 100)) * 100, 0.01);
        }

        [Fact]
        public void ProducedPieces_ShouldBeNonNegative()
        {
            // Arrange
            var machine = new MachineOee("Test Machine", 60);

            // Act
            machine.SetInitialCondition(-100, 50, 450, 30);

            // Assert
            machine.ProducedPieces.Should().Be(0);
        }

        [Fact]
        public void RunningTime_ShouldBeNonNegative()
        {
            // Arrange
            var machine = new MachineOee("Test Machine", 60);

            // Act
            machine.SetInitialCondition(500, 50, -450, 30);

            // Assert
            machine.RunningTime.Should().Be(0);
        }

        [Fact]
        public void StoppingTime_ShouldBeNonNegative()
        {
            // Arrange
            var machine = new MachineOee("Test Machine", 60);

            // Act
            machine.SetInitialCondition(500, 50, 450, -30);

            // Assert
            machine.StoppingTime.Should().Be(0);
        }

        [Fact]
        public void ToString_ShouldReturnCorrectFormat()
        {
            // Arrange
            var machine = new MachineOee("Test Machine", 60);
            machine.SetInitialCondition(500, 50, 450, 30);

            // Act
            var result = machine.ToString();

            // Assert
            result.Should().Be(
                $"MachineOee: Test Machine\n" +
                $"OEE: {machine.Oee:F2}%\n" +
                $"Availability: {machine.Availability:F2}%\n" +
                $"Performance: {machine.Performance:F2}%\n" +
                $"Quality: {machine.Quality:F2}%\n" +
                $"Defective Rate: {machine.DefectiveRate:F2}\n" +
                $"Produced Pieces: {machine.ProducedPieces}\n" +
                $"Rejected Pieces: {machine.RejectedPieces}\n" +
                $"Running Time: {machine.RunningTime:F2} minutes\n" +
                $"Stopping Time: {machine.StoppingTime:F2} minutes");
        }
    }
}
