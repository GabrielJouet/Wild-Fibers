using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that will handle tower data like number of shots and more.
/// </summary>
[CreateAssetMenu(menuName = "Towers/Data", fileName = "NewTower")]
public class TowerData : ScriptableObject
{
    #region Description
    [Header("Description")]

    /// <summary>
    /// Description.
    /// </summary>
    [SerializeField]
    protected string _description;
    public string Description { get => _description; private set => _description = value; }

    /// <summary>
    /// Description.
    /// </summary>
    [SerializeField]
    protected string _libraryDescription;
    public string LibraryDescription { get => _libraryDescription; private set => _libraryDescription = value; }

    /// <summary>
    /// Price of the tower.
    /// </summary>
    [SerializeField]
    protected int _price;
    public int Price { get => _price; set => _price = value; }

    /// <summary>
    /// Resell Price factor of the tower.
    /// </summary>
    protected float _resellPriceFactor = 1;
    public float ResellPriceFactor { get => _resellPriceFactor; set => _resellPriceFactor = value; }

    /// <summary>
    /// Icon of the tower.
    /// </summary>
    [SerializeField]
    protected Sprite _icon;
    public Sprite Icon { get => _icon; private set => _icon = value; }

    /// <summary>
    /// Sprite used in game.
    /// </summary>
    [SerializeField]
    protected Sprite _sprite;
    public Sprite Sprite { get => _sprite; private set => _sprite = value; }

    /// <summary>
    /// Shadow sprite used in game.
    /// </summary>
    [SerializeField]
    protected Sprite _shadowSprite;
    public Sprite Shadow { get => _shadowSprite; private set => _shadowSprite = value; }

    /// <summary>
    /// Script used for this tower.
    /// </summary>
    [SerializeField]
    protected string _scriptName;
    public string Script { get => _scriptName; private set => _scriptName = value; }
    #endregion



    #region Damage
    [Header("Damage Related")]

    /// <summary>
    /// Projectile used in attack.
    /// </summary>
    [SerializeField]
    protected GameObject _projectileUsed;
    public GameObject Projectile { get => _projectileUsed; private set => _projectileUsed = value; }

    /// <summary>
    /// Time between attack in second.
    /// </summary>
    [SerializeField]
    protected float _timeBetweenShots;
    public float TimeShots { get => _timeBetweenShots; set => _timeBetweenShots = value; }

    /// <summary>
    /// Damage per attack.
    /// </summary>
    [SerializeField]
    protected int _damage;
    public int Damage { get => _damage; set => _damage = value; }

    /// <summary>
    /// Damage in string version.
    /// </summary>
    public string DamageInfo { get => _damage.ToString(); }

    /// <summary>
    /// Armor through on each attack.
    /// </summary>
    [SerializeField]
    protected float _armorThrough;
    public float ArmorThrough { get => _armorThrough; set => _armorThrough = value; }

    /// <summary>
    /// Number of projectile per attack.
    /// </summary>
    [SerializeField]
    protected int _numberOfShots;
    public int Shots { get => _numberOfShots; set => _numberOfShots = value; }

    /// <summary>
    /// Range of the tower.
    /// </summary>
    [SerializeField]
    protected float _range;
    public float Range { get => _range; set => _range = value; }

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
    #endregion



    #region Dot
    [Header("Dot related")]

    /// <summary>
    /// Armor malus with each shot.
    /// </summary>
    [SerializeField]
    private float _armorThroughMalus;
    public float ArmorThroughMalus { get => _armorThroughMalus; set => _armorThroughMalus = value; }

    /// <summary>
    /// Dot over time damage.
    /// </summary>
    [SerializeField]
    private int _damageOverTime;
    public int Dot { get => _damageOverTime; set => _damageOverTime = value; }

    /// <summary>
    /// Dot duration.
    /// </summary>
    [SerializeField]
    private float _dotDuration;
    public float DotDuration { get => _dotDuration; set => _dotDuration = value; }
    #endregion



    #region Upgrades
    [Header("Upgrades")]

    /// <summary>
    /// Tower upgrades.
    /// </summary>
    [SerializeField]
    protected List<TowerData> _towerUpgrades;
    public List<TowerData> Upgrades { get => _towerUpgrades; private set => _towerUpgrades = value; }

    /// <summary>
    /// Tower specs (only for last level).
    /// </summary>
    [SerializeField]
    protected List<TowerSpec> _towerSpecs;
    public List<TowerSpec> Specs { get => _towerSpecs; private set => _towerSpecs = value; }


    /// <summary>
    /// Tower Augmentations (only for first level).
    /// </summary>
    [SerializeField]
    protected List<Augmentation> _towerAugmentations;
    public List<Augmentation> Augmentations { get => _towerAugmentations; private set => _towerAugmentations = value; }

    /// <summary>
    /// Current augmentation level of this tower.
    /// </summary>
    public int AugmentationLevel { get; set; }
    #endregion



    #region Library
    [Header("Info related")]

    /// <summary>
    /// A special info box.
    /// </summary>
    [SerializeField]
    protected string _special;
    public string Special { get => _special; private set => _special = value; }

    /// <summary>
    /// Screen shot displaying what the tower can do.
    /// </summary>
    [SerializeField]
    protected Sprite _screenShot;
    public Sprite ScreenShot { get => _screenShot; private set => _screenShot = value; }

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
    #endregion



    /// <summary>
    /// Method used to clone tower data.
    /// </summary>
    /// <param name="clone">The object to clone</param>
    public void Populate(TowerData clone)
    {
        name = clone.name;
        Description = clone.Description;
        LibraryDescription = clone.LibraryDescription;
        Price = clone.Price;
        ResellPriceFactor = clone.ResellPriceFactor;
        Icon = clone.Icon;
        Sprite = clone.Sprite;
        Shadow = clone.Shadow;
        Script = clone.Script;

        Projectile = clone.Projectile;
        TimeShots = clone.TimeShots;
        Damage = clone.Damage;
        ArmorThrough = clone.ArmorThrough;
        Shots = clone.Shots;
        Range = clone.Range;
        HitFlying = clone.HitFlying;
        ShotsRandomly = clone.ShotsRandomly;

        ArmorThroughMalus = clone.ArmorThroughMalus;
        Dot = clone.Dot;
        DotDuration = clone.DotDuration;

        Specs = clone.Specs;
        Augmentations = clone.Augmentations;
        AugmentationLevel = clone.AugmentationLevel;

        ScreenShot = clone.ScreenShot;
        Special = clone.Special;

        List<TowerData> newUpgrades = new List<TowerData>();
        foreach (TowerData upgrade in clone.Upgrades)
        {
            TowerData buffer = CreateInstance<TowerData>();
            buffer.Populate(upgrade, AugmentationLevel);
            newUpgrades.Add(buffer);
        }

        Upgrades = newUpgrades;
    }

    /// <summary>
    /// Method used to clone tower data.
    /// </summary>
    /// <param name="clone">The object to clone</param>
    /// <param name="augmentationLevel">The new augmentation level</param>
    public void Populate(TowerData clone, int augmentationLevel)
    {
        name = clone.name;
        Description = clone.Description;
        LibraryDescription = clone.LibraryDescription;
        Price = clone.Price;
        ResellPriceFactor = clone.ResellPriceFactor;
        Icon = clone.Icon;
        Sprite = clone.Sprite;
        Shadow = clone.Shadow;
        Script = clone.Script;

        Projectile = clone.Projectile;
        TimeShots = clone.TimeShots;
        Damage = clone.Damage;
        ArmorThrough = clone.ArmorThrough;
        Shots = clone.Shots;
        Range = clone.Range;
        HitFlying = clone.HitFlying;
        ShotsRandomly = clone.ShotsRandomly;

        ArmorThroughMalus = clone.ArmorThroughMalus;
        Dot = clone.Dot;
        DotDuration = clone.DotDuration;

        List<TowerData> newUpgrades = new List<TowerData>();
        foreach (TowerData upgrade in clone.Upgrades)
        {
            TowerData buffer = CreateInstance<TowerData>();
            buffer.Populate(upgrade, augmentationLevel);
            newUpgrades.Add(buffer);
        }

        Upgrades = newUpgrades;
        Specs = clone.Specs;
        Augmentations = clone.Augmentations;
        AugmentationLevel = augmentationLevel;

        ScreenShot = clone.ScreenShot;
        Special = clone.Special;
    }


    /// <summary>
    /// Method used to reduce price of a tower with a ratio.
    /// </summary>
    /// <param name="ratio">Ratio of the price asked</param>
    public void ReducePrice(float ratio)
    {
        Price = Mathf.FloorToInt(_price * ratio);
    }


    /// <summary>
    /// Method used to reduce price of a tower with a fixed amount.
    /// </summary>
    /// <param name="count">Amount of the price lowered</param>
    public void ReducePrice(int count)
    {
        Price = _price - count;
    }


    /// <summary>
    /// Method used to increase the resell price.
    /// </summary>
    /// <param name="factor">Factor of the resell price</param>
    public void IncreaseResellPrice(float factor)
    {
        ResellPriceFactor = _resellPriceFactor * factor;
    }
}