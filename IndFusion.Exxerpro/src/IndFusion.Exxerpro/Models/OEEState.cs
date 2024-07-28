using System;
using System.Collections.Generic;
using System.Linq;

namespace IndFusion.Exxerpro.Models
{
    public class OeeState
    {
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

        public OeeState(IDateTimeMachine dateTimeMachine)
        {
            _dateTimeMachine = dateTimeMachine;
            _startTime = _dateTimeMachine.Now.AddHours(-8);
        // Generate initial data points for the past 8 hours
            GenerateInitialDataPoints();
        }

        private void GenerateInitialDataPoints()
        {
            _startTime = _dateTimeMachine.Now.AddHours(-8);
            var historicTime = _dateTimeMachine.Now.AddHours(-8);
            var now = _dateTimeMachine.Now;

            while (_startTime <= now)
            {
                var dataPoint = GenerateNewDataPoint(historicTime);
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
