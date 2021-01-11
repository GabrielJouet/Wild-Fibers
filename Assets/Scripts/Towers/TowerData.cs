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
    public string Description { get => _description; }

    /// <summary>
    /// Price of the tower.
    /// </summary>
    [SerializeField]
    protected int _price;
    public int Price { get => _price; }

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
    public float TimeShots { get => _timeBetweenShots; }

    /// <summary>
    /// Damage per attack.
    /// </summary>
    [SerializeField]
    protected int _damage;
    public int Damage { get => _damage; }

    /// <summary>
    /// Armor through on each attack.
    /// </summary>
    [SerializeField]
    protected float _armorThrough;
    public float ArmorThrough { get => _armorThrough; }

    /// <summary>
    /// Armor through on each attack.
    /// </summary>
    [SerializeField]
    protected float _speed;
    public float ProjectileSpeed { get => _speed; }

    /// <summary>
    /// Number of projectile per attack.
    /// </summary>
    [SerializeField]
    protected int _numberOfShots;
    public int Shots { get => _numberOfShots; }

    /// <summary>
    /// Range of the tower.
    /// </summary>
    [SerializeField]
    protected float _range;
    public float Range { get => _range; }

    /// <summary>
    /// Can the tower hits flying target?
    /// </summary>
    [SerializeField]
    protected bool _canHitFlying;
    public bool HitFlying { get => _canHitFlying; }

    /// <summary>
    /// Does the towers choose its target?
    /// </summary>
    [SerializeField]
    protected bool _shotsRandomly;
    public bool ShotsRandomly { get => _shotsRandomly; }


    [Header("Dot related")]
    //Armor through negative effect on enemy
    [SerializeField]
    private float _armorThroughMalus;
    public float ArmorThroughMalus { get => _armorThroughMalus; }

    //How much damage the dot will do?
    [SerializeField]
    private float _damageOverTime;
    public float Dot { get => _damageOverTime; }

    //Duration of the dot in seconds
    [SerializeField]
    private float _dotDuration;
    public float DotDuration { get => _dotDuration; }

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
}
