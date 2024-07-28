namespace IndFusion.Exxerpro.Domain.Models;

public class ProductionData(
    double producedPieces,
    double rejectedPieces,
    double runningTime,
    double stoppingTime,
    DateTime startTime,
    DateTime endTime)
{
    public ProductionData() : this(0, 0, 0, 0, DateTime.MaxValue, DateTime.MinValue) { }

    public double ProducedPieces { get; private set; } = Math.Max(0, producedPieces);
    public double RejectedPieces { get; private set; } = Math.Max(0, rejectedPieces);
    public double RunningTime { get; private set; } = Math.Max(0, runningTime);
    public double StoppingTime { get; private set; } = Math.Max(0, stoppingTime);
    public DateTime StartTime { get; private set; } = startTime;
    public DateTime EndTime { get; private set; } = endTime;

    public void AddProductionData(ProductionData productionData)
    {
        if (productionData == null)
            throw new ArgumentNullException(nameof(productionData), "Production data cannot be null.");

        if (productionData.ProducedPieces < 0 || productionData.RejectedPieces < 0 || productionData.RunningTime < 0 || productionData.StoppingTime < 0)
            throw new ArgumentOutOfRangeException(nameof(productionData), "Production data values cannot be negative.");

        ProducedPieces += productionData.ProducedPieces;
        RejectedPieces += productionData.RejectedPieces;
        RunningTime += productionData.RunningTime;
        StoppingTime += productionData.StoppingTime;

        if (productionData.StartTime < StartTime)
        {
            StartTime = productionData.StartTime;
        }

        if (productionData.EndTime > EndTime)
        {
            EndTime = productionData.EndTime;
        }
    }

    public static ProductionData operator +(ProductionData pd1, ProductionData pd2)
    {
        return new ProductionData(
            pd1.ProducedPieces + pd2.ProducedPieces,
            pd1.RejectedPieces + pd2.RejectedPieces,
            pd1.RunningTime + pd2.RunningTime,
            pd1.StoppingTime + pd2.StoppingTime,
            pd1.StartTime < pd2.StartTime ? pd1.StartTime : pd2.StartTime,
            pd1.EndTime > pd2.EndTime ? pd1.EndTime : pd2.EndTime
        );
    }

    public override string ToString()
    {
        return $"StartTime: {StartTime}, EndTime: {EndTime}, ProducedPieces: {ProducedPieces}, RejectedPieces: {RejectedPieces}, RunningTime: {RunningTime} minutes, StoppingTime: {StoppingTime} minutes";
    }
}