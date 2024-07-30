namespace IndFusion.Exxerpro.Domain.Models;

public static class RandomExtensions
{
    public static double NextDecimal(this Random random, int minValue, int maxValue)
    {
        return random.Next(minValue, maxValue + 1);
    }

    public static double SampleWithTendencyToTheRight(this Random random)
    {
        var alpha = 75.0;
        var beta = 5.0;
        return random.BetaSample(alpha, beta);
    }

    public static double SampleWithTendencyToTheLeft(this Random random)
    {
        var alpha = 5.0;
        var beta = 75.0;
        return random.BetaSample(alpha, beta);
    }

    private static double BetaSample(this Random random, double alpha, double beta)
    {
        var sampleAlpha = random.GammaSample(alpha, 1.0);
        var sampleBeta = random.GammaSample(beta, 1.0);
        return sampleAlpha / (sampleAlpha + sampleBeta);
    }

    private static double GammaSample(this Random random, double shape, double scale)
    {
        var d = shape - 1.0 / 3.0;
        var c = 1.0 / Math.Sqrt(9.0 * d);
        double v;

        do
        {
            double x, u;
            do
            {
                x = random.NormalSample();
                v = 1.0 + c * x;
            }
            while (v <= 0);

            v = v * v * v;
            u = random.NextDouble();

            if (u < 1.0 - 0.0331 * x * x * x * x || Math.Log(u) < 0.5 * x * x + d * (1.0 - v + Math.Log(v)))
                break;
        }
        while (true);

        return d * v * scale;
    }

    private static double NormalSample(this Random random)
    {
        var u1 = random.NextDouble();
        var u2 = random.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
    }
}



public static class StatisticalExtensions
{
    public static double ComputeMean(this double[] samples)
    {
        return samples.Average();
    }

    public static double ComputeStandardDeviation(this double[] samples)
    {
        var mean = samples.ComputeMean();
        return Math.Sqrt(samples.Sum(x => Math.Pow(x - mean, 2)) / samples.Length);
    }
    public static double ComputeStandardDeviation(this double[] samples, double mean)
    {
        return Math.Sqrt(samples.Sum(x => Math.Pow(x - mean, 2)) / samples.Length);
    }

    public static double ComputeMode(this double[] samples)
    {
        return samples.GroupBy(v => v)
                      .OrderByDescending(g => g.Count())
                      .First().Key;
    }

    public static double ComputeSkewness(this double[] samples, double mean, double stdDev)
    {

        return samples.Sum(x => Math.Pow((x - mean) / stdDev, 3)) / samples.Length;
    }
    public static double ComputeSkewness(this double[] samples)
    {
        var mean = samples.ComputeMean();
        var stdDev = samples.ComputeStandardDeviation();
        return samples.Sum(x => Math.Pow((x - mean) / stdDev, 3)) / samples.Length;
    }
    public static double ComputeKurtosis(this double[] samples, double mean, double stdDev)
    {

        var kurtosis = samples.Sum(x => Math.Pow((x - mean) / stdDev, 4)) / samples.Length;
        return kurtosis - 3; // Excess kurtosis
    }

    public static double ComputeKurtosis(this double[] samples)
    {
        var mean = samples.ComputeMean();
        var stdDev = samples.ComputeStandardDeviation();
        var kurtosis = samples.Sum(x => Math.Pow((x - mean) / stdDev, 4)) / samples.Length;
        return kurtosis - 3; // Excess kurtosis
    }
}