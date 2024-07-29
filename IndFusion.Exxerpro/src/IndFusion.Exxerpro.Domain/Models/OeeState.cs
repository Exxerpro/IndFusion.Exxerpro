namespace IndFusion.Exxerpro.Domain.Models
{
    public class OeeState
    {

        private readonly Random _random = new();
        private readonly IDateTimeMachine _dateTimeMachine;
        private DateTime _startTime;

        public event Action OnChange;
        public Dictionary<string, PerformanceData> Machines { get; private set; } = new();

        public OeeState(IDateTimeMachine dateTimeMachine)
        {

            _dateTimeMachine = dateTimeMachine;
            _startTime = _dateTimeMachine.Now.AddHours(-8);

            // Initialize machines with default data
            InitializeMachines();

            // Generate initial data points for the past 8 hours

        }

        private void InitializeMachines()
        {
            // Add initial machines to HistoricalData with empty lists
            Machines["Power Puncher"] = new PerformanceData("Power Puncher", "Description", 30);
            Machines["Press Power"] = new PerformanceData("Press Power", "Description", 200);
            Machines["Cross Cutter"] = new PerformanceData("Cross Cutter", "Description", 100);
            Machines["Crosswise Cutter"] = new PerformanceData("Crosswise Cutter", "Description", 300);
            Machines["Press Titan"] = new PerformanceData("Press Titan", "Description", 100);

            GeneratePastData();
        }

        public void GenerateNewData()
        {
            foreach (var machine in Machines.Values)
            {
                var productionData = machine.GenerateNewData(_random, _startTime, _dateTimeMachine.Now);
                machine.UpdateData(productionData);
            }
        }

        public void GeneratePastData()
        {
            foreach (var machine in Machines.Values)
            {
                machine.GeneratePastData(_random, _startTime, _dateTimeMachine.Now);
            }
        }



    }
}
