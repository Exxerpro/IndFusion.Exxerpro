namespace IndFusion.Exxerpro.Data
{
    public static class MachineOeeExtensions
    {
        private static double GenerateNegativeSkewedRandom(Random random)
        {
            double u = random.NextDouble();
            return Math.Pow(u, 2); // Skewed towards 0
        }

        private static double GeneratePositiveSkewedRandom(Random random)
        {
            double u = random.NextDouble();
            return Math.Sqrt(u); // Skewed towards 1
        }

        public static void MutateData(this MachineOee machine, Random random, DateTime startTime, DateTime endTime)
        {
            double newRunningFactor = GenerateNegativeSkewedRandom(random); // Generate negative skewed random between 0 and 1
            double newDefectiveRate = GeneratePositiveSkewedRandom(random); // Generate positive skewed random between 0 and 1
            int newProducedPieces = Math.Max(0, machine.ProducedPieces + random.Next(0, 20));

            machine.UpdateInfo(newProducedPieces, newRunningFactor, newDefectiveRate, startTime, endTime);
        }
    }
}