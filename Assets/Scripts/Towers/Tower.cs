﻿using System;
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
    [SerializeField]
    protected GameObject _collider;
    [SerializeField]
    protected bool _canHitFlying;


    [Header("In game")]
    [SerializeField]
    protected Transform _transformRange;
    [SerializeField]
    protected GameObject _selector;


    private BackgroudSelecter _backgroundSelecter;
    protected TowerSlot _currentSlot;
    protected RessourceController _ressourceController;
    protected bool _sellerActive = false; 
    protected List<Enemy> _availableEnemies = new List<Enemy>();



    public void Initialize(TowerSlot newSlot, RessourceController newRessourceController, BackgroudSelecter newBackgroundSelecter)
    {
        _backgroundSelecter = newBackgroundSelecter;
        _ressourceController = newRessourceController;
        _currentSlot = newSlot;

        _transformRange.localScale *= _range;
        _collider.transform.localScale *= _range;
    }



    /*Upgrades and Money related*/
    #region
    public void ResellTower()
    {
        _ressourceController.AddGold(Mathf.FloorToInt(_price / 4));
        _backgroundSelecter.DisableTowerInformation();

        _backgroundSelecter.DisableTowerSellButton();
        _currentSlot.ResetSlot();
        Destroy(gameObject);
    }


    public void UpgradeTower()
    {
        //TO DO
    }
    #endregion



    /*Enemies interaction*/
    #region
    public void AddEnemy(Enemy enemy)
    {
        if(!(!_canHitFlying && enemy.GetFlying()))
            _availableEnemies.Add(enemy);
    }


    public void RemoveEnemy(Enemy enemy)
    {
        if (_availableEnemies.Contains(enemy))
            _availableEnemies.Remove(enemy);
    }



    protected void SortEnemies()
    {
        Array.Sort(_availableEnemies.ToArray(), (a, b) => a.GetPathPercentage().CompareTo(b.GetPathPercentage()));
    }
    #endregion



    /*Reset related*/
    #region
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

    public void RevertSellerActive() { _sellerActive = !_sellerActive; }
    #endregion



    /*Getters*/
    #region
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