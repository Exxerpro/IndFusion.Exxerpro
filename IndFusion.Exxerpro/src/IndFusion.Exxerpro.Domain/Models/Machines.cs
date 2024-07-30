namespace IndFusion.Exxerpro.Domain.Models
{
    public class Machine(int machineId, string machineName, string imageName, string status, DateTime lastMaintenanceDate)
    {
        public int MachineId { get; set; } = machineId;
        public string MachineName { get; set; } = machineName;
        public string Status { get; set; } = status;
        public string ImageName { get; set; } = imageName;
        public DateTime LastMaintenanceDate { get; set; } = lastMaintenanceDate;

        // Constructor

        // Method to generate bogus data
        public static List<Machine> GenerateBogusData(int numberOfMachines)
        {
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

            return machineNames.Select((t, i) => new Machine(machineId: i + 1, machineName: t, imageName: "/img/machine" + i.ToString("D1") + ".png", status: statuses[random.Next(statuses.Count)], lastMaintenanceDate: DateTime.Now.AddDays(-random.Next(30)))).ToList();
        }
        // Override ToString method for easy display
        public override string ToString()
        {
            return $"ID: {MachineId}, Name: {MachineName}, Status: {Status}, Last Maintenance: {LastMaintenanceDate.ToShortDateString()}";
        }
    }

}
