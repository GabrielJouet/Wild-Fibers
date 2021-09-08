/// <summary>
/// Class used to handle conversion between numeric to string values.
/// </summary>
public static class Converter
{
    private static readonly string veryFast = "V. Fast";
    private static readonly string veryHigh = "V. High";
    private static readonly string destroyer = "Destroyer";
    private static readonly string unbreakable = "Unbreakable";

    private static readonly string fast = "Fast";
    private static readonly string high = "High";
    private static readonly string breacher = "Breacher";

    private static readonly string average = "Average";
    private static readonly string bigHitter = "Big Hitter";
    private static readonly string shielded = "Shielded";

    private static readonly string small = "Small";
    private static readonly string slow = "Slow";
    private static readonly string scratcher = "Scratcher";
    private static readonly string low = "Low";

    private static readonly string verySmall = "V. Small";
    private static readonly string verySlow = "V. Slow";
    private static readonly string stillPolish = "Still Polish";

    private static readonly string none = "None";



    /// <summary>
    /// Method used to convert value.
    /// </summary>
    /// <param name="armor">Float value to convert</param>
    /// <returns>Converted value</returns>
    public static string TransformArmor(float armor)
    {
        if (armor <= 0.1f)
            return none;
        else if (0.1f < armor && armor <= 0.25f)
            return low;
        else if (0.25f < armor && armor <= 0.45f)
            return shielded;
        else if (0.45f < armor && armor <= 0.85f)
            return high;
        else if (armor > 0.85f)
            return unbreakable;
        else
            return "Unknown";
    }



    /// <summary>
    /// Method used to convert value.
    /// </summary>
    /// <param name="armor">Float value to convert</param>
    /// <returns>Converted value</returns>
    public static string TransformResistance(float armor)
    {
        if (armor <= 0.1f)
            return none;
        else if (0.1f < armor && armor <= 0.25f)
            return low;
        else if (0.25f < armor && armor <= 0.45f)
            return shielded;
        else if (0.45f < armor && armor <= 0.85f)
            return high;
        else if (armor > 0.85f)
            return unbreakable;
        else
            return "Unknown";
    }

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



    /// <summary>
    /// Method used to convert value.
    /// </summary>
    /// <param name="fireRate">Float value to convert</param>
    /// <returns>Converted value</returns>
    public static string TransformFireRate(float fireRate)
    {
        if (fireRate <= 0.25f)
            return verySlow;
        else if (0.25f < fireRate && fireRate <= 0.55f)
            return slow;
        else if (0.55f < fireRate && fireRate <= 0.85f)
            return average;
        else if (0.85f < fireRate && fireRate <= 1.15f)
            return fast;
        else if (fireRate > 1.15f)
            return veryFast;
        else
            return "Unknown";
    }
}