namespace IndFusion.Exxerpro.Models;

public class PerformanceIndicator(ProductionData productionData, double capacity)
{
    private readonly ProductionData _productionData = productionData ?? throw new ArgumentNullException(nameof(productionData));

    public double Oee => CalculatePercentage(CalculateOee);

    public double Availability => CalculatePercentage(CalculateAvailability);

    public double Performance => CalculatePercentage(CalculatePerformance);

    public double Quality => CalculatePercentage(CalculateQuality);

    private double CalculateOee()
    {
        var availability = CalculateAvailability();
        var performance = CalculatePerformance();
        var quality = CalculateQuality();

        return availability * performance * quality / 10_000;
    }

    private double CalculateAvailability()
    {
        return _productionData.RunningTime + _productionData.StoppingTime > 0
            ? (_productionData.RunningTime / (_productionData.RunningTime + _productionData.StoppingTime)) * 100
            : 0;
    }

    private double CalculatePerformance()
    {
        return _productionData.RunningTime > 0
            ? (_productionData.ProducedPieces / (_productionData.RunningTime * capacity)) * 100
            : 0;
    }

    private double CalculateQuality()
    {
        return _productionData.ProducedPieces > 0
            ? ((_productionData.ProducedPieces - _productionData.RejectedPieces) / _productionData.ProducedPieces) * 100
            : 0;
    }

    private double CalculatePercentage(Func<double> calculationMethod)
    {
        return EnsurePercentageRange(calculationMethod());
    }

    private double EnsurePercentageRange(double value)
    {
        return Math.Max(0, Math.Min(value, 100));
    }
}