using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that will handle tower data like number of shots and more.
/// </summary>
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
    public int Price { get; set; }

    /// <summary>
    /// Resell Price factor of the tower.
    /// </summary>
    protected float _resellPriceFactor = 1;
    public float ResellPriceFactor { get; set; }

    /// <summary>
    /// Icon of the tower.
    /// </summary>
    [SerializeField]
    protected Sprite _icon;
    public Sprite Icon { get => _icon; }

    /// <summary>
    /// Sprite used in game.
    /// </summary>
    [SerializeField]
    protected Sprite _sprite;
    public Sprite Sprite { get => _sprite; }

    /// <summary>
    /// Shadow sprite used in game.
    /// </summary>
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
    public float TimeShots { get; set; }

    /// <summary>
    /// Damage per attack.
    /// </summary>
    [SerializeField]
    protected int _damage;
    public int Damage { get; set; }

    /// <summary>
    /// Armor through on each attack.
    /// </summary>
    [SerializeField]
    protected float _armorThrough;
    public float ArmorThrough { get; set; }

    /// <summary>
    /// Armor through on each attack.
    /// </summary>
    [SerializeField]
    protected float _speed;
    public float ProjectileSpeed { get;set; }

    /// <summary>
    /// Number of projectile per attack.
    /// </summary>
    [SerializeField]
    protected int _numberOfShots;
    public int Shots { get; set; }

    /// <summary>
    /// Range of the tower.
    /// </summary>
    [SerializeField]
    protected float _range;
    public float Range { get; set; }

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

    /// <summary>
    /// Armor malus with each shot.
    /// </summary>
    [SerializeField]
    private float _armorThroughMalus;
    public float ArmorThroughMalus { get; set; }

    /// <summary>
    /// Dot over time damage.
    /// </summary>
    [SerializeField]
    private int _damageOverTime;
    public int Dot { get; set; }

    /// <summary>
    /// Dot duration.
    /// </summary>
    [SerializeField]
    private float _dotDuration;
    public float DotDuration { get; set; }

    /// <summary>
    /// Dot sprite.
    /// </summary>
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

    /// <summary>
    /// Tower specs (only for last level).
    /// </summary>
    [SerializeField]
    protected List<TowerSpec> _towerSpecs;
    public List<TowerSpec> Specs { get => _towerSpecs; }


    [SerializeField]
    protected List<Augmentation> _towerAugmentations;
    public List<Augmentation> Augmentations { get => _towerAugmentations; }

    public int AugmentationLevel { get; set; }


    /// <summary>
    /// Script used for this tower.
    /// </summary>
    [SerializeField]
    protected string _scriptName;
    public string Script { get => _scriptName; }


    [Header("Info related")]

    /// <summary>
    /// A special info box.
    /// </summary>
    [SerializeField]
    protected string _special;
    public string Special { get => _special; }

    /// <summary>
    /// Screen shot displaying what the tower can do.
    /// </summary>
    [SerializeField]
    protected Sprite _screenShot;
    public Sprite ScreenShot { get => _screenShot; }

    /// <summary>
    /// Fire rate transformed.
    /// </summary>
    public string FireRateInfo { get => Converter.TransformFireRate(1 / _timeBetweenShots); }

    /// <summary>
    /// Armor through transformed.
    /// </summary>
    public string ArmorThroughInfo { get => Converter.TransformArmorThrough((_armorThroughMalus + _armorThrough) / 100); }

    /// <summary>
    /// Dot transformed.
    /// </summary>
    public string DotInfo { get => Converter.TransformDot(_dotDuration * _damageOverTime * 2); }

    /// <summary>
    /// Number of shots in string version.
    /// </summary>
    public string ShotsInfo { get => _numberOfShots.ToString(); }

    /// <summary>
    /// Price in string version.
    /// </summary>
    public string PriceInfo { get => _price.ToString(); }

    /// <summary>
    /// Damage in string version.
    /// </summary>
    public string DamageInfo { get => _damage.ToString(); }



    private void OnEnable()
    {
        Price = _price;
        ResellPriceFactor = _resellPriceFactor;

        TimeShots = _timeBetweenShots;
        Damage = _damage;
        ArmorThrough = _armorThrough;
        ProjectileSpeed = _speed;
        Shots = _numberOfShots;
        Range = _range;

        ArmorThroughMalus = _armorThroughMalus;
        Dot = _damageOverTime;
        DotDuration = _dotDuration;
    }
}