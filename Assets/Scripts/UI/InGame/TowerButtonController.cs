﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to handle tower buttons.
/// </summary>
public class TowerButtonController : MonoBehaviour
{
    [Header("Components")]

    /// <summary>
    /// Resource controller handles gold and lives.
    /// </summary>
    [SerializeField]
    private RessourceController _ressourceController;

    /// <summary>
    /// Level controller handles level.
    /// </summary>
    [SerializeField]
    private LevelController _levelController;


    [Header("Button Elements")]

    /// <summary>
    /// List of all tower buttons.
    /// </summary>
    [SerializeField]
    private List<TowerButton> _towerPurchaseButtons;

    /// <summary>
    /// Up description object.
    /// </summary>
    [SerializeField]
    private GameObject _upDescription;

    /// <summary>
    /// Down description object.
    /// </summary>
    [SerializeField]
    private GameObject _downDescription;

    /// <summary>
    /// Sell button.
    /// </summary>
    [SerializeField]
    private TowerButton _sellButton;

    /// <summary>
    /// Tower upgrades buttons.
    /// </summary>
    [SerializeField]
    private List<TowerButton> _towerUpgradesButtons;


    /// <summary>
    /// Rect transform used in movement.
    /// </summary>
    [SerializeField]
    private RectTransform _rectTransform;


    /// <summary>
    /// Does the sell button is active?
    /// </summary>
    public bool SellButtonActive { get => _sellButton.gameObject.activeSelf; }

    /// <summary>
    /// Buffer tower.
    /// </summary>
    private Tower _currentTower;

    /// <summary>
    /// Squad controller component.
    /// </summary>
    private SquadController _squadController;



    /// <summary>
    /// Awake method, called at first.
    /// </summary>
    private void Awake()
    {
        _squadController = Controller.Instance.SquadControl;
    }


    /// <summary>
    /// Start method, called at first, after Awake.
    /// </summary>
    private void Start()
    {
        List<TowerData> buffer = _squadController.CurrentSquad.Towers;
        List<TowerData> blockedTowers = Controller.Instance.SaveControl.LoadedLevel.BlockedTowers;

        for (int i = 0; i < _towerPurchaseButtons.Count; i ++)
        {
            bool found = false;
            foreach(TowerData blockedTower in blockedTowers)
            {
                if (blockedTower.name == buffer[i].name)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                _towerPurchaseButtons[i].Initialize(buffer[i].Icon, buffer[i].Price, _ressourceController, buffer[i].Description);
            else
                _towerPurchaseButtons[i].Lock();
        }
    }


    /// <summary>
    /// Method used to activate the purchase buttons.
    /// </summary>
    /// <param name="newPosition">The new position of the buttons</param>
    /// <param name="newUsedTowerSlot">The related tower slot</param>
    public void ActivatePurchaseButtons(Vector2 newPosition, TowerSlot newUsedTowerSlot)
    {
        _rectTransform.localPosition = newPosition;

        for (int i = 0; i < _towerPurchaseButtons.Count; i++)
        {
            TowerData buffer = _squadController.CurrentSquad.Towers[i];

            _towerPurchaseButtons[i].SetTargetDescription(newUsedTowerSlot.UpDescription? _upDescription : _downDescription);
            _towerPurchaseButtons[i].gameObject.SetActive(true);

            _towerPurchaseButtons[i].ChangeBehavior(() => newUsedTowerSlot.ChooseTower(buffer));
            _sellButton.UpdateState(buffer.Price < _ressourceController.GoldCount);
        }
    }


    /// <summary>
    /// Method used to desactivate purchase buttons.
    /// </summary>
    public void DesactivatePurchaseButtons()
    {
        for (int i = 0; i < _towerPurchaseButtons.Count; i++)
            _towerPurchaseButtons[i].gameObject.SetActive(false);
    }


    /// <summary>
    /// Method used to activate the sell button.
    /// </summary>
    /// <param name="newPosition">The new position of the buttons</param>
    /// <param name="newTower">The related tower</param>
    public void ActivateTowerUpgradeButtons(Vector2 newPosition, Tower newTower)
    {
        _currentTower = newTower;
        _rectTransform.localPosition = newPosition;
        GameObject buffer = _currentTower.gameObject;

        if (_currentTower.Data.Upgrades.Count > 1)
        {
            UpdateTowerUpgradeButtons(0, 0);
            UpdateTowerUpgradeButtons(2, 1);
        }
        else if (_currentTower.Data.Upgrades.Count == 1)
            UpdateTowerUpgradeButton();
        else if (_currentTower.Data.Specs.Count > 0)
            UpdateTowerSpecButtons();

        _sellButton.Initialize(Mathf.FloorToInt((newTower.CumulativeGold * newTower.Data.ResellPriceFactor) * 0.65f));
        _sellButton.gameObject.SetActive(true);
        _sellButton.ChangeBehavior(() => _currentTower.ResellTower());
        _sellButton.UpdateState(true);
    }


    /// <summary>
    /// Method called to update the tower upgrades buttons.
    /// </summary>
    /// <param name="buttonIndex">The button to update</param>
    /// <param name="index">The upgrade index</param>
    private void UpdateTowerUpgradeButtons(int buttonIndex, int index)
    {
        TowerData buffer = _currentTower.Data.Upgrades[index];
        _towerUpgradesButtons[buttonIndex].gameObject.SetActive(true);
        _towerUpgradesButtons[buttonIndex].Initialize(buffer.Icon, buffer.Price, _ressourceController, buffer.Description);

        if(_levelController.LoadedLevel.TowerLevel > 0)
        {
            _towerUpgradesButtons[buttonIndex].ChangeBehavior(() => _currentTower.UpgradeTower(buffer));
            _towerUpgradesButtons[buttonIndex].UpdateState(buffer.Price < _ressourceController.GoldCount);
        }
        else
        {
            _towerUpgradesButtons[buttonIndex].UpdateState(true);
            _towerUpgradesButtons[buttonIndex].Lock();
        }
    }


    /// <summary>
    /// Method called to update tower upgrade button.
    /// </summary>
    /// <remarks>The previous method update 2 buttons, whereas, this one only activate one</remarks>
    private void UpdateTowerUpgradeButton()
    {
        TowerData buffer = _currentTower.Data.Upgrades[0];
        _towerUpgradesButtons[1].gameObject.SetActive(true);
        _towerUpgradesButtons[1].Initialize(buffer.Icon, buffer.Price, _ressourceController, buffer.Description);

        if (_levelController.LoadedLevel.TowerLevel > 1)
        {
            _towerUpgradesButtons[1].ChangeBehavior(() => _currentTower.UpgradeTower(buffer));
            _towerUpgradesButtons[1].UpdateState(buffer.Price < _ressourceController.GoldCount);
        }
        else
        {
            _towerUpgradesButtons[1].UpdateState(true);
            _towerUpgradesButtons[1].Lock();
        }
    }


    /// <summary>
    /// Method called to update tower specs buttons.
    /// </summary>
    private void UpdateTowerSpecButtons()
    {
        for (int i = 0; i < _towerUpgradesButtons.Count; i++)
        {
            TowerSpec buffer = _currentTower.Data.Specs[i];

            _towerUpgradesButtons[i].gameObject.SetActive(true);
            _towerUpgradesButtons[1].Initialize(buffer.Icon, buffer.Price, _ressourceController, buffer.Description);

            if (_levelController.LoadedLevel.TowerLevel > 1)
            {
                _towerUpgradesButtons[i].ChangeBehavior(() => _currentTower.AddSpec(_currentTower.Data.Specs[i]));
                _towerUpgradesButtons[i].UpdateState(_currentTower.Data.Specs[i].Price < _ressourceController.GoldCount);
            }
            else
            {
                _towerUpgradesButtons[i].UpdateState(true);
                _towerUpgradesButtons[i].Lock();
            }
        }
    }

    /// <summary>
    /// Method used to desactivate sell button.
    /// </summary>
    public void DesactivateTowerUpgradeButtons()
    {
        for (int i = 0; i < _towerUpgradesButtons.Count; i++)
            _towerUpgradesButtons[i].gameObject.SetActive(false);

        _sellButton.gameObject.SetActive(false);
    }
}