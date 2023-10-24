namespace UI
{
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
            return armor switch
            {
                <= 0.1f => "None",
                > 0.1f and <= 0.25f => "Low",
                > 0.25f and <= 0.45f => "Shielded",
                > 0.45f and <= 0.85f => "High",
                > 0.85f => "Unbreakable",
                _ => "Unknown"
            };
        }



        /// <summary>
        /// Method used to convert value.
        /// </summary>
        /// <param name="armor">Float value to convert</param>
        /// <returns>Converted value</returns>
        public static string TransformResistance(float armor)
        {
            return armor switch
            {
                <= 0.1f => "None",
                > 0.1f and <= 0.25f => "Low",
                > 0.25f and <= 0.45f => "Shielded",
                > 0.45f and <= 0.85f => "High",
                > 0.85f => "Unbreakable",
                _ => "Unknown"
            };
        }

        /// <summary>
        /// Method used to convert value.
        /// </summary>
        /// <param name="armorThrough">Float value to convert</param>
        /// <returns>Converted value</returns>
        public static string TransformArmorThrough(float armorThrough)
        {
            return armorThrough switch
            {
                <= 0.05f => "Still Polish",
                > 0.05f and <= 0.15f => "Scratcher",
                > 0.15f and <= 0.35f => "Big Hitter",
                > 0.35f and <= 0.65f => "Breacher",
                > 0.65f => "Destroyer",
                _ => "Unknown"
            };
        }

        /// <summary>
        /// Method used to convert value.
        /// </summary>
        /// <param name="dot">Float value to convert</param>
        /// <returns>Converted value</returns>
        public static string TransformDot(float dot)
        {
            return dot switch
            {
                <= 0 => "None",
                > 0 and <= 10 => "V. Small",
                > 10 and <= 25 => "Small",
                > 25 and <= 55 => "Average",
                > 55 and <= 80 => "High",
                > 80 => "V. High",
                _ => "Unknown"
            };
        }



        /// <summary>
        /// Method used to convert value.
        /// </summary>
        /// <param name="speed">Float value to convert</param>
        /// <returns>Converted value</returns>
        public static string TransformSpeed(float speed)
        {
            return speed switch
            {
                <= 0.1f => "V. Slow",
                > 0.1f and <= 0.2f => "Slow",
                > 0.2f and <= 0.3f => "Average",
                > 0.3f and <= 0.4f => "Fast",
                > 0.4f => "V. Fast",
                _ => "Unknown"
            };
        }



        /// <summary>
        /// Method used to convert value.
        /// </summary>
        /// <param name="fireRate">Float value to convert</param>
        /// <returns>Converted value</returns>
        public static string TransformFireRate(float fireRate)
        {
            return fireRate switch
            {
                <= 0.25f => "V. Slow",
                > 0.25f and <= 0.55f => "Slow",
                > 0.55f and <= 0.85f => "Average",
                > 0.85f and <= 1.15f => "Fast",
                > 1.15f => "V. Fast",
                _ => "Unknown"
            };
        }
    }
}