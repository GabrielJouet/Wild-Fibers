﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tower class, main object of the game.
/// </summary>
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
        _selector.SetActive(false);
        _transformRange.gameObject.SetActive(false);

        _towerData = newData;
        CumulativeGold += _towerData.Price;

        _spriteRenderer.sprite = _towerData.Sprite;
        _shadowSpriteRenderer.sprite = _towerData.Shadow;

        _backgroundSelecter = newBackgroundSelecter;
        _ressourceController = newRessourceController;
        _projectilePool = newPool;
        _currentSlot = newSlot;
        _towerPool = newTowerPool;

        transform.position = newSlot.transform.position;

        _transformRange.localScale *= _towerData.Range;
        _collider.localScale *= (0.9f * _towerData.Range);

        SpecialBehavior();
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

        int numberOfStrikes = _availableEnemies.Count < _towerData.Shots ? _availableEnemies.Count : _towerData.Shots;

        if (!_towerData.ShotsRandomly)
            SortEnemies();

        foreach (Enemy current in RecoverAvailableEnemies(numberOfStrikes, _towerData.ShotsRandomly))
            _projectilePool.GetOneProjectile().Initialize(_towerData, current, _projectilePool, transform);

        yield return new WaitForSeconds(_towerData.TimeShots);
        _coroutineStarted = false;
    }



    #region Upgrades and Money related
    /// <summary>
    /// Method used to resell the tower.
    /// </summary>
    public void ResellTower()
    {
        _ressourceController.AddGold(Mathf.FloorToInt(CumulativeGold / 4), false);

        _backgroundSelecter.DisableTowerInformation();
        _backgroundSelecter.DisableTowerSellButton();

        _currentSlot.ResetSlot();
        _towerPool.AddOneTower(gameObject);
    }

    /// <summary>
    /// Method used to upgrade the tower.
    /// </summary>
    public void UpgradeTower(TowerData newData)
    {
        _ressourceController.RemoveGold(newData.Price);
        _towerData = newData;
        CumulativeGold += _towerData.Price;

        _transformRange.localScale = _initialRangeScale * _towerData.Range;
        _collider.localScale = _initialColliderScale * (0.9f * _towerData.Range);

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
    #endregion 



    #region Enemies interaction
    /// <summary>
    /// Method used to add an enemy to the list.
    /// </summary>
    /// <param name="enemy">The enemy to add</param>
    public void AddEnemy(Enemy enemy)
    {
        if (!(!_towerData.HitFlying && enemy.Flying))
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
    protected List<Enemy> RecoverAvailableEnemies(int numberOfEnemiesToFound, bool random)
    {
        List<Enemy> availableEnemies = new List<Enemy>();

        if (random)
            _availableEnemies.Shuffle();

        for(int i = 0; i < numberOfEnemiesToFound; i ++)
        {
            foreach (Enemy buffer in _availableEnemies)
            {
                if (buffer.AlreadyAimed || availableEnemies.Contains(buffer))
                    continue;
                else
                {
                    availableEnemies.Add(buffer);

                    if (!buffer.CanSurvive(_towerData.Damage, _towerData.ArmorThrough))
                        buffer.AlreadyAimed = true;

                    break;
                }
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
    #endregion
}