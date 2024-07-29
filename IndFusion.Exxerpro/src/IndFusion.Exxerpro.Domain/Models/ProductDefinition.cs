namespace IndFusion.Exxerpro.Domain.Models
{

    public class ProductDefinition(string name, bool newMachine, string newMachineName)
    {
        public string Name { get; init; } = name;
        public bool NewMachine { get; set; } = newMachine;
        public string NewMachineName { get; set; } = newMachineName;
    }
}
