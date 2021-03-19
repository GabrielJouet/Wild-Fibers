public static class DotConverter
{
    private static readonly string veryHigh = "V. High";

    private static readonly string high = "High";

    private static readonly string average = "Average";

    private static readonly string small = "Small";

    private static readonly string verySmall = "V. Small";

    private static readonly string none = "None";



    /// <summary>
    /// Method used to convert value.
    /// </summary>
    /// <param name="dot">Float value to convert</param>
    /// <returns>Converted value</returns>
    public static string TransformDot(float dot)
    {
        if (dot <= 0)
            return none;
        else if (0 < dot && dot <= 10)
            return verySmall;
        else if (10 < dot && dot <= 25)
            return small;
        else if (25 < dot && dot <= 55)
            return average;
        else if (55 < dot && dot <= 80)
            return high;
        else if (dot > 80)
            return veryHigh;
        else
            return "Unknown";
    }
}
