namespace IndFusion.Exxerpro.Data
{
    public class MachineOee(string name)
    {
        public string Name { get; set; } = name;
        public double Oee { get; private set; }
        public double Availability { get; private set; }
        public double Performance { get; private set; }
        public double Quality { get; private set; }
        private double _defectiveRate;
        private int _producedPieces;
        private double _runningTime; // in minutes
        private double _stoppingTime; // in minutes
        public double Capacity  { get; private set; }// in pieces per minute

        public double DefectiveRate
        {
            get => _defectiveRate;
            private set => _defectiveRate = Math.Max(0, Math.Min(value, 1)); // between 0 and 1
        }

        public int DefectivePieces
        {
            get
            {
                // Defensive programming and boundary checking
                if (ProducedPieces < 0)
                {
                    throw new InvalidOperationException("Produced pieces cannot be negative.");
                }

                if (DefectiveRate < 0 || DefectiveRate > 1)
                {
                    throw new InvalidOperationException("Defective rate must be between 0 and 1.");
                }

                return (int)(ProducedPieces * DefectiveRate / (1 - DefectiveRate));
            }
        }
        public int ProducedPieces
        {
            get => _producedPieces;
            private set => _producedPieces = Math.Max(0, value);
        }

        public double RunningTime
        {
            get => _runningTime;
            private set => _runningTime = Math.Max(0, value); // non-negative
        }

        public double StoppingTime
        {
            get => _stoppingTime;
            private set => _stoppingTime = Math.Max(0, value); // non-negative
        }

        public void SetInitialCondition(double oee, double availability, double performance, double quality, double defectiveRate, int producedPieces, double runningTime, double stoppingTime, double capacity)
        {
            Oee = oee;
            Availability = availability;
            Performance = performance;
            Quality = quality;
            DefectiveRate = defectiveRate;
            ProducedPieces = producedPieces;
            RunningTime = runningTime;
            StoppingTime = stoppingTime;
            Capacity = capacity;
        }

        public void UpdateInfo(int producedPieces, double runningFactor, double defectiveRate, DateTime startTime, DateTime endTime)
        {
            ProducedPieces = producedPieces;
            DefectiveRate = defectiveRate;
            CalculateTime(startTime, endTime, runningFactor);
            CalculateMetrics();
        }

        private void CalculateTime(DateTime startTime, DateTime endTime, double runningFactor)
        {
            var totalMinutes = (endTime - startTime).TotalMinutes;
            RunningTime = totalMinutes * runningFactor;
            StoppingTime = totalMinutes - RunningTime;
        }

        private void CalculateMetrics()
        {
            if (RunningTime + StoppingTime > 0)
            {
                Availability = EnsurePercentageRange((RunningTime / (RunningTime + StoppingTime)) * 100);
            }
            else
            {
                Availability = 0;
            }

            Performance = Capacity > 0 ? EnsurePercentageRange((ProducedPieces / (RunningTime * Capacity)) * 100) : 0;
            Quality = ProducedPieces > 0 ? EnsurePercentageRange(((ProducedPieces * (1 - DefectiveRate)) / ProducedPieces) * 100) : 0;

            Oee = (Availability * Performance * Quality) / 10_000;
            Oee = EnsurePercentageRange(Oee);
        }
        private double EnsurePercentageRange(double value)
        {
            return Math.Max(0, Math.Min(value, 100));
        }
        public override string ToString()
        {
            return $"MachineOee: {Name}\n" +
                   $"OEE: {Oee:F2}%\n" +
                   $"Availability: {Availability:F2}%\n" +
                   $"Performance: {Performance:F2}%\n" +
                   $"Quality: {Quality:F2}%\n" +
                   $"Defective Rate: {DefectiveRate:F2}\n" +
                   $"Produced Pieces: {ProducedPieces}\n" +
                   $"Running Time: {RunningTime:F2} minutes\n" +
                   $"Stopping Time: {StoppingTime:F2} minutes";
        }
    }
}
