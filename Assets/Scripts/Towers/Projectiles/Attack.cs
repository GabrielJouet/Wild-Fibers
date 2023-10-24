namespace Towers.Projectiles
{
    /// <summary>
    /// Class used to handle a tower attack.
    /// </summary>
    public class Attack
    {
        /// <summary>
        ///  Blank damage.
        /// </summary>
        public float Damage { get; set; }

        /// <summary>
        /// Armor through in percentage.
        /// </summary>
        public float ArmorThrough { get; set; }

        /// <summary>
        /// Dot duration in seconds.
        /// </summary>
        public float DotDuration { get; set; }

        /// <summary>
        /// Armor through malus when dotted.
        /// </summary>
        public float ArmorThroughMalus { get; set; }

        /// <summary>
        /// Damage over time.
        /// </summary>
        public int DotDamage { get; set; }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="damage">Damage of the new attack</param>
        /// <param name="armorThrough">Armor through of the new attack</param>
        /// <param name="dotDuration">Dot duration of the new attack</param>
        /// <param name="armorMalus">Armor malus of the new attack</param>
        /// <param name="dotDamage">Dot damage of the new attack</param>
        public Attack(float damage, float armorThrough, float dotDuration, float armorMalus, int dotDamage)
        {
            Damage = damage;
            ArmorThrough = armorThrough;
            DotDuration = dotDuration;
            ArmorThroughMalus = armorMalus;
            DotDamage = dotDamage;
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="clone">Attack to clone</param>
        public Attack(Attack clone)
        {
            Damage = clone.Damage;
            ArmorThrough = clone.ArmorThrough;
            DotDuration = clone.DotDuration;
            ArmorThroughMalus = clone.ArmorThroughMalus;
            DotDamage = clone.DotDamage;
        }
    }
}