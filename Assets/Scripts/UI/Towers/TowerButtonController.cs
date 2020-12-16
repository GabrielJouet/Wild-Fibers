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
    private List<TowerButton> _towerButtons;

    /// <summary>
    /// Sell button.
    /// </summary>
    [SerializeField]
    private TowerButton _sellButton;

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
    /// Start method, called at first.
    /// </summary>
    private void Start()
    {
        List<Tower> buffer = _playerController.Towers;

        for (int i = 0; i < _towerButtons.Count; i ++)
            _towerButtons[i].Initialize(buffer[i].Icon, buffer[i].Price);
    }


    /// <summary>
    /// Update method, called each frame.
    /// </summary>
    private void Update()
    {
        if (_towerButtons[0].gameObject.activeSelf)
        {
            for (int i = 0; i < _towerButtons.Count; i++)
            {
                Tower buffer = _playerController.Towers[i];

                _towerButtons[i].UpdateState(buffer.Price < _ressourceController.GoldCount);
            }
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

        for (int i = 0; i < _towerButtons.Count; i++)
        {
            Tower buffer = _playerController.Towers[i];
            _towerButtons[i].gameObject.SetActive(true);

            _towerButtons[i].ChangeBehavior(() => newUsedTowerSlot.ChooseTower(buffer));
            _sellButton.UpdateState(buffer.Price < _ressourceController.GoldCount);
        }
    }


    /// <summary>
    /// Method used to desactivate purchase buttons.
    /// </summary>
    public void DesactivatePurchaseButtons()
    {
        for (int i = 0; i < _towerButtons.Count; i++)
            _towerButtons[i].gameObject.SetActive(false);
    }


    /// <summary>
    /// Method used to activate the sell button.
    /// </summary>
    /// <param name="newPosition">The new position of the buttons</param>
    /// <param name="newTower">The related tower</param>
    /// <param name="newPrice">The new price of the tower</param>
    public void ActivateSellButton(Vector2 newPosition, Tower newTower, int newPrice)
    {
        _rectTransform.localPosition = newPosition;

        _sellButton.Initialize(newPrice);
        _sellButton.gameObject.SetActive(true);
        _sellButton.ChangeBehavior(() => newTower.ResellTower());
        _sellButton.UpdateState(true);
    }


    /// <summary>
    /// Method used to desactivate sell button.
    /// </summary>
    public void DesactivateSellButton()
    {
        _sellButton.gameObject.SetActive(false);
    }
}