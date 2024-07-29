namespace IndFusion.Exxerpro.Domain.Models;

public static class RandomExtensions
{

    public static double NextDecimal(this Random random, int minValue, int maxValue)
    {
        return random.Next(minValue, maxValue + 1);
    }
    public static double SampleWithTendencyToTheRight(Random random)
    {
        // Beta distribution parameters for stronger negative skewness with positive kurtosis
        var alpha = 75.0;
        var beta = 5.0;


        return BetaSample(random, alpha, beta);
    }

    public static double SampleWithTendencyToTheLeft(Random random)
    {
        // Beta distribution parameters for stronger positive skewness with positive kurtosis
        var alpha = 5.0;
        var beta = 75.0;

        return BetaSample(random, alpha, beta);
    }

    // Function to sample from a Beta distribution using the provided alpha and beta parameters
    private static double BetaSample(Random random, double alpha, double beta)
    {
        // Using the Gamma distribution to sample from the Beta distribution
        var sampleAlpha = GammaSample(random, alpha, 1.0);
        var sampleBeta = GammaSample(random, beta, 1.0);
        return sampleAlpha / (sampleAlpha + sampleBeta);
    }

    // Function to sample from a Gamma distribution using the provided shape and scale parameters
    private static double GammaSample(Random random, double shape, double scale)
    {
        // Implementation of Marsaglia and Tsang's method for Gamma(α,1)
        if (shape < 1.0)
        {
            shape += 1.0;
        }

        var d = shape - 1.0 / 3.0;
        var c = 1.0 / Math.Sqrt(9.0 * d);
        double v;

        while (true)
        {
            double x;
            do
            {
                x = NormalSample(random);
                v = 1.0 + c * x;
            }
            while (v <= 0.0);

            v = v * v * v;
            var u = random.NextDouble();

            if (u < 1.0 - 0.0331 * x * x * x * x)
            {
                break;
            }

            if (Math.Log(u) < 0.5 * x * x + d * (1.0 - v + Math.Log(v)))
            {
                break;
            }
        }

        return d * v * scale;
    }

    // Function to sample from a standard normal distribution
    private static double NormalSample(Random random)
    {
        // Using Box-Muller transform to generate a standard normal sample
        var u1 = random.NextDouble();
        var u2 = random.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
    }
}