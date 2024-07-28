namespace IndFusion.Exxerpro.Models
{
    public interface IMachineOee
    {
        double Availability { get; }
        double Capacity { get; }
        double DefectiveRate { get; }
        string Name { get; }
        double Oee { get; }
        double Performance { get; }
        double ProducedPieces { get; }
        double Quality { get; }
        double RejectedPieces { get; }

        double RunningTime
        {
            get;
            // non-negative
        }

        double StoppingTime
        {
            get;
            // non-negative
        }

        void SetInitialCondition(int producedPieces, int rejectedPieces, double runningTime, double stoppingTime);
        string ToString();
        void UpdateInfo(int producedPieces, int rejectedPieces, double runningTime, double stoppingTime);
    }

    public class MachineOee(string name, double capacity) : IMachineOee
    {
        
        private double _producedPieces;

        private double _rejectedPieces;

        private double _runningTime;

        private double _stoppingTime;

        public MachineOee(string name) : this(name, 0)
        {
        }

        public double Availability => RunningTime + StoppingTime > 0
            ? EnsurePercentageRange((RunningTime / (RunningTime + StoppingTime)) * 100)
            : 0;

        public double Capacity { get; private set; } = capacity;
        // in pieces per minute
        public double DefectiveRate
        {
            get => ProducedPieces > 0 ? (double)RejectedPieces / ProducedPieces : 0;
            
        }

        public string Name { get; private set; } = name;
        public double Oee => EnsurePercentageRange((Availability * Performance * Quality) / 10_000);

        public double Performance => Capacity > 0
            ? EnsurePercentageRange((ProducedPieces / (RunningTime * Capacity)) * 100)
            : 0;

        public double ProducedPieces
        {
            get => _producedPieces;
            private set => _producedPieces = Math.Max(0, value);
        }

        public double Quality => ProducedPieces > 0
            ? EnsurePercentageRange(((ProducedPieces - RejectedPieces) / (double)ProducedPieces) * 100)
            : 0;

        public double RejectedPieces
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
        public void SetInitialCondition(int producedPieces, int rejectedPieces, double runningTime, double stoppingTime)
        {
            ProducedPieces = producedPieces;
            RejectedPieces = rejectedPieces;
            RunningTime = runningTime;
            StoppingTime = stoppingTime;
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
    }
}
