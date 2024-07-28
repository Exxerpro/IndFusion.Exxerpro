namespace IndFusion.Exxerpro.Models
{
    public class OeeState
    {
        private readonly Random _random = new();

        public event Action OnChange;
        public List<OeeData> HistoricalData { get; private set; } = [];

        public List<MachineOee> Machines =
        [
            new("Power Puncher"),
            new MachineOee("Press Power"),
            new MachineOee("Cross Cutter"),
            new MachineOee("Crosswise Cutter"),
            new MachineOee("Press Titan")
        ];

        public OeeState()
        {
            Machines[0].SetInitialCondition(85, 90, 80, 95, 0.02, 500, 450, 30, 60);
            Machines[1].SetInitialCondition(78, 85, 75, 90, 0.04, 480, 420, 30, 60);
            Machines[2].SetInitialCondition(82, 88, 78, 94, 0.03, 490, 430, 30, 60);
            Machines[3].SetInitialCondition(80, 87, 76, 93, 0.05, 495, 440, 30, 60);
            Machines[4].SetInitialCondition(84, 89, 79, 92, 0.01, 500, 450, 30, 60);
        }

        private DateTime _startTime = DateTime.Now;

        public void UpdateData(OeeData newData)
        {
            HistoricalData.Add(newData);
            HistoricalData = HistoricalData.Where(data => data.Timestamp >= DateTime.Now.AddHours(-8)).ToList();
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        public OeeData GenerateNewDataPoint(DateTime now)
        {
            foreach (var machine in Machines)
            {
                machine.MutateData(_random, _startTime, now);
            }
            _startTime = now; // Update the start time for the next interval
            var oeeData = new OeeData { Timestamp = now, Machines = Machines };
            return oeeData;
        }
    }
}
