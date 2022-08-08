/// <summary>
/// Class used to handle conversion between numeric to string values.
/// </summary>
public static class Converter
{
    /// <summary>
    /// Method used to convert value.
    /// </summary>
    /// <param name="armor">Float value to convert</param>
    /// <returns>Converted value</returns>
    public static string TransformArmor(float armor)
    {
        if (armor <= 0.1f)
            return "None";
        else if (0.1f < armor && armor <= 0.25f)
            return "Low";
        else if (0.25f < armor && armor <= 0.45f)
            return "Shielded";
        else if (0.45f < armor && armor <= 0.85f)
            return "High";
        else if (armor > 0.85f)
            return "Unbreakable";
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
            return "None";
        else if (0.1f < armor && armor <= 0.25f)
            return "Low";
        else if (0.25f < armor && armor <= 0.45f)
            return "Shielded";
        else if (0.45f < armor && armor <= 0.85f)
            return "High";
        else if (armor > 0.85f)
            return "Unbreakable";
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
            return "Still Polish";
        else if (0.05f < armorThrough && armorThrough <= 0.15f)
            return "Scratcher";
        else if (0.15f < armorThrough && armorThrough <= 0.35f)
            return "Big Hitter";
        else if (0.35f < armorThrough && armorThrough <= 0.65f)
            return "Breacher";
        else if (armorThrough > 0.65f)
            return "Destroyer";
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
            return "None";
        else if (0 < dot && dot <= 10)
            return "V. Small";
        else if (10 < dot && dot <= 25)
            return "Small";
        else if (25 < dot && dot <= 55)
            return "Average";
        else if (55 < dot && dot <= 80)
            return "High";
        else if (dot > 80)
            return "V. High";
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
            return "V. Slow";
        else if (0.1f < speed && speed <= 0.2f)
            return "Slow";
        else if (0.2f < speed && speed <= 0.3f)
            return "Average";
        else if (0.3f < speed && speed <= 0.4f)
            return "Fast";
        else if (speed > 0.4f)
            return "V. Fast";
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
            return "V. Slow";
        else if (0.25f < fireRate && fireRate <= 0.55f)
            return "Slow";
        else if (0.55f < fireRate && fireRate <= 0.85f)
            return "Average";
        else if (0.85f < fireRate && fireRate <= 1.15f)
            return "Fast";
        else if (fireRate > 1.15f)
            return "V. Fast";
        else
            return "Unknown";
    }
}