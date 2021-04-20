public class Attack
{
    public float Damage { get; set; }

    public float ArmorThrough { get; set; }

    public float DotDuration { get; set; }

    public float ArmorThroughMalus { get; set; }

    public int DotDamage { get; set; }


    public Attack(float damage, float armorThrough, float dotDuration, float armorMalus, int dotDamage)
    {
        Damage = damage;
        ArmorThrough = armorThrough;
        DotDuration = dotDuration;
        ArmorThroughMalus = armorMalus;
        DotDamage = dotDamage;
    }


    public Attack() { }


    public Attack(Attack clone)
    {
        Damage = clone.Damage;
        ArmorThrough = clone.ArmorThrough;
        DotDuration = clone.DotDuration;
        ArmorThroughMalus = clone.ArmorThroughMalus;
        DotDamage = clone.DotDamage;
    }
}
