namespace IndFusion.Exxerpro.Models
{
    public class MachineOee
    {
        public MachineOee(string name, double capacity)
        {
            Name = name;
            Capacity = capacity;
        }

        public MachineOee(string name) : this(name, 0)
        {
        }

        public string Name { get; private set; }
        public double Capacity { get; private set; } // in pieces per minute

        private double _defectiveRate;
        private int _producedPieces;
        private double _runningTime; // in minutes
        private double _stoppingTime; // in minutes
        private int _rejectedPieces;

        public double DefectiveRate
        {
            get => ProducedPieces > 0 ? (double)RejectedPieces / ProducedPieces : 0;
            private set => _defectiveRate = Math.Max(0, Math.Min(value, 1)); // between 0 and 1
        }

        public int ProducedPieces
        {
            get => _producedPieces;
            private set => _producedPieces = Math.Max(0, value);
        }

        public int RejectedPieces
        {
            get => _rejectedPieces;
            private set => _rejectedPieces = Math.Max(0, value);
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

        public double Availability => RunningTime + StoppingTime > 0
            ? EnsurePercentageRange((RunningTime / (RunningTime + StoppingTime)) * 100)
            : 0;

        public double Performance => Capacity > 0
            ? EnsurePercentageRange((ProducedPieces / (RunningTime * Capacity)) * 100)
            : 0;

        public double Quality => ProducedPieces > 0
            ? EnsurePercentageRange(((ProducedPieces - RejectedPieces) / (double)ProducedPieces) * 100)
            : 0;

        public double Oee => EnsurePercentageRange((Availability * Performance * Quality) / 10_000);

        public void SetInitialCondition(int producedPieces, int rejectedPieces, double runningTime, double stoppingTime)
        {
            ProducedPieces = producedPieces;
            RejectedPieces = rejectedPieces;
            RunningTime = runningTime;
            StoppingTime = stoppingTime;
        }

        public void UpdateInfo(int producedPieces, int rejectedPieces, double runningTime, double stoppingTime)
        {
            ProducedPieces += producedPieces;
            RejectedPieces += rejectedPieces;
            RunningTime += runningTime;
            StoppingTime += stoppingTime;
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
                   $"Rejected Pieces: {RejectedPieces}\n" +
                   $"Running Time: {RunningTime:F2} minutes\n" +
                   $"Stopping Time: {StoppingTime:F2} minutes";
        }
    }
}
