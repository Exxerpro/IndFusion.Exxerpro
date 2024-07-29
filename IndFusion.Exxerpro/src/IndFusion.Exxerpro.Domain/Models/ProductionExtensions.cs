namespace IndFusion.Exxerpro.Domain.Models;

public static class ProductionExtensions
{
    public static ProductionData GenerateNewData(this PerformanceData machine, Random random, DateTime startTime, DateTime endTime)
    {
        double runningFactor = RandomExtensions.SampleWithTendencyToTheRight(random); // Generate negative skewed random between 0 and 1
        double defectiveRate = RandomExtensions.SampleWithTendencyToTheLeft(random); // Generate positive skewed random between 0 and 1
        double productiveFactor = RandomExtensions.SampleWithTendencyToTheRight(random); // Generate negative skewed random between 0 and 1

        var producedPieces = (int)(machine.Capacity * runningFactor * productiveFactor * (endTime - startTime).TotalMinutes);
        var defectivePieces = (int)(producedPieces * defectiveRate);

        var runningTime = (endTime - startTime).TotalMinutes * runningFactor;
        var stoppingTime = (endTime - startTime).TotalMinutes * (1 - runningFactor);


        var productionData = new ProductionData(producedPieces, defectivePieces, runningTime, stoppingTime, startTime, endTime);
        return productionData;


    }

    public static void GeneratePastData(this PerformanceData machine, Random random, DateTime startTime, DateTime endTime)
    {

        var currentTime = startTime;
        while (currentTime < endTime)
        {
            var nextTime = currentTime.AddMinutes(5);
            var productionData = machine.GenerateNewData(random, currentTime, nextTime);
            machine.UpdateData(productionData);
            currentTime = nextTime;
        }
    }
}