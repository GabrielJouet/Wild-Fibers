using System.Collections.Generic;
using Levels;
using Towers;
using Towers.Upgrades;
using UnityEngine;

namespace UI.InGame
{
    /// <summary>
    /// Class used to handle tower buttons.
    /// </summary>
    public class TowerButtonController : MonoBehaviour
    {
        [Header("Components")]

        /// <summary>
        /// Level controller handles level.
        /// </summary>
        [SerializeField]
        private LevelController levelController;


        [Header("Button Elements")]

        /// <summary>
        /// List of all tower buttons.
        /// </summary>
        [SerializeField]
        private List<TowerButton> towerPurchaseButtons;

        /// <summary>
        /// Up description object.
        /// </summary>
        [SerializeField]
        private GameObject upDescription;

        /// <summary>
        /// Down description object.
        /// </summary>
        [SerializeField]
        private GameObject downDescription;

        /// <summary>
        /// Sell button.
        /// </summary>
        [SerializeField]
        private TowerButton sellButton;

        /// <summary>
        /// Tower upgrades buttons.
        /// </summary>
        [SerializeField]
        private List<TowerButton> towerUpgradesButtons;


        /// <summary>
        /// Rect transform used in movement.
        /// </summary>
        [SerializeField]
        private RectTransform rectTransform;


        /// <summary>
        /// Does the sell button is active?
        /// </summary>
        public bool SellButtonActive => sellButton.gameObject.activeSelf;

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
            _squadController = Controller.Instance.SquadController;
        }


        /// <summary>
        /// Start method, called at first, after Awake.
        /// </summary>
        private void Start()
        {
            List<Tower> buffer = _squadController.CurrentSquad.Towers;
            List<Tower> blockedTowers = Controller.Instance.SaveController.LoadedLevel.BlockedTowers;

            for (int i = 0; i < towerPurchaseButtons.Count; i ++)
            {
                bool found = false;
                foreach (Tower blockedTower in blockedTowers)
                {
                    if (blockedTower.name == buffer[i].name)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                    towerPurchaseButtons[i].Lock();
            }
        }


        /// <summary>
        /// Method used to activate the purchase buttons.
        /// </summary>
        /// <param name="newPosition">The new position of the buttons</param>
        /// <param name="newUsedTowerSlot">The related tower slot</param>
        public void ActivatePurchaseButtons(Vector2 newPosition, TowerSlot newUsedTowerSlot)
        {
            rectTransform.localPosition = newPosition;

            for (int i = 0; i < towerPurchaseButtons.Count; i++)
            {
                Tower buffer = _squadController.CurrentSquad.Towers[i];

                towerPurchaseButtons[i].gameObject.SetActive(true);
                towerPurchaseButtons[i].Initialize(buffer.Icon, buffer.Price, buffer.Description, newUsedTowerSlot.UpDescription ? upDescription : downDescription);
                towerPurchaseButtons[i].ChangeBehavior(() => newUsedTowerSlot.ChooseTower(buffer));
            }
        }


        /// <summary>
        /// Method used to deactivate purchase buttons.
        /// </summary>
        public void DeactivatePurchaseButtons()
        {
            for (int i = 0; i < towerPurchaseButtons.Count; i++)
                towerPurchaseButtons[i].gameObject.SetActive(false);
        }


        /// <summary>
        /// Method used to activate the sell button.
        /// </summary>
        /// <param name="newPosition">The new position of the buttons</param>
        /// <param name="newTower">The related tower</param>
        public void ActivateTowerUpgradeButtons(Vector2 newPosition, Tower newTower)
        {
            _currentTower = newTower;
            rectTransform.localPosition = newPosition;
            GameObject descriptionObject = newTower.Slot.UpDescription ? upDescription : downDescription;

            if (_currentTower.Upgrades.Count > 1)
            {
                UpdateTowerUpgradeButtons(0, 0, descriptionObject);
                UpdateTowerUpgradeButtons(2, 1, descriptionObject);
            }
            else if (_currentTower.Upgrades.Count == 1)
                UpdateTowerUpgradeButton(descriptionObject);
            else if (_currentTower.Specs.Count > 0)
                UpdateTowerSpecButtons(descriptionObject);

            sellButton.gameObject.SetActive(true);
            sellButton.Initialize(Mathf.FloorToInt((newTower.CumulativeGold * newTower.ResellPriceFactor) * 0.65f), descriptionObject);
            sellButton.ChangeBehavior(() => _currentTower.ResellTower());
        }


        /// <summary>
        /// Method called to update the tower upgrades buttons.
        /// </summary>
        /// <param name="buttonIndex">The button to update</param>
        /// <param name="index">The upgrade index</param>
        /// <param name="descriptionObject">The game object used for description display</param>
        private void UpdateTowerUpgradeButtons(int buttonIndex, int index, GameObject descriptionObject)
        {
            Tower buffer = _currentTower.Upgrades[index];
            towerUpgradesButtons[buttonIndex].gameObject.SetActive(true);
            towerUpgradesButtons[buttonIndex].Initialize(buffer.Icon, buffer.Price, buffer.Description, descriptionObject);

            if (levelController.LoadedLevel.TowerLevel > 0)
                towerUpgradesButtons[buttonIndex].ChangeBehavior(() => _currentTower.UpgradeTower(buffer));
            else
            {
                towerUpgradesButtons[buttonIndex].Lock();
                towerUpgradesButtons[buttonIndex].UpdateState(false);
            }
        }


        /// <summary>
        /// Method called to update tower upgrade button.
        /// </summary>
        /// <param name="descriptionObject">The game object used for description display</param>
        /// <remarks>The previous method update 2 buttons, whereas, this one only activate one</remarks>
        private void UpdateTowerUpgradeButton(GameObject descriptionObject)
        {
            Tower buffer = _currentTower.Upgrades[0];
            towerUpgradesButtons[1].gameObject.SetActive(true);
            towerUpgradesButtons[1].Initialize(buffer.Icon, buffer.Price, buffer.Description, descriptionObject);

            if (levelController.LoadedLevel.TowerLevel > 1)
                towerUpgradesButtons[1].ChangeBehavior(() => _currentTower.UpgradeTower(buffer));
            else
            {
                towerUpgradesButtons[1].Lock();
                towerUpgradesButtons[1].UpdateState(false);
            }
        }


        /// <summary>
        /// Method called to update tower specs buttons.
        /// </summary>
        /// <param name="descriptionObject">The game object used for description display</param>
        private void UpdateTowerSpecButtons(GameObject descriptionObject)
        {
            for (int i = 0; i < towerUpgradesButtons.Count; i++)
            {
                TowerSpec buffer = _currentTower.Specs[i];

                towerUpgradesButtons[i].gameObject.SetActive(true);
                towerUpgradesButtons[i].Initialize(buffer.Icon, buffer.Price, buffer.Description, descriptionObject);

                if (levelController.LoadedLevel.TowerLevel > 1)
                    towerUpgradesButtons[i].ChangeBehavior(() => _currentTower.AddSpec(_currentTower.Specs[i]));
                else
                {
                    towerUpgradesButtons[i].Lock();
                    towerUpgradesButtons[1].UpdateState(false);
                }
            }
        }

        /// <summary>
        /// Method used to deactivate sell button.
        /// </summary>
        public void DeactivateTowerUpgradeButtons()
        {
            for (int i = 0; i < towerUpgradesButtons.Count; i++)
                towerUpgradesButtons[i].gameObject.SetActive(false);

            sellButton.gameObject.SetActive(false);
        }
    }
}