public class Attack
{
    public float Damage { get; set; }

    public float ArmorThrough { get; set; }

    public Attack(float damage, float armorThrough)
    {
        Damage = damage;
        ArmorThrough = armorThrough;
    }
}
