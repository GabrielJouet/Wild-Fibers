using System.Collections.Generic;
using UnityEngine;

/*
 * Tower class is the main object of the game, they shoot enemies
 */
public class Tower : MonoBehaviour
{
    [Header("Description")]
    //Name display on UI
    [SerializeField]
    protected string _displayName;

    //Price of the tower to build it
    [SerializeField]
    protected int _price;

    //Icon used in buttons and UI
    [SerializeField]
    protected Sprite _icon;



    [Header("Damage Related")]
    //How many seconds between each attacks?
    [SerializeField]
    protected float _timeBetweenShots;

    //How much damage an attack does?
    [SerializeField]
    protected int _damage;

    //How much armor an attack breaks?
    [SerializeField]
    protected float _armorThrough;

    //Number of projectiles in one attack
    [SerializeField]
    protected int _numberOfShots;

    //Projectile used in attack
    [SerializeField]
    protected GameObject _projectileUsed;

    //Tower range
    [SerializeField]
    protected float _range;

    //Tower Collider used to recover enemies
    [SerializeField]
    protected GameObject _collider;

    //Can the tower hit flying targets?
    [SerializeField]
    protected bool _canHitFlying;


    [Header("In game")]
    //Range display object
    [SerializeField]
    protected Transform _transformRange;

    //Selector object
    [SerializeField]
    protected GameObject _selector;


    //Information UI
    private BackgroudSelecter _backgroundSelecter;

    //Which tower slot the tower is based on?
    protected TowerSlot _currentSlot;

    //Ressource controller used to record money
    protected RessourceController _ressourceController;

    //Does the seller UI is active?
    protected bool _sellerActive = false; 

    //List of in-range enemies
    protected List<Enemy> _availableEnemies = new List<Enemy>();

    protected bool _coroutineStarted = false;

    protected ProjectilePool _projectilePool;

    protected TowerPool _towerPool;

    protected Vector2 _colliderSize = Vector2.zero;



    //Method used to initialize class (like a constructor)
    //
    //Parameters => newSlot, the tower slot related to this tower
    //              newRessourceController, ressource controller used in this game
    //              newBackgroundSelecter, used to display UI information
    //              newPool, used to recover projectiles
    //              newTowerPool, used when destroyed
    public void Initialize(TowerSlot newSlot, RessourceController newRessourceController, BackgroudSelecter newBackgroundSelecter, ProjectilePool newPool, TowerPool newTowerPool)
    {
        StopAllCoroutines();
        _availableEnemies.Clear();

        //We set variables
        _backgroundSelecter = newBackgroundSelecter;
        _ressourceController = newRessourceController;
        _projectilePool = newPool;
        _currentSlot = newSlot;
        _towerPool = newTowerPool;

        //And we change tower range 
        transform.position = newSlot.transform.position;

        if (_colliderSize == Vector2.zero)
            _colliderSize = _collider.transform.localScale * _range;

        _transformRange.localScale = _colliderSize;
        _collider.transform.localScale = _colliderSize;
    }



    #region Upgrades and Money related
    //Method used to resell a tower and destroy it
    public void ResellTower()
    {
        _ressourceController.AddGold(Mathf.FloorToInt(_price / 4));
        _backgroundSelecter.DisableTowerInformation();

        _backgroundSelecter.DisableTowerSellButton();
        _currentSlot.ResetSlot();
        _towerPool.AddOneTower(this);
    }


    //Method used to upgrade a tower
    public void UpgradeTower()
    {
        //TO DO
    }
    #endregion 



    #region Enemies interaction
    //Method used to add one enemy from its list
    public void AddEnemy(Enemy enemy)
    {
        if(!(!_canHitFlying && enemy.GetFlying()))
            _availableEnemies.Add(enemy);
    }


    //Method used to remove one enemy from its list
    public void RemoveEnemy(Enemy enemy)
    {
        if (_availableEnemies.Contains(enemy))
            _availableEnemies.Remove(enemy);
    }


    //Method used to sort enemies by their position toward the end of the path
    protected void SortEnemies()
    {
        _availableEnemies.Sort((a, b) => b.GetPathPercentage().CompareTo(a.GetPathPercentage()));
    }

     
    protected List<Enemy> RecoverAvailableEnemies(int numberOfEnemiesToFound)
    {
        List<Enemy> availableEnemies = new List<Enemy>();
        int j;

        for(int i = 0; i < numberOfEnemiesToFound; i ++)
        {
            j = -1;

            do
                j++;
            while (_availableEnemies[j].GetAlreadyAimed() || availableEnemies.Contains(_availableEnemies[j]));

            availableEnemies.Add(_availableEnemies[j]);

            if (!_availableEnemies[j].CanSurvive(_damage, _armorThrough))
                _availableEnemies[j].SetAlreadyAimed();
        }

        return availableEnemies;
    }
    #endregion



    #region Reset related
    //Method used to activate range display (when selected)
    public void ActivateRangeDisplay()
    {
        _transformRange.gameObject.SetActive(true);
        _selector.SetActive(true);
    }


    //Method used to desactivate range display (when no longer selected)
    public void DesactivateRangeDisplay()
    {
        _transformRange.gameObject.SetActive(false);
        _selector.SetActive(false);
    }


    //Method used to desactivate tower display (UI)
    public void ResetTowerDisplay()
    {
        DesactivateRangeDisplay();
        _sellerActive = false;
    }


    //Method used to revert seller UI state
    public void RevertSellerActive() { _sellerActive = !_sellerActive; }
    #endregion



    #region Getters
    public string GetName() { return _displayName; }

    public int GetPrice() { return _price; }

    public float GetTimeBetweenShots() { return _timeBetweenShots; }

    public int GetDamage() { return _damage; }

    public float GetArmorThrough() { return _armorThrough; }

    public int GetNumberOfShots() { return _numberOfShots; }

    public GameObject GetProjectileUsed() { return _projectileUsed; }

    public float GetRange() { return _range; }

    public Sprite GetIcon() { return _icon; }

    public bool GetSellerActive() { return _sellerActive; }
    #endregion
}