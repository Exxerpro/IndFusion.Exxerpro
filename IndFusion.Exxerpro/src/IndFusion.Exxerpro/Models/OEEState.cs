using System;
using System.Collections.Generic;
using System.Linq;

namespace IndFusion.Exxerpro.Models
{
    public class OeeState
    {
        private readonly ILogger<OeeState> _logger;

        private readonly Random _random = new();
        private readonly IDateTimeMachine _dateTimeMachine;
        private DateTime _startTime;

        public event Action OnChange;
        public List<OeeData> HistoricalData { get; private set; } = [];

        public List<MachineOee> Machines =
        [
            new("Power Puncher", 30),
            new MachineOee("Press Power",200),
            new MachineOee("Cross Cutter",100),
            new MachineOee("Crosswise Cutter",300),
            new MachineOee("Press Titan",100)
        ];

        public OeeState(IDateTimeMachine dateTimeMachine, ILogger<OeeState> logger= null)
        {
            this._logger = logger;

            _dateTimeMachine = new DateTimeMachine();
            _startTime = _dateTimeMachine.Now.AddHours(-8);
        // Generate initial data points for the past 8 hours
            GenerateInitialDataPoints();
        }

        public void GenerateInitialDataPoints()
        {
         
            var historicTime = _startTime.AddMinutes(5);
            var now = _dateTimeMachine.Now;

            while (_startTime <= now)
            {
                var dataPoint = GenerateNewDataPoint(historicTime);

                if(_logger is not null)
                {
                    _logger.LogInformation("Generated data point at {DataPoint}", dataPoint);
                }

                HistoricalData.Add(dataPoint);
                historicTime = historicTime.AddMinutes(5);
            }

            NotifyStateChanged();
        }

        public void UpdateData(OeeData newData)
        {
            HistoricalData.Add(newData);
            HistoricalData = HistoricalData.Where(data => data.Timestamp >= _dateTimeMachine.Now.AddHours(-8)).ToList();
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void ExportHistoricalDataToCsv(string filePath)
        {
            HistoricalData.ExportToCsv(filePath);
        }
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

        public IEnumerable<(DateTime Timestamp, double Oee, double Availability, double Performance, double Quality)> GetMachineHistoricalData(string machineName)
        {
            return HistoricalData
                .Select(data => new
                {
                    data.Timestamp,
                    Machine = data.Machines.FirstOrDefault(machine => machine.Name == machineName)
                })
                .Where(x => x.Machine != null)
                .Select(x => (x.Timestamp, x.Machine.Oee, x.Machine.Availability, x.Machine.Performance, x.Machine.Quality));
        }
    }
}
