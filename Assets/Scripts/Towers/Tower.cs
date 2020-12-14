using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tower class, main object of the game.
/// </summary>
public class Tower : MonoBehaviour
{
    [Header("Description")]

    /// <summary>
    /// Display name.
    /// </summary>
    [SerializeField]
    protected string _displayName;
    public string Name { get => _displayName; }

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



    [Header("Damage Related")]

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
    /// Number of projectile per attack.
    /// </summary>
    [SerializeField]
    protected int _numberOfShots;
    public int Shots { get => _numberOfShots; }

    /// <summary>
    /// Projectile used in attack.
    /// </summary>
    [SerializeField]
    protected GameObject _projectileUsed;
    public GameObject Projectile { get => _projectileUsed; }

    /// <summary>
    /// Range of the tower.
    /// </summary>
    [SerializeField]
    protected float _range;

    /// <summary>
    /// Collider used in range detection.
    /// </summary>
    [SerializeField]
    protected GameObject _collider;

    /// <summary>
    /// Can the tower hits flying target?
    /// </summary>
    [SerializeField]
    protected bool _canHitFlying;


    [Header("In game")]

    /// <summary>
    /// Range display.
    /// </summary>
    [SerializeField]
    protected Transform _transformRange;

    /// <summary>
    /// Selector object used when clicked.
    /// </summary>
    [SerializeField]
    protected GameObject _selector;


    /// <summary>
    /// Information UI object.
    /// </summary>
    private BackgroudSelecter _backgroundSelecter;

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



    /// <summary>
    /// Method used to initialize.
    /// </summary>
    /// <param name="newSlot">The parent slot</param>
    /// <param name="newRessourceController">The resource controller</param>
    /// <param name="newBackgroundSelecter">The background selecter component</param>
    /// <param name="newPool">The new projectile pool</param>
    /// <param name="newTowerPool">The new tower pool</param>
    public void Initialize(TowerSlot newSlot, RessourceController newRessourceController, BackgroudSelecter newBackgroundSelecter, ProjectilePool newPool, TowerPool newTowerPool)
    {
        StopAllCoroutines();
        _availableEnemies.Clear();
        _coroutineStarted = false;

        _backgroundSelecter = newBackgroundSelecter;
        _ressourceController = newRessourceController;
        _projectilePool = newPool;
        _currentSlot = newSlot;
        _towerPool = newTowerPool;

        transform.position = newSlot.transform.position;
        _transformRange.localScale = _collider.transform.localScale * _range;
        _collider.transform.localScale = _collider.transform.localScale * _range;
    }



    #region Upgrades and Money related
    /// <summary>
    /// Method used to resell the tower.
    /// </summary>
    public void ResellTower()
    {
        _ressourceController.AddGold(Mathf.FloorToInt(_price / 4));
        _backgroundSelecter.DisableTowerInformation();

        _backgroundSelecter.DisableTowerSellButton();
        _currentSlot.ResetSlot();
        _towerPool.AddOneTower(this);
    }


    /// <summary>
    /// Method used to upgrade the tower.
    /// </summary>
    public void UpgradeTower()
    {
        //TO DO
    }
    #endregion 



    #region Enemies interaction
    /// <summary>
    /// Method used to add an enemy to the list.
    /// </summary>
    /// <param name="enemy">The enemy to add</param>
    public void AddEnemy(Enemy enemy)
    {
        if(!(!_canHitFlying && enemy.Flying))
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
        int j;

        for(int i = 0; i < numberOfEnemiesToFound; i ++)
        {
            j = -1;

            do
                j++;
            while (j < _availableEnemies.Count && (_availableEnemies[j].AlreadyAimed || availableEnemies.Contains(_availableEnemies[j])));

            if(j < _availableEnemies.Count)
            {
                availableEnemies.Add(_availableEnemies[j]);

                if (!_availableEnemies[j].CanSurvive(_damage, _armorThrough))
                    _availableEnemies[j].AlreadyAimed = true;
            }
            else
                availableEnemies.Add(_availableEnemies[i]);
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