using Enemies.Enemy_Types;
using TMPro;
using Towers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame
{
    /// <summary>
    /// Class that handles every interactional UI.
    /// </summary>
    public class BackgroudSelecter : MonoBehaviour
    {
        /// <summary>
        /// Instance of itself.
        /// </summary>
        public static BackgroudSelecter Instance { get; private set; }
        
        [Header("Tower related objects")]

        /// <summary>
        /// Tower information panel object.
        /// </summary>
        [SerializeField]
        private GameObject towerInformationPanel;

        /// <summary>
        /// Text component containing the tower name.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI towerName;

        /// <summary>
        /// Text component containing the tower damage.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI damageText;

        /// <summary>
        /// Text component containing the break armor value.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI breakArmorText;

        /// <summary>
        /// Text component containing the fire rate value.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI fireRateText;


        [Header("Enemy related objects")]

        /// <summary>
        /// Enemy information panel object.
        /// </summary>
        [SerializeField]
        private GameObject enemyInformationPanel;

        /// <summary>
        /// Text component containing the enemy name.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI enemyName;

        /// <summary>
        /// Text component containing the enemy life value.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI lifeValue;

        /// <summary>
        /// Text component containing the armor value.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI armorValue;
        
        /// <summary>
        /// Text component containing the number of lives lost.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI livesLostValue;


        [Header("Tower buttons related")]

        /// <summary>
        /// Tower choose button controller.
        /// </summary>
        [SerializeField]
        private TowerButtonController towerButtonController;

        /// <summary>
        /// UI canvas.
        /// </summary>
        [SerializeField]
        private RectTransform canvas;


        /// <summary>
        /// Previous object clicked.
        /// </summary>
        private Enemy _previousEnemy;
        private Tower _previousTower;
        private TowerSlot _previousSlot;


        /// <summary>
        /// Actual camera used.
        /// </summary>
        private Camera _currentCamera;


        private void Start()
        {
            _currentCamera = Camera.main;
            Instance = this;
        }


        /// <summary>
        /// Update method called each frame.
        /// </summary>
        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(_currentCamera.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 90);

                if (hit.collider)
                {
                    if (hit.collider.TryGetComponent(out Enemy selectedEnemy))
                        TouchEnemy(selectedEnemy);
                    else if (hit.collider.TryGetComponent(out Tower selectedTower))
                        TouchTower(selectedTower);
                    else if (hit.collider.TryGetComponent(out TowerSlot selectedTowerSlot))
                        TouchSlot(selectedTowerSlot);
                    else if (!hit.collider.GetComponent<Button>())
                        BackgroundClick();
                }
            }
        }



        #region Enemy related
        /// <summary>
        /// Method used when clicking on an enemy.
        /// </summary>
        /// <param name="selectedEnemy">The clicked enemy</param>
        private void TouchEnemy(Enemy selectedEnemy)
        {
            if(_previousTower)
                DeactivateTower();

            if(_previousSlot)
                DeactivateTowerSlot();

            if(_previousEnemy)
            {
                Enemy enemyBuffer = _previousEnemy;

                DeactivateEnemy();

                if(enemyBuffer != selectedEnemy)
                    ActivateEnemy(selectedEnemy);
            }
            else
                ActivateEnemy(selectedEnemy);
        }


        /// <summary>
        /// Method used to activate enemy display.
        /// </summary>
        /// <param name="enemy">Enemy displayed</param>
        private void ActivateEnemy(Enemy enemy)
        {
            UpdateEnemyInformation(enemy);
            enemy.SetSelector(true);

            _previousEnemy = enemy;
            _previousEnemy.InformationUI = this;
        }


        /// <summary>
        /// Method used to deactivate previous enemy display.
        /// </summary>
        private void DeactivateEnemy()
        {
            _previousEnemy.InformationUI = null;
            _previousEnemy.SetSelector(false);

            DisableEnemyInformation();
            ResetPrevious();
        }


        /// <summary>
        /// Method used to update enemy information on UI.
        /// </summary>
        /// <param name="enemyToDisplay">The enemy updated</param>
        public void UpdateEnemyInformation(Enemy enemyToDisplay)
        {
            if (!enemyInformationPanel.activeSelf)
            {
                if (towerInformationPanel.activeSelf)
                    DisableTowerInformation();

                enemyInformationPanel.SetActive(true);
            }
        
            lifeValue.text = enemyToDisplay.Health + " / " + enemyToDisplay.HealthMax;
            enemyName.text = enemyToDisplay.name;
            armorValue.text = Converter.TransformArmor(enemyToDisplay.Armor / 100);
            livesLostValue.text = enemyToDisplay.LivesTaken.ToString();
        }


        /// <summary>
        /// Method used to disable enemy information display.
        /// </summary>
        public void DisableEnemyInformation()
        {
            enemyInformationPanel.SetActive(false);
        }


        /// <summary>
        /// Method used by Enemy to erase its value.
        /// </summary>
        public void ErasePreviousEnemy()
        {
            _previousEnemy = null;
        }
        #endregion



        #region Tower related
        /// <summary>
        /// Method used when we click on a tower.
        /// </summary>
        /// <param name="selectedTower">The clicked tower</param>
        private void TouchTower(Tower selectedTower)
        {
            if (_previousSlot)
                DeactivateTowerSlot();

            if (_previousEnemy)
                DeactivateEnemy();

            if (_previousTower)
            {
                Tower towerBuffer = _previousTower;

                DeactivateTower();

                if (towerBuffer != selectedTower)
                    ActivateTower(selectedTower);
            }
            else
                ActivateTower(selectedTower);
        }


        /// <summary>
        /// Method called when we activate the tower informational UI.
        /// </summary>
        /// <param name="selectedTower">The tower to display</param>
        private void ActivateTower(Tower selectedTower)
        {
            Vector2 cameraScreen = _currentCamera.WorldToScreenPoint(selectedTower.transform.position);
            Vector2 finalPosition = new Vector2(cameraScreen.x - canvas.rect.width / 2, cameraScreen.y - canvas.rect.height / 2);

            if (!towerButtonController.SellButtonActive)
            {
                ActivateTowerSellButton(finalPosition, selectedTower);
                SetTowerInformation(selectedTower);

                selectedTower.ActivateRangeDisplay();
            }
            else
            {
                towerButtonController.DeactivateTowerUpgradeButtons();
                DisableTowerInformation();
                DisableTowerSellButton();

                selectedTower.DeactivateRangeDisplay();
            }

            _previousTower = selectedTower;
        }


        /// <summary>
        /// Method called when we want to desactivate previous tower UI.
        /// </summary>
        public void DeactivateTower()
        {
            _previousTower.DeactivateRangeDisplay();

            DisableTowerSellButton();
            DisableTowerChooseButton();
            DisableTowerInformation();

            ResetPrevious();
        }


        /// <summary>
        /// Method used to set or update tower information.
        /// </summary>
        /// <param name="newTower">The tower to display</param>
        public void SetTowerInformation(Tower newTower)
        {
            if (enemyInformationPanel.activeSelf)
                DisableEnemyInformation();

            towerInformationPanel.SetActive(true);

            towerName.text = newTower.name;
            damageText.text = newTower.Damage.ToString();
            breakArmorText.text = newTower.ArmorThroughInfo;
            fireRateText.text = newTower.FireRateInfo;
        }


        /// <summary>
        /// Method used to disable tower information UI.
        /// </summary>
        public void DisableTowerInformation()
        {
            towerButtonController.DeactivateTowerUpgradeButtons();
            towerInformationPanel.SetActive(false);
        }
        #endregion



        #region Tower slot related
        /// <summary>
        /// Method called when we click on a tower slot.
        /// </summary>
        /// <param name="selectedSlot">The clicked slot</param>
        private void TouchSlot(TowerSlot selectedSlot)
        {
            if (_previousTower)
                DeactivateTower();

            if (_previousEnemy)
                DeactivateEnemy();

            if (_previousSlot)
            {
                TowerSlot slotBuffer = _previousSlot;

                DeactivateTowerSlot();

                if (slotBuffer != selectedSlot)
                    ActivateTowerSlot(selectedSlot);
            }
            else
                ActivateTowerSlot(selectedSlot);
        }


        /// <summary>
        /// Method called when we want to activate a tower slot.
        /// </summary>
        /// <param name="selectedSlot">The tower slot to activate</param>
        private void ActivateTowerSlot(TowerSlot selectedSlot)
        {
            Vector2 cameraScreen = _currentCamera.WorldToScreenPoint(selectedSlot.transform.position);
            Vector2 finalPosition = new Vector2(cameraScreen.x - canvas.rect.width / 2, cameraScreen.y - canvas.rect.height / 2);

            ActivateTowerChooseButton(finalPosition, selectedSlot);

            _previousSlot = selectedSlot;
        }


        /// <summary>
        /// Method used when we want to desactivate the tower slot UI.
        /// </summary>
        private void DeactivateTowerSlot()
        {
            DisableTowerChooseButton();

            ResetPrevious();
        }


        /// <summary>
        /// Method called when we activate the tower choose button.
        /// </summary>
        /// <param name="newPosition">The new position of the UI</param>
        /// <param name="activatedSlot">The tower slot to activate</param>
        public void ActivateTowerChooseButton(Vector2 newPosition, TowerSlot activatedSlot)
        {
            towerButtonController.gameObject.SetActive(true);
            towerButtonController.DeactivateTowerUpgradeButtons();
            towerButtonController.ActivatePurchaseButtons(newPosition, activatedSlot);
        }


        /// <summary>
        /// Method used to desactivate tower choose buttons.
        /// </summary>
        public void DisableTowerChooseButton()
        {
            towerButtonController.gameObject.SetActive(false);
        }


        /// <summary>
        /// Method used to activate the tower sell button.
        /// </summary>
        /// <param name="newPosition">The new position of the button</param>
        /// <param name="activatedTower">The tower to activate</param>
        public void ActivateTowerSellButton(Vector2 newPosition, Tower activatedTower)
        {
            towerButtonController.gameObject.SetActive(true);
            towerButtonController.DeactivatePurchaseButtons();
            towerButtonController.ActivateTowerUpgradeButtons(newPosition, activatedTower);
        }


        /// <summary>
        /// Method used to disable tower sell button.
        /// </summary>
        public void DisableTowerSellButton()
        {
            towerButtonController.gameObject.SetActive(false);
        }
        #endregion



        #region Desactivate all
        /// <summary>
        /// Method used to desactivate previous objects.
        /// </summary>
        private void DesactivatePreviousOnes()
        {
            if (_previousEnemy)
                DeactivateEnemy();

            if (_previousTower)
                DeactivateTower();

            if (_previousSlot)
                DeactivateTowerSlot();
        }


        /// <summary>
        /// Method called when the player clicks on the background.
        /// </summary>
        private void BackgroundClick()
        {
            DesactivatePreviousOnes();

            DisableEnemyInformation();
            DisableTowerInformation();
            DisableTowerChooseButton();
            DisableTowerSellButton();

            ResetPrevious();
        }


        /// <summary>
        /// Method used to reset previous objects.
        /// </summary>
        private void ResetPrevious()
        {
            _previousEnemy = null;
            _previousSlot = null;
            _previousTower = null;
        }
        #endregion
    }
}