using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tower class, main object of the game.
/// </summary>
/// <remarks>Needs a static depth manager</remarks>
[RequireComponent(typeof(StaticDepthManager))]
public class Tower : MonoBehaviour
{
    [SerializeField]
    protected TowerData _towerData;
    public TowerData Data { get => _towerData; }


    /// <summary>
    /// Range display.
    /// </summary>
    protected Transform _transformRange;
    protected Vector3 _initialRangeScale;

    /// <summary>
    /// Collider used in range detection.
    /// </summary>
    protected Transform _collider;
    protected Vector3 _initialColliderScale;

    protected SpriteRenderer _spriteRenderer;

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

    public int CumulativeGold { get; protected set; } = 0;



    protected void Awake()
    {
        _transformRange = transform.Find("Range");
        _collider = transform.Find("Collider");

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _shadowSpriteRenderer = transform.Find("Shadow").GetComponent<SpriteRenderer>();

        _selector = transform.Find("Selecter").gameObject;

        _initialColliderScale = _collider.localScale;
        _initialRangeScale = _transformRange.localScale;
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


    private void SetDefaultValues(TowerData newData)
    {
        _selector.SetActive(false);
        _transformRange.gameObject.SetActive(false);

        _towerData = newData;
        CheckAugmentation();

        CumulativeGold += Data.Price;

        _spriteRenderer.sprite = Data.Sprite;
        _shadowSpriteRenderer.sprite = Data.Shadow;

        _transformRange.localScale = _initialRangeScale * Data.Range;
        _collider.localScale = _initialColliderScale * (0.9f * Data.Range);

        _collider.GetComponent<TowerCollider>().ParentTower = this;
    }


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
            _projectilePool.GetOneProjectile().Initialize(Data, current, _projectilePool, transform);

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

        _ressourceController.AddGold(Mathf.FloorToInt((CumulativeGold * Data.ResellPriceFactor) / 4), false);

        _backgroundSelecter.DisableTowerInformation();
        _backgroundSelecter.DisableTowerSellButton();

        _currentSlot.ResetSlot();
        _towerPool.AddOneTower(gameObject);
    }


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

    protected virtual void UpgradeSpecialBehavior() { }

    /// <summary>
    /// Method used to add a spec to the tower.
    /// </summary>
    public virtual void AddSpec(TowerSpec newSpec)
    {

    }

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


    protected virtual void LevelOneAugmentation() { }


    protected virtual void LevelTwoAugmentation() { }


    protected virtual void LevelThreeAugmentation() { }


    protected virtual void LevelFourAugmentation() { }


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
    /// <returns>A list of foound enemies</returns>
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
                    availableEnemies.Add(buffer);
                    buffer.AddAttack(Data);
                }
            }
        }

        foreach (Enemy buffer in _availableEnemies)
        {
            if (buffer.CanBeTargeted)
            {
                availableEnemies.Add(buffer);
                buffer.AddAttack(Data);

                if (availableEnemies.Count >= numberOfEnemiesToFound)
                    break;
            }
        }

        return availableEnemies;
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


    public void DesactivateTower()
    {
        DesactivateRangeDisplay();

        _collider.localScale = _initialColliderScale;
        _transformRange.localScale = _initialRangeScale;

        gameObject.SetActive(false);
    }

    #endregion
}