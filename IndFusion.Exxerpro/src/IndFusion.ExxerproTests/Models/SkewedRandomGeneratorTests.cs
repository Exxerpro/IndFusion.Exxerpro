using FluentAssertions;
using IndFusion.Exxerpro.Domain.Models;
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
            samples[i] = random.SampleWithTendencyToTheRight();
        }

        double mean = samples.ComputeMean();
        double stdDev = samples.ComputeStandardDeviation(mean);
        double mode = samples.ComputeMode();
        double skewness = samples.ComputeSkewness(mean, stdDev);
        double kurtosis = samples.ComputeKurtosis(mean, stdDev);

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
            samples[i] = random.SampleWithTendencyToTheLeft();
        }

        double mean = samples.ComputeMean();
        double stdDev = samples.ComputeStandardDeviation(mean);
        double mode = samples.ComputeMode();
        double skewness = samples.ComputeSkewness(mean, stdDev);
        double kurtosis = samples.ComputeKurtosis(mean, stdDev);

        _output.WriteLine($"Mean: {mean}");
        _output.WriteLine($"Standard Deviation: {stdDev}");
        _output.WriteLine($"Mode: {mode}");
        _output.WriteLine($"Skewness: {skewness}");
        _output.WriteLine($"Kurtosis: {kurtosis}");

        mean.Should().BeLessThan(expectedMean, $"because the mean was {mean}");
    }


}