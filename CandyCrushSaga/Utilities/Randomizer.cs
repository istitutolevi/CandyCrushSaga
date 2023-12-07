using System;

namespace CandyCrushSaga.Utilities
{
    internal static class Randomizer
    {
        static Random Rand = new Random();
        
        internal static int Next()
        {
            return Rand.Next();
        }
        internal static int Next(int maxValue)
        {
            return Rand.Next(maxValue);
        }
        internal static int Next(int minValue, int maxValue)
        {
            return Rand.Next(minValue, maxValue);
        }
        internal static void NextBytes(byte[] buffer)
        {
            Rand.NextBytes(buffer);
        }
        internal static double NextDouble()
        {
            return Rand.NextDouble();
        }
    }
}
