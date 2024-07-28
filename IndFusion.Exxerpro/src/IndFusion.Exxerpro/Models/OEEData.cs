namespace IndFusion.Exxerpro.Models;

public class OeeData
{
    public DateTime Timestamp { get; set; }
    public List<MachineOee> Machines { get; set; }

    public override string ToString()
    {
        return $"Timestamp: {Timestamp}, Machines: {string.Join(", ", Machines)}";
    }
}