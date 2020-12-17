/// <summary>
/// Class used to convert armor through value into text.
/// </summary>
public static class BreakArmorConverter
{
    private static readonly string destroyer = "Destroyer";

    private static readonly string breacher = "Breacher";

    private static readonly string bigHitter = "Big Hitter";

    private static readonly string scratcher = "Scratcher";

    private static readonly string stillPolish = "Still Polish";



    /// <summary>
    /// Method used to convert value.
    /// </summary>
    /// <param name="armorThrough">Float value to convert</param>
    /// <returns>Converted value</returns>
    public static string TransformArmorThrough(float armorThrough)
    {
        if (armorThrough <= 0.05f)
            return stillPolish;
        else if (0.05f < armorThrough && armorThrough <= 0.15f)
            return scratcher;
        else if (0.15f < armorThrough && armorThrough <= 0.35f)
            return bigHitter;
        else if (0.35f < armorThrough && armorThrough <= 0.65f)
            return breacher;
        else if (armorThrough > 0.65f)
            return destroyer;
        else
            return "Unknown";
    }
}
