public class Attack
{
    public float Damage { get; private set; }

    public float ArmorThrough { get; private set; }

    public float DotDuration { get; private set; }

    public float ArmorThroughMalus { get; private set; }

    public float DotDamage { get; private set; }


    public Attack(float damage, float armorThrough, float dotDuration, float armorMalus, float dotDamage)
    {
        Damage = damage;
        ArmorThrough = armorThrough;
        DotDuration = dotDuration;
        ArmorThroughMalus = armorMalus;
        DotDamage = dotDamage;
    }
}
