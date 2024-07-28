namespace IndFusion.Exxerpro.Models;

public class PerformanceData
{
    public ProductionData ProductionData { get; private set; } = new();

    public List<ProductionData> HistoricData { get; private set; } = new();

    public string Name { get; private set; }
    public string Description { get; private set; }
    public double Capacity { get; private set; }
    public PerformanceIndicator Indicator { get; private set; }

    public PerformanceData(string name, string description, double capacity)
    {
        Name = name;
        Description = description;
        Capacity = capacity;
        Indicator = new PerformanceIndicator(ProductionData, capacity);
    }


    public List<PerformanceIndicator> HistoricIndicator => HistoricData.Select(data => new PerformanceIndicator(data, Capacity)).ToList();


    public void UpdateData(ProductionData productionData)
    {
        ProductionData.AddProductionData(productionData);
        HistoricData.Add(productionData);
    }
}