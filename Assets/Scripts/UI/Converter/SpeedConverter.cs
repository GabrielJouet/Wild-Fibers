public static class SpeedConverter
{
    private static readonly string veryFast = "V. Fast";

    private static readonly string fast = "Fast";

    private static readonly string average = "Average";

    private static readonly string slow = "Slow";

    private static readonly string verySlow = "V. Slow";



    /// <summary>
    /// Method used to convert value.
    /// </summary>
    /// <param name="speed">Float value to convert</param>
    /// <returns>Converted value</returns>
    public static string TransformSpeed(float speed)
    {
        if (speed <= 0.1f)
            return verySlow;
        else if (0.1f < speed && speed <= 0.2f)
            return slow;
        else if (0.2f < speed && speed <= 0.3f)
            return average;
        else if (0.3f < speed && speed <= 0.4f)
            return fast;
        else if (speed > 0.4f)
            return veryFast;
        else
            return "Unknown";
    }
}
