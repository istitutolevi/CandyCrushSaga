namespace CandyCrushSaga.Utilities
{
    internal sealed class Math
    {
        internal static float GetPercentage(float percent, float value)
        {
            return (percent * value) / 100f;
        }
        internal static float GetPercent(float subvalue, float value)
        {
            if (value == 0f) return 0f;
            return (subvalue * 100f) / value;
        }

        internal static double GetPercentage(double percent, double value)
        {
            return (percent * value) / 100f;
        }
        internal static double GetPercent(double subvalue, double value)
        {
            if (value == 0f) return 0f;
            return (subvalue * 100f) / value;
        }
    }
}
