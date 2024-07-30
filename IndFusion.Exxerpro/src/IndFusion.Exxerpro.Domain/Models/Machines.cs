namespace IndFusion.Exxerpro.Domain.Models
{
    public class Machine
    {
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public string Status { get; set; }
        public DateTime LastMaintenanceDate { get; set; }

        // Constructor
        public Machine(int machineId, string machineName, string status, DateTime lastMaintenanceDate)
        {
            MachineId = machineId;
            MachineName = machineName;
            Status = status;
            LastMaintenanceDate = lastMaintenanceDate;
        }

        // Method to generate bogus data
        public static List<Machine> GenerateBogusData(int numberOfMachines)
        {
            var machines = new List<Machine>();
            var random = new Random();
            var statuses = new List<string> { "Running", "Stopped", "Maintenance", "Idle" };

            for (int i = 1; i <= numberOfMachines; i++)
            {
                var machine = new Machine(
                    machineId: i,
                    machineName: $"Machine_{i}",
                    status: statuses[random.Next(statuses.Count)],
                    lastMaintenanceDate: DateTime.Now.AddDays(-random.Next(30))
                );
                machines.Add(machine);
            }

            return machines;
        }

        // Override ToString method for easy display
        public override string ToString()
        {
            return $"ID: {MachineId}, Name: {MachineName}, Status: {Status}, Last Maintenance: {LastMaintenanceDate.ToShortDateString()}";
        }
    }

}
