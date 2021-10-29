using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tower class, main object of the game.
/// </summary>
/// <remarks>Needs a static depth manager</remarks>
[RequireComponent(typeof(StaticDepthManager))]
public abstract class Tower : MonoBehaviour
{
    /// <summary>
    /// Data associated to this tower.
    /// </summary>
    [SerializeField]
    protected TowerData _towerData;
    public TowerData Data { get => _towerData; }


    /// <summary>
    /// Range display.
    /// </summary>
    protected Transform _transformRange;

    /// <summary>
    /// Collider used in range detection.
    /// </summary>
    protected Transform _collider;

    /// <summary>
    /// Sprite renderer component.
    /// </summary>
    protected SpriteRenderer _spriteRenderer;

    /// <summary>
    /// Sprite renderer component of the shadow.
    /// </summary>
    protected SpriteRenderer _shadowSpriteRenderer;


    /// <summary>
    /// Selector object used when clicked.
    /// </summary>
    protected GameObject _selector;


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
    /// Pool used to recover projectiles.
    /// </summary>
    protected ProjectilePool _projectilePool;

    /// <summary>
    /// Tower pool to recover and create towers.
    /// </summary>
    protected TowerPool _towerPool;

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
    /// Awake method used at initialization.
    /// </summary>
    protected void Awake()
    {
        _transformRange = transform.Find("Range");
        _collider = transform.Find("Collider");

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _shadowSpriteRenderer = transform.Find("Shadow").GetComponent<SpriteRenderer>();

        _selector = transform.Find("Selecter").gameObject;
    }


    /// <summary>
    /// Method used to initialize.
    /// </summary>
    /// <param name="newSlot">The parent slot</param>
    /// <param name="newRessourceController">The resource controller</param>
    /// <param name="newBackgroundSelecter">The background selecter component</param>
    /// <param name="newPool">The new projectile pool</param>
    /// <param name="newTowerPool">The new tower pool</param>
    public virtual void Initialize(TowerSlot newSlot, RessourceController newRessourceController, BackgroudSelecter newBackgroundSelecter, ProjectilePool newPool, TowerPool newTowerPool, TowerData newData)
    {
        _nextAttack.Clear();
        SetDefaultValues(newData);

        transform.position = newSlot.transform.position;
        _backgroundSelecter = newBackgroundSelecter;
        _ressourceController = newRessourceController;
        _projectilePool = newPool;
        _currentSlot = newSlot;
        _towerPool = newTowerPool;

        GetComponent<StaticDepthManager>().ResetSortingOrder();
        SpecialBehavior();
    }


    /// <summary>
    /// Method used to reset the tower to tower data values.
    /// </summary>
    /// <param name="newData">The new data to use</param>
    private void SetDefaultValues(TowerData newData)
    {
        _selector.SetActive(false);
        _transformRange.gameObject.SetActive(false);

        _towerData = ScriptableObject.CreateInstance<TowerData>();
        _towerData.Populate(newData);

        _attack = new Attack(Data.Damage, Data.ArmorThrough, Data.DotDuration, Data.ArmorThroughMalus, Data.Dot);
        CheckAugmentation();

        CumulativeGold += Data.Price;

        _spriteRenderer.sprite = Data.Sprite;
        _shadowSpriteRenderer.sprite = Data.Shadow;

        _transformRange.localScale = Data.Range * Vector3.one;
        _collider.localScale = (0.9f * Data.Range) * Vector3.one;

        _collider.GetComponent<TowerCollider>().ParentTower = this;
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

        int numberOfStrikes = _availableEnemies.Count < Data.Shots ? _availableEnemies.Count : Data.Shots;

        if (!Data.ShotsRandomly)
            SortEnemies();

        foreach (Enemy current in RecoverAvailableEnemies(numberOfStrikes))
            _projectilePool.GetOneProjectile().Initialize(_nextAttack.Dequeue(), current, _projectilePool, transform);

        yield return new WaitForSeconds(Data.TimeShots);
        _coroutineStarted = false;
    }



    #region Upgrades and Money related
    /// <summary>
    /// Method used to resell the tower.
    /// </summary>
    public void ResellTower()
    {
        ResellSpecialBehavior();

        _ressourceController.AddGold(Mathf.FloorToInt((CumulativeGold * Data.ResellPriceFactor) * 0.65f), false);

        _backgroundSelecter.DisableTowerInformation();
        _backgroundSelecter.DisableTowerSellButton();

        _currentSlot.ResetSlot();
        _towerPool.AddOneTower(gameObject);
    }


    /// <summary>
    /// Method override by children to improve resell behavior.
    /// </summary>
    protected virtual void ResellSpecialBehavior() { }


    /// <summary>
    /// Method used to upgrade the tower.
    /// </summary>
    public void UpgradeTower(TowerData newData)
    {
        _ressourceController.RemoveGold(newData.Price);

        SetDefaultValues(newData);
        _backgroundSelecter.DesactivateTower();

        UpgradeSpecialBehavior();
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
        if (Data.AugmentationLevel > 0)
        {
            LevelOneAugmentation();

            if (Data.AugmentationLevel > 1)
            {
                LevelTwoAugmentation();

                if (Data.AugmentationLevel > 2)
                {
                    LevelThreeAugmentation();

                    if (Data.AugmentationLevel > 3)
                    {
                        LevelFourAugmentation();

                        if (Data.AugmentationLevel > 4)
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
        if (!(!Data.HitFlying && enemy.Flying))
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

        if (Data.ShotsRandomly)
            _availableEnemies.Shuffle();
        else
            SortEnemies();

        if (numberOfEnemiesToFound > _availableEnemies.Count)
        {
            foreach (Enemy buffer in _availableEnemies)
            {
                if (buffer.CanBeTargeted)
                {
                    Attack attackBuffered = ChangeNextAttack(buffer);
                    _nextAttack.Enqueue(attackBuffered);

                    availableEnemies.Add(buffer);
                    buffer.AddAttack(attackBuffered);
                }
            }
        }

        foreach (Enemy buffer in _availableEnemies)
        {
            if (buffer.CanBeTargeted)
            {
                Attack attackBuffered = ChangeNextAttack(buffer);
                _nextAttack.Enqueue(attackBuffered);

                availableEnemies.Add(buffer);
                buffer.AddAttack(attackBuffered);

                if (availableEnemies.Count >= numberOfEnemiesToFound)
                    break;
            }
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
    public void DesactivateTower()
    {
        DesactivateRangeDisplay();

        _collider.localScale = Vector3.one;
        _transformRange.localScale = Vector3.one;

        gameObject.SetActive(false);
    }
    #endregion
}