public static class ArmorType 
{
    private static readonly string unbreakable = "Unbreakable";

    private static readonly string high = "High";

    private static readonly string shielded = "Shielded";

    private static readonly string low = "Low";

    private static readonly string none = "None";


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
}
