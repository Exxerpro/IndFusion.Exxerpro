namespace IndFusion.Exxerpro.Data;

public static class RandomExtensions
{
    public static double NextDecimal(this Random random, int minValue, int maxValue)
    {
        return random.Next(minValue, maxValue + 1);
    }
}