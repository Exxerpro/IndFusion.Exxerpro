namespace IndFusion.Exxerpro.Domain.Models.Mahts;
public class AdvancedTrapezoidFunction(double startRise, double endRise, double startFall, double endFall, double peakValue, double frequency, double amplitude)
{
    private readonly double startRise = startRise;
    private readonly double endRise = endRise;
    private readonly double startFall = startFall;
    private readonly double endFall = endFall;
    private readonly double peakValue = peakValue;
    private readonly double frequency = frequency;
    private readonly double amplitude = amplitude;

    private double LogisticRise(double x)
    {
        double midPoint = (startRise + endRise) / 2;
        double growthRate = 10 / (endRise - startRise);
        return peakValue / (1 + Math.Exp(-growthRate * (x - midPoint)));
    }

    private double LogisticFall(double x)
    {
        double midPoint = (startFall + endFall) / 2;
        double decayRate = 10 / (endFall - startFall);
        return peakValue * (1 - 1 / (1 + Math.Exp(-decayRate * (x - midPoint))));
    }

    private double SinusoidalNoise(double x)
    {
        return amplitude * Math.Sin(2 * Math.PI * frequency * x);
    }

    public double Evaluate(double x)
    {
        if (x < startRise)
        {
            return 0;
        }
        else if (x < endRise)
        {
            return LogisticRise(x);
        }
        else if (x < startFall)
        {
            // Adding sinusoidal variations during the stable phase
            return peakValue + SinusoidalNoise(x);
        }
        else if (x < endFall)
        {
            return LogisticFall(x);
        }
        else
        {
            return 0;
        }
    }
}
