/// <summary>
/// Class used to convert armor value into text.
/// </summary>
public static class ArmorTypeConverter
{
    private static readonly string unbreakable = "Unbreakable";

    private static readonly string high = "High";

    private static readonly string shielded = "Shielded";

    private static readonly string low = "Low";

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
}
