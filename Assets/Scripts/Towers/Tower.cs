using System;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Description")]
    [SerializeField]
    protected string _displayName;
    [SerializeField]
    protected int _price;
    [SerializeField]
    protected Sprite _icon;



    [Header("Damage Related")]
    [SerializeField]
    protected float _timeBetweenShots;
    [SerializeField]
    protected int _damage;
    [SerializeField]
    protected float _armorThrough;
    [SerializeField]
    protected int _numberOfShots;
    [SerializeField]
    protected GameObject _projectileUsed;
    [SerializeField]
    protected float _range;


    [Header("In game")]
    [SerializeField]
    protected Transform _transformRange;
    [SerializeField]
    protected GameObject _selector;


    protected InformationUIController _informationUIController;
    protected TowerSlot _currentSlot;
    protected RessourceController _ressourceController;
    protected bool _sellerActive = false; 
    protected List<Enemy> _availableEnemies = new List<Enemy>();
    protected CapsuleCollider2D _collider;


    public void Initialize(TowerSlot newSlot, RessourceController newRessourceController, InformationUIController newInformationUIController)
    {
        _informationUIController = newInformationUIController;
        _ressourceController = newRessourceController;
        _currentSlot = newSlot;

        _transformRange.localScale *= _range; 
    }



    protected void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            _availableEnemies.Add(enemy);

        Debug.Log("one more");
    }


    protected void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            if (_availableEnemies.Contains(enemy))
                _availableEnemies.Remove(enemy);
        }

        Debug.Log("one left");
    }



    private void OnMouseDown()
    {
        Vector2 cameraScreen = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 finalPosition = new Vector2(cameraScreen.x - Screen.width / 2, cameraScreen.y - Screen.height / 2);

        if (!_sellerActive)
        {
            _informationUIController.ActivateTowerSellButton(finalPosition, this, Mathf.FloorToInt(_price / 4));
            _informationUIController.SetTowerInformation(_icon, _displayName, _damage, _armorThrough, _timeBetweenShots);

            ActivateRangeDisplay();
        }
        else
        {
            _informationUIController.DisableTowerInformation();
            _informationUIController.DisableTowerSellButton();

            DesactivateRangeDisplay();
        }

        _sellerActive = !_sellerActive;
    }


    public void ResellTower()
    {
        _ressourceController.AddGold(Mathf.FloorToInt(_price / 4));
        _informationUIController.DisableTowerInformation();

        _informationUIController.DisableTowerSellButton();
        _currentSlot.ResetSlot();
        Destroy(gameObject);
    }
    

    protected void Shoot()
    {
        //TO DO
    }



    protected void SortEnemies()
    {
        Array.Sort(_availableEnemies.ToArray(), (a, b) => a.GetPathPercentage().CompareTo(b.GetPathPercentage()));

        foreach(Enemy current in _availableEnemies)
            Debug.Log(current + " : " + current.GetPathPercentage());
    }


    public void ActivateRangeDisplay()
    {
        _transformRange.gameObject.SetActive(true);
        _selector.SetActive(true);
    }


    public void DesactivateRangeDisplay()
    {
        _transformRange.gameObject.SetActive(false);
        _selector.SetActive(false);
    }


    public void ResetTowerDisplay()
    {
        DesactivateRangeDisplay();
        _sellerActive = false;
    }



    public string GetName() { return _displayName; }

    public int GetPrice() { return _price; }

    public float GetTimeBetweenShots() { return _timeBetweenShots; }

    public int GetDamage() { return _damage; }

    public float GetArmorThrough() { return _armorThrough; }

    public int GetNumberOfShots() { return _numberOfShots; }

    public GameObject GetProjectileUsed() { return _projectileUsed; }

    public float GetRange() { return _range; }

    public Sprite GetIcon() { return _icon; }
}