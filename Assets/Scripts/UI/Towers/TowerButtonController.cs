using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to handle tower buttons.
/// </summary>
public class TowerButtonController : MonoBehaviour
{
    [Header("Components")]

    /// <summary>
    /// Player controller handles towers.
    /// </summary>
    [SerializeField]
    private PlayerController _playerController;

    /// <summary>
    /// Resource controller handles gold and lives.
    /// </summary>
    [SerializeField]
    private RessourceController _ressourceController;


    [Header("Button Elements")]

    /// <summary>
    /// List of all tower buttons.
    /// </summary>
    [SerializeField]
    private List<TowerButton> _towerPurchaseButtons;

    /// <summary>
    /// Sell button.
    /// </summary>
    [SerializeField]
    private TowerButton _sellButton;

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


    private Tower _currentTower;



    /// <summary>
    /// Start method, called at first.
    /// </summary>
    private void Start()
    {
        List<TowerData> buffer = _playerController.Towers;

        for (int i = 0; i < _towerPurchaseButtons.Count; i ++)
            _towerPurchaseButtons[i].Initialize(buffer[i].Icon, buffer[i].Price, _ressourceController);
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
            TowerData buffer = _playerController.Towers[i];
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
    /// <param name="newPrice">The new price of the tower</param>
    public void ActivateTowerUpgradeButtons(Vector2 newPosition, Tower newTower, int newPrice)
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

        _sellButton.Initialize(newPrice);
        _sellButton.gameObject.SetActive(true);
        _sellButton.ChangeBehavior(() => _currentTower.ResellTower());
        _sellButton.UpdateState(true);
    }


    private void UpdateTowerUpgradeButtons(int buttonIndex, int index)
    {
        _towerUpgradesButtons[buttonIndex].gameObject.SetActive(true);
        _towerUpgradesButtons[buttonIndex].Initialize(_currentTower.Data.Upgrades[index].Icon, _currentTower.Data.Upgrades[index].Price, _ressourceController);

        _towerUpgradesButtons[buttonIndex].ChangeBehavior(() => _currentTower.UpgradeTower(_currentTower.Data.Upgrades[index]));

        _towerUpgradesButtons[buttonIndex].UpdateState(_currentTower.Data.Upgrades[index].Price < _ressourceController.GoldCount);
    }


    private void UpdateTowerUpgradeButton()
    {
        _towerUpgradesButtons[1].gameObject.SetActive(true);
        _towerUpgradesButtons[1].Initialize(_currentTower.Data.Upgrades[0].Icon, _currentTower.Data.Upgrades[0].Price, _ressourceController);

        _towerUpgradesButtons[1].ChangeBehavior(() => _currentTower.UpgradeTower(_currentTower.Data.Upgrades[0]));
        _towerUpgradesButtons[1].UpdateState(_currentTower.Data.Upgrades[0].Price < _ressourceController.GoldCount);
    }


    private void UpdateTowerSpecButtons()
    {
        for (int i = 0; i < _towerUpgradesButtons.Count; i++)
        {
            _towerUpgradesButtons[i].gameObject.SetActive(true);

            _towerUpgradesButtons[i].ChangeBehavior(() => _currentTower.AddSpec(_currentTower.Data.Specs[i]));
            _towerUpgradesButtons[i].UpdateState(_currentTower.Data.Specs[i].Price < _ressourceController.GoldCount);
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