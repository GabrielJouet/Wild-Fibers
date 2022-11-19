using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tower class, main object of the game.
/// </summary>
/// <remarks>Needs a static depth manager</remarks>
[RequireComponent(typeof(DepthManager))]
public abstract class Tower : PoolableObject
{
    /// <summary>
    /// Range display.
    /// </summary>
    [SerializeField]
    protected Transform _transformRange;

    /// <summary>
    /// Collider used in range detection.
    /// </summary>
    [SerializeField]
    protected Transform _collider;

    /// <summary>
    /// Selector object used when clicked.
    /// </summary>
    [SerializeField]
    protected GameObject _selector;


    #region Description
    [Header("Description")]

    /// <summary>
    /// Description.
    /// </summary>
    [SerializeField, TextArea]
    protected string _description;
    public string Description { get => _description; private set => _description = value; }

    /// <summary>
    /// Description.
    /// </summary>
    [SerializeField, TextArea]
    protected string _libraryDescription;
    public string LibraryDescription { get => _libraryDescription; private set => _libraryDescription = value; }

    /// <summary>
    /// Base price of the tower.
    /// </summary>
    [SerializeField]
    protected int _price;
    public float PriceFactor { get; set; } = 1f;

    /// <summary>
    /// Resell Price factor of the tower.
    /// </summary>
    public float ResellPriceFactor { get;  set; } = 1f;

    /// <summary>
    /// Calculated price of the tower.
    /// </summary>
    public int Price { get => Mathf.FloorToInt(_price * PriceFactor); }

    /// <summary>
    /// Icon of the tower.
    /// </summary>
    [SerializeField]
    protected Sprite _icon;
    public Sprite Icon { get => _icon; private set => _icon = value; }
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
    protected List<Tower> _towerUpgrades;
    public List<Tower> Upgrades { get => _towerUpgrades; private set => _towerUpgrades = value; }

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
    [SerializeField, TextArea]
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
    /// Information UI object.
    /// </summary>
    protected BackgroudSelecter _backgroundSelecter;

    /// <summary>
    /// The related tower slot.
    /// </summary>
    protected TowerSlot _currentSlot;
    public TowerSlot Slot { get => _currentSlot; }

    /// <summary>
    /// Resource controller used to record money.
    /// </summary>
    protected RessourceController _ressourceController;

    /// <summary>
    /// List of enemies in range.
    /// </summary>
    protected List<Enemy> _availableEnemies = new List<Enemy>();

    /// <summary>
    /// Did the coroutine started?
    /// </summary>
    protected bool _coroutineStarted;

    /// <summary>
    /// How much gold was used on this tower from the beginning.
    /// </summary>
    public int CumulativeGold { get; protected set; } = 0;

    /// <summary>
    /// Basic attack of this tower.
    /// </summary>
    protected Attack _attack;

    /// <summary>
    /// Queue of attacks, used for multi shots towers.
    /// </summary>
    protected Queue<Attack> _nextAttack = new Queue<Attack>();



    /// <summary>
    /// Method used to initialize.
    /// </summary>
    /// <param name="newSlot">The parent slot</param>
    /// <param name="newRessourceController">The resource controller</param>
    /// <param name="newBackgroundSelecter">The background selecter component</param>
    public virtual void Initialize(TowerSlot newSlot, RessourceController newRessourceController, BackgroudSelecter newBackgroundSelecter, int cumulativeGold)
    {
        _nextAttack.Clear();
        _selector.SetActive(false);
        _transformRange.gameObject.SetActive(false);

        _attack = new Attack(Damage, ArmorThrough, DotDuration, ArmorThroughMalus, Dot);
        CheckAugmentation();

        CumulativeGold = cumulativeGold + Mathf.FloorToInt(_price * PriceFactor);

        _transformRange.localScale = Range * Vector3.one;
        _collider.localScale = (0.9f * Range) * Vector3.one;

        _collider.GetComponent<TowerCollider>().ParentTower = this;

        transform.position = newSlot.transform.position;
        _backgroundSelecter = newBackgroundSelecter;
        _ressourceController = newRessourceController;
        _currentSlot = newSlot;

        SpecialBehavior();
    }


    /// <summary>
    /// Method used to add a special behavior at the start of the initialization.
    /// </summary>
    protected virtual void SpecialBehavior() { }


    /// <summary>
    /// FixedUpdate, called 50 times a second.
    /// </summary>
    protected virtual void FixedUpdate()
    {
        if (_availableEnemies.Count > 0 && !_coroutineStarted)
            StartCoroutine(SummonProjectile());
    }


    /// <summary>
    /// Coroutine used to delay attacks.
    /// </summary>
    protected virtual IEnumerator SummonProjectile()
    {
        _coroutineStarted = true;

        if (!ShotsRandomly)
            SortEnemies();

        foreach (Enemy current in RecoverAvailableEnemies(_availableEnemies.Count < Shots ? _availableEnemies.Count : Shots))
            Controller.Instance.PoolController.Out(Projectile.GetComponent<PoolableObject>()).GetComponent<Projectile>().Initialize(_nextAttack.Dequeue(), current, transform);

        yield return new WaitForSeconds(TimeShots);
        _coroutineStarted = false;
    }



    #region Upgrades and Money related
    /// <summary>
    /// Method used to resell the tower.
    /// </summary>
    public void ResellTower()
    {
        ResellSpecialBehavior();

        _ressourceController.AddGold(Mathf.FloorToInt((CumulativeGold * ResellPriceFactor) * 0.65f), false);

        _backgroundSelecter.DisableTowerInformation();
        _backgroundSelecter.DisableTowerSellButton();

        _currentSlot.ResetSlot();
        Controller.Instance.PoolController.In(GetComponent<PoolableObject>());
    }


    /// <summary>
    /// Method override by children to improve resell behavior.
    /// </summary>
    protected virtual void ResellSpecialBehavior() { }


    /// <summary>
    /// Method used to upgrade the tower.
    /// </summary>
    public void UpgradeTower(Tower newData)
    {
        _ressourceController.RemoveGold(newData.Price);

        _backgroundSelecter.DesactivateTower();
        UpgradeSpecialBehavior();

        Controller.Instance.PoolController.In(GetComponent<PoolableObject>());
        Controller.Instance.PoolController.Out(newData).GetComponent<Tower>().Initialize(_currentSlot, _ressourceController, _backgroundSelecter, Price);
    }


    /// <summary>
    /// Method override by children to improve upgrade behavior.
    /// </summary>
    protected virtual void UpgradeSpecialBehavior() { }


    /// <summary>
    /// Method used to add a spec to the tower, will be override by children.
    /// </summary>
    /// <param name="newSpec">The new spec to add</param>
    public virtual void AddSpec(TowerSpec newSpec) { }


    /// <summary>
    /// Method used to check augmentation level of a tower and applying changes.
    /// </summary>
    protected void CheckAugmentation()
    {
        if (AugmentationLevel > 0)
        {
            LevelOneAugmentation();

            if (AugmentationLevel > 1)
            {
                LevelTwoAugmentation();

                if (AugmentationLevel > 2)
                {
                    LevelThreeAugmentation();

                    if (AugmentationLevel > 3)
                    {
                        LevelFourAugmentation();

                        if (AugmentationLevel > 4)
                            LevelFiveAugmentation();
                    }
                }
            }
        }
    }


    /// <summary>
    /// Method called when the tower has one augmentation.
    /// </summary>
    protected virtual void LevelOneAugmentation() { }


    /// <summary>
    /// Method called when the tower has two augmentations.
    /// </summary>
    protected virtual void LevelTwoAugmentation() { }


    /// <summary>
    /// Method called when the tower has three augmentations.
    /// </summary>
    protected virtual void LevelThreeAugmentation() { }


    /// <summary>
    /// Method called when the tower has four augmentations.
    /// </summary>
    protected virtual void LevelFourAugmentation() { }


    /// <summary>
    /// Method called when the tower has five augmentations.
    /// </summary>
    protected virtual void LevelFiveAugmentation() { }
    #endregion 



    #region Enemies interaction
    /// <summary>
    /// Method used to add an enemy to the list.
    /// </summary>
    /// <param name="enemy">The enemy to add</param>
    public void AddEnemy(Enemy enemy)
    {
        if (!(!HitFlying && enemy.Flying))
            _availableEnemies.Add(enemy);
    }


    /// <summary>
    /// Method used to remove an enemy to the list.
    /// </summary>
    /// <param name="enemy">The enemy to remove</param>
    public void RemoveEnemy(Enemy enemy)
    {
        if (_availableEnemies.Contains(enemy))
            _availableEnemies.Remove(enemy);
    }


    /// <summary>
    /// Method used to sort the enemy list.
    /// </summary>
    protected void SortEnemies()
    {
        _availableEnemies.Sort((a, b) => b.PathRatio.CompareTo(a.PathRatio));
    }


    /// <summary>
    /// Method used to recover available and prefered enemies.
    /// </summary>
    /// <param name="numberOfEnemiesToFound">How many enemies are needed</param>
    /// <returns>A list of found enemies</returns>
    protected List<Enemy> RecoverAvailableEnemies(int numberOfEnemiesToFound)
    {
        List<Enemy> availableEnemies = new List<Enemy>();

        if (ShotsRandomly)
            _availableEnemies.Shuffle();
        else
            SortEnemies();

        if (numberOfEnemiesToFound > _availableEnemies.Count)
        {
            foreach (Enemy buffer in _availableEnemies)
            {
                _nextAttack.Enqueue(ChangeNextAttack(buffer));

                availableEnemies.Add(buffer);
            }
        }

        foreach (Enemy buffer in _availableEnemies)
        {
            _nextAttack.Enqueue(ChangeNextAttack(buffer));

            availableEnemies.Add(buffer);

            if (availableEnemies.Count >= numberOfEnemiesToFound)
                break;
        }

        return availableEnemies;
    }


    /// <summary>
    /// Method override by children to change their attack.
    /// </summary>
    /// <param name="enemy">Enemt targeted</param>
    protected virtual Attack ChangeNextAttack(Enemy enemy)
    {
        return _attack;
    }
    #endregion



    #region Reset related
    /// <summary>
    /// Method used to activate the range.
    /// </summary>
    public void ActivateRangeDisplay()
    {
        _transformRange.gameObject.SetActive(true);
        _selector.SetActive(true);
    }


    /// <summary>
    /// Method used to desactivate the range.
    /// </summary>
    public void DesactivateRangeDisplay()
    {
        _transformRange.gameObject.SetActive(false);
        _selector.SetActive(false);
    }


    /// <summary>
    /// Method called when the tower needs to be desactivated.
    /// </summary>
    public override void OnInPool()
    {
        DesactivateRangeDisplay();

        _coroutineStarted = false;
        StopAllCoroutines();

        _collider.localScale = Vector3.one;
        _transformRange.localScale = Vector3.one;

        gameObject.SetActive(false);
    }
    #endregion



    /// <summary>
    /// Method used to reduce price of a tower with a ratio.
    /// </summary>
    /// <param name="ratio">Ratio of the price asked</param>
    public void ReducePrice(float ratio)
    {
        PriceFactor = ratio;
    }


    /// <summary>
    /// Method used to increase the resell price.
    /// </summary>
    /// <param name="factor">Factor of the resell price</param>
    public void IncreaseResellPrice(float factor)
    {
        ResellPriceFactor = factor;
    }
}