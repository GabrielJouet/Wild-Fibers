﻿public static class FireRate 
{
    private static readonly string veryFast = "Very Fast";

    private static readonly string fast = "Fast";

    private static readonly string average = "Average";

    private static readonly string slow = "Slow";

    private static readonly string verySlow = "Very Slow";


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
