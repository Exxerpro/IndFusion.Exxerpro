using FluentAssertions;
using IndFusion.Exxerpro.Models;
using Xunit;
using Xunit.Abstractions;

namespace IndFusion.ExxerproTests.Models;

    public class SkewedRandomGeneratorTests(ITestOutputHelper output)
    {
        private readonly ITestOutputHelper _output = output;

        [Theory]
        [InlineData(10000, 0.5)]
        public void SampleWithTendencyToTheRight_ShouldHaveMeanGreaterThanHalf(int sampleSize, double expectedMean)
        {
            var random = new Random();
            var samples = new double[sampleSize];
            for (int i = 0; i < sampleSize; i++)
            {
                samples[i] = MachineOeeExtensions.SampleWithTendencyToTheRight(random);
            }

            double mean = ComputeMean(samples);
            double stdDev = ComputeStandardDeviation(samples, mean);
            double mode = ComputeMode(samples);
            double skewness = ComputeSkewness(samples, mean, stdDev);
            double kurtosis = ComputeKurtosis(samples, mean, stdDev);

            _output.WriteLine($"Mean: {mean}");
            _output.WriteLine($"Standard Deviation: {stdDev}");
            _output.WriteLine($"Mode: {mode}");
            _output.WriteLine($"Skewness: {skewness}");
            _output.WriteLine($"Kurtosis: {kurtosis}");

            mean.Should().BeGreaterThan(expectedMean, $"because the mean was {mean}");
        }

        [Theory]
        [InlineData(10000, 0.5)]
        public void SampleWithTendencyToTheLeft_ShouldHaveMeanLesserThanHalf(int sampleSize, double expectedMean)
        {
            var random = new Random();
            var samples = new double[sampleSize];
            for (int i = 0; i < sampleSize; i++)
            {
                samples[i] = MachineOeeExtensions.SampleWithTendencyToTheLeft(random);
            }

            double mean = ComputeMean(samples);
            double stdDev = ComputeStandardDeviation(samples, mean);
            double mode = ComputeMode(samples);
            double skewness = ComputeSkewness(samples, mean, stdDev);
            double kurtosis = ComputeKurtosis(samples, mean, stdDev);

            _output.WriteLine($"Mean: {mean}");
            _output.WriteLine($"Standard Deviation: {stdDev}");
            _output.WriteLine($"Mode: {mode}");
            _output.WriteLine($"Skewness: {skewness}");
            _output.WriteLine($"Kurtosis: {kurtosis}");

            mean.Should().BeLessThan(expectedMean, $"because the mean was {mean}");
        }

        private double ComputeMean(double[] samples)
        {
            double sum = 0;
            foreach (var sample in samples)
            {
                sum += sample;
            }
            return sum / samples.Length;
        }

        private double ComputeStandardDeviation(double[] samples, double mean)
        {
            double sumOfSquares = 0;
            foreach (var sample in samples)
            {
                sumOfSquares += Math.Pow(sample - mean, 2);
            }
            return Math.Sqrt(sumOfSquares / samples.Length);
        }

        private double ComputeMode(double[] samples)
        {
            return samples.GroupBy(v => v)
                .OrderByDescending(g => g.Count())
                .First()
                .Key;
        }

        private double ComputeSkewness(double[] samples, double mean, double stdDev)
        {
            double skewness = 0;
            foreach (var sample in samples)
            {
                skewness += Math.Pow((sample - mean) / stdDev, 3);
            }
            return skewness / samples.Length;
        }

        private double ComputeKurtosis(double[] samples, double mean, double stdDev)
        {
            double kurtosis = 0;
            foreach (var sample in samples)
            {
                kurtosis += Math.Pow((sample - mean) / stdDev, 4);
            }
            return kurtosis / samples.Length - 3; // Excess kurtosis
        }
    }