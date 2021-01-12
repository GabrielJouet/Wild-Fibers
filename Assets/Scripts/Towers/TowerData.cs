using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Towers/Data", fileName = "NewTower")]
public class TowerData : ScriptableObject
{
    [Header("Description")]

    /// <summary>
    /// Description.
    /// </summary>
    [SerializeField]
    protected string _description;
    public string Description { get => _description; private set => _description = value; }

    /// <summary>
    /// Price of the tower.
    /// </summary>
    [SerializeField]
    protected int _price;
    public int Price { get => _price; private set => _price = value; }

    /// <summary>
    /// Icon of the tower.
    /// </summary>
    [SerializeField]
    protected Sprite _icon;
    public Sprite Icon { get => _icon; }

    [SerializeField]
    protected Sprite _sprite;
    public Sprite Sprite { get => _sprite; }

    [SerializeField]
    protected Sprite _shadowSprite;
    public Sprite Shadow { get => _shadowSprite; }



    [Header("Damage Related")]

    /// <summary>
    /// Projectile used in attack.
    /// </summary>
    [SerializeField]
    protected GameObject _projectileUsed;
    public GameObject Projectile { get => _projectileUsed; }

    /// <summary>
    /// Time between attack in second.
    /// </summary>
    [SerializeField]
    protected float _timeBetweenShots;
    public float TimeShots { get => _timeBetweenShots; private set => _timeBetweenShots = value; }

    /// <summary>
    /// Damage per attack.
    /// </summary>
    [SerializeField]
    protected int _damage;
    public int Damage { get => _damage; private set => _damage = value; }

    /// <summary>
    /// Armor through on each attack.
    /// </summary>
    [SerializeField]
    protected float _armorThrough;
    public float ArmorThrough { get => _armorThrough; private set => _armorThrough = value; }

    /// <summary>
    /// Armor through on each attack.
    /// </summary>
    [SerializeField]
    protected float _speed;
    public float ProjectileSpeed { get => _speed; private set => _speed = value; }

    /// <summary>
    /// Number of projectile per attack.
    /// </summary>
    [SerializeField]
    protected int _numberOfShots;
    public int Shots { get => _numberOfShots; private set => _numberOfShots = value; }

    /// <summary>
    /// Range of the tower.
    /// </summary>
    [SerializeField]
    protected float _range;
    public float Range { get => _range; private set => _range = value; }

    /// <summary>
    /// Can the tower hits flying target?
    /// </summary>
    [SerializeField]
    protected bool _canHitFlying;
    public bool HitFlying { get => _canHitFlying; private set => _canHitFlying = value; }

    /// <summary>
    /// Does the towers choose its target?
    /// </summary>
    [SerializeField]
    protected bool _shotsRandomly;
    public bool ShotsRandomly { get => _shotsRandomly; private set => _shotsRandomly = value; }


    [Header("Dot related")]
    //Armor through negative effect on enemy
    [SerializeField]
    private float _armorThroughMalus;
    public float ArmorThroughMalus { get => _armorThroughMalus; private set => _armorThroughMalus = value; }

    //How much damage the dot will do?
    [SerializeField]
    private float _damageOverTime;
    public float Dot { get => _damageOverTime; private set => _damageOverTime = value; }

    //Duration of the dot in seconds
    [SerializeField]
    private float _dotDuration;
    public float DotDuration { get => _dotDuration; private set => _dotDuration = value; }

    [SerializeField]
    private Sprite _dotIcon;
    public Sprite DotIcon { get => _dotIcon; }


    [Header("Upgrades")]

    /// <summary>
    /// Tower upgrades.
    /// </summary>
    [SerializeField]
    protected List<TowerData> _towerUpgrades;
    public List<TowerData> Upgrades { get => _towerUpgrades; }

    [SerializeField]
    protected List<TowerSpec> _towerSpecs;
    public List<TowerSpec> Specs { get => _towerSpecs; }


    public void LoadData(int newPrice, float newTimeShots, int newDamage, float newArmorThrough, float newSpeed, int newShots, float newRange, bool newCanHitFLying, bool newShotsRandomly, float newArmorMalus, float newDot, float newDotDuration)
    {
        Price = newPrice;

        TimeShots = newTimeShots;
        Damage = newDamage;
        ArmorThrough = newArmorThrough;
        ProjectileSpeed = newSpeed;
        Shots = newShots;
        Range = newRange;
        HitFlying = newCanHitFLying;
        ShotsRandomly = newShotsRandomly;

        ArmorThroughMalus = newArmorMalus;
        Dot = newDot;
        DotDuration = newDotDuration;
    }
}
