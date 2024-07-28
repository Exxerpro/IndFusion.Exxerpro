namespace IndFusion.Exxerpro.Models;

public class MachineMetrics(double oee, double availability, double performance, double quality)
{
    public double Oee { get; set; } = oee;
    public double Availability { get; set; } = availability;
    public double Performance { get; set; } = performance;
    public double Quality { get; set; } = quality;

    public override string ToString()
    {
        return $"OEE: {Oee:F2}%, Availability: {Availability:F2}%, Performance: {Performance:F2}%, Quality: {Quality:F2}%";
    }
}