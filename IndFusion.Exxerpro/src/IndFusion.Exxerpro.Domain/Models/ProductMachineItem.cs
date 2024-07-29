namespace IndFusion.Exxerpro.Domain.Models;

public class ProductMachineItem(string name, string status, int indexInZone, int machineId, string sourceZoneIdentifier)
{
    public int IndexInZone { get; set; } = indexInZone;
    public string Name { get; init; } = name;
    public int MachineId { get; init; } = machineId;
    public string Status { get; set; } = status;

    public string SourceZoneIdentifier { get; set; } = sourceZoneIdentifier;
}