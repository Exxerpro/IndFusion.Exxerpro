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
            var machineNames = new List<string>
            {
                "Power Puncher",
                "Press Power",
                "Cross Cutter",
                "Crosswise Cutter",
                "Press Titan",
                "Laser Cutter",
                "Hydraulic Press",
                "Automatic Feeder",
                "Conveyor Belt",
                "Packaging Robot"
            };

            for (int i = 0; i < machineNames.Count; i++)
            {
                var machine = new Machine(
                    machineId: i + 1,
                    machineName: machineNames[i],
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
