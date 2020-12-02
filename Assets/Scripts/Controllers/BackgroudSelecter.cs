using UnityEngine;
using UnityEngine.UI;

/*
 * Class that handles all informational UI
 */
public class BackgroudSelecter : MonoBehaviour
{
    [Header("Tower related objects")]
    //Tower information UI object
    [SerializeField]
    private GameObject _towerInformationPanel;

    //Text component handling tower name
    [SerializeField]
    private Text _towerName;

    //Text component handling tower damage
    [SerializeField]
    private Text _damageText;

    //Text component handling tower armor through
    [SerializeField]
    private Text _breakArmorText;

    //Text component handling tower fire rate
    [SerializeField]
    private Text _fireRateText;


    [Header("Enemy related objects")]
    //Enemy UI panel object
    [SerializeField]
    private GameObject _enemyInformationPanel;

    //Text component handling enemy name
    [SerializeField]
    private Text _enemyName;

    //Text component handling enemy health
    [SerializeField]
    private Text _lifeValue;

    //Text component handling enemy armor
    [SerializeField]
    private Text _armorValue;

    //Text component handling enemy lives taken
    [SerializeField]
    private Text _livesLostValue;


    [Header("Tower buttons related")]
    //Choose buttons object
    [SerializeField]
    private GameObject _chooseButton;

    //Sell buttons object
    [SerializeField]
    private GameObject _sellButton;

    //Regular canvas
    [SerializeField]
    private RectTransform _canvas;


    //Buffer variables used to record previous objects
    private Enemy _previousEnemy;

    private Tower _previousTower;

    private TowerSlot _previousSlot;



    //Update method, called each frame
    private void Update()
    {
        //If we press mouse button
        //We will check what type of object we pressed
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 90);

            if (hit.collider != null)
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



    /*Enemy related*/
    #region
    //Method used to display enemy information after touching it
    //
    //Parameter => selected enemy
    private void TouchEnemy(Enemy selectedEnemy)
    {
        if(_previousTower)
            DesactivateTower();

        if(_previousSlot)
            DesactivateTowerSlot();


        if(_previousEnemy != null)
        {
            Enemy enemyBuffer = _previousEnemy;

            DesactivateEnemy();

            if(enemyBuffer != selectedEnemy)
                ActivateEnemy(selectedEnemy);
        }
        else
            ActivateEnemy(selectedEnemy);
    }


    //Method used to activate feedback on selected enemy
    //
    //Parameter => enemy, the enemy to activate visual feedback
    private void ActivateEnemy(Enemy enemy)
    {
        UpdateEnemyInformation(enemy);
        enemy.SetSelector(true);

        _previousEnemy = enemy;
        _previousEnemy.InformationUI = this;
    }


    //Method used to desactivate feedback on previous selected enemy
    private void DesactivateEnemy()
    {
        _previousEnemy.InformationUI = null;
        _previousEnemy.SetSelector(false);

        DisableEnemyInformation();
        ResetPrevious();
    }


    //Method used to update UI information with selected enemy
    //
    //Parameter => enemy, the enemy to display
    public void UpdateEnemyInformation(Enemy enemyToDisplay)
    {
        if (!_enemyInformationPanel.activeSelf)
        {
            if (_towerInformationPanel.activeSelf)
                DisableTowerInformation();

            _enemyInformationPanel.SetActive(true);
        }
        
        _lifeValue.text = enemyToDisplay.Health + " / " + enemyToDisplay.HealthMax;
        _enemyName.text = enemyToDisplay.Name;
        _armorValue.text = ArmorType.TransformArmor(enemyToDisplay.Armor);
        _livesLostValue.text = enemyToDisplay.LivesTaken.ToString();
    }


    //Method used to disable enemy UI information panel
    public void DisableEnemyInformation()
    {
        _enemyInformationPanel.SetActive(false);
    }


    //Method used to erase previous enemy, if it dies 
    public void ErasePreviousEnemy()
    {
        _previousEnemy = null;
    }
    #endregion



    /*Tower related*/
    #region
    //Method used to display tower information after touching it
    //
    //Parameter => selected tower
    private void TouchTower(Tower selectedTower)
    {
        if (_previousSlot)
            DesactivateTowerSlot();

        if (_previousEnemy)
            DesactivateEnemy();


        if (_previousTower != null)
        {
            Tower towerBuffer = _previousTower;

            DesactivateTower();

            if (towerBuffer != selectedTower)
                ActivateTower(selectedTower);
        }
        else
            ActivateTower(selectedTower);
    }


    //Method used to activate feedback on selected tower
    //
    //Parameter => selectedTower, the tower to activate visual feedback
    private void ActivateTower(Tower selectedTower)
    {
        Vector2 cameraScreen = Camera.main.WorldToScreenPoint(selectedTower.transform.position);
        Vector2 finalPosition = new Vector2(cameraScreen.x - _canvas.rect.width / 2, cameraScreen.y - _canvas.rect.height / 2);

        if (!selectedTower.SellerActive)
        {
            ActivateTowerSellButton(finalPosition, selectedTower, Mathf.FloorToInt(selectedTower.Price / 4));
            SetTowerInformation(selectedTower.Icon, selectedTower.Name, selectedTower.Damage, selectedTower.ArmorThrough, selectedTower.TimeShots / 1f);

            selectedTower.ActivateRangeDisplay();
        }
        else
        {
            DisableTowerInformation();
            DisableTowerSellButton();

            selectedTower.DesactivateRangeDisplay();
        }
        selectedTower.RevertSellerActive();

        _previousTower = selectedTower;
    }


    //Method used to desactivate feedback on previous selected tower
    private void DesactivateTower()
    {
        _previousTower.ResetTowerDisplay();

        DisableTowerSellButton();
        DisableTowerChooseButton();
        DisableTowerInformation();

        ResetPrevious();
    }


    //Method used to set UI information with selected enemy
    //
    //Parameter => newTowerIcon, the icon to display
    //             newTowerName, name to display
    //             newDamageValue, damage to display
    //             newBreakArmorValue, armor through to display
    //             newFireRateValue, fire rate to display
    public void SetTowerInformation(Sprite newTowerIcon, string newTowerName, int newDamageValue, float newBreakArmorValue, float newFireRateValue)
    {
        if (_enemyInformationPanel.activeSelf)
            DisableEnemyInformation();

        _towerInformationPanel.SetActive(true);

        _towerName.text = newTowerName;
        _damageText.text = newDamageValue.ToString();
        _breakArmorText.text = BreakArmor.TransformArmorThrough(newBreakArmorValue / 100);
        _fireRateText.text = FireRate.TransformFireRate(1 / newFireRateValue);
    }


    //Method used to disable tower UI information panel
    public void DisableTowerInformation()
    {
        _towerInformationPanel.SetActive(false);
    }
    #endregion



    /*Tower slot and buttons related*/
    #region
    //Method used to display tower slot information after touching it
    //
    //Parameter => selected tower
    private void TouchSlot(TowerSlot selectedSlot)
    {
        if (_previousTower)
            DesactivateTower();

        if (_previousEnemy)
            DesactivateEnemy();


        if (_previousSlot != null)
        {
            TowerSlot slotBuffer = _previousSlot;

            DesactivateTowerSlot();

            if (slotBuffer != selectedSlot)
                ActivateTowerSlot(selectedSlot);
        }
        else
            ActivateTowerSlot(selectedSlot);
    }


    //Method used to activate feedback on selected tower slot
    //
    //Parameter => selectedSlot, the tower slot to activate visual feedback
    private void ActivateTowerSlot(TowerSlot selectedSlot)
    {
        if (selectedSlot.Tower == null)
        {
            Vector2 cameraScreen = Camera.main.WorldToScreenPoint(selectedSlot.transform.position);
            Vector2 finalPosition = new Vector2(cameraScreen.x - _canvas.rect.width / 2, cameraScreen.y - _canvas.rect.height / 2);

            if (!selectedSlot.ChooserActive)
                ActivateTowerChooseButton(finalPosition, selectedSlot);
            else
                DisableTowerChooseButton();

            selectedSlot.RevertChooserActive();

            _previousSlot = selectedSlot;
        }
    }


    //Method used to desactivate feedback on previous selected tower slot
    private void DesactivateTowerSlot()
    {
        DisableTowerChooseButton();

        _previousSlot.ResetChooserActive();

        ResetPrevious();
    }


    //Method used to activate choose button on selected tower slot
    //
    //Parameter => newPosition, position of buttons
    //             activatedSlot, slot selected
    public void ActivateTowerChooseButton(Vector2 newPosition, TowerSlot activatedSlot)
    {
        _chooseButton.SetActive(true);
        _chooseButton.GetComponent<ChooseButton>().Activate(newPosition, activatedSlot);
    }


    //Method used to desactivate choose button on previous selected tower slot
    public void DisableTowerChooseButton()
    {
        _chooseButton.SetActive(false);
    }


    //Method used to activate sell button on selected tower slot    
    //
    //Parameter => newPosition, position of buttons
    //             activatedSlot, slot selected
    //             price, price to display
    public void ActivateTowerSellButton(Vector2 newPosition, Tower activatedTower, int price)
    {
        _sellButton.SetActive(true);
        _sellButton.GetComponent<SellButton>().Activate(newPosition, activatedTower, price);
    }


    //Method used to desactivate sell buttons on previous selected tower slot
    public void DisableTowerSellButton()
    {
        _sellButton.SetActive(false);
    }
    #endregion



    /*Desactivate all*/
    #region
    //Method used to desactivate previous selected objects
    private void DesactivatePreviousOnes()
    {
        if (_previousEnemy)
            DesactivateEnemy();

        if (_previousTower)
            DesactivateTower();

        if (_previousSlot)
            DesactivateTowerSlot();
    }


    //Method used when user clicks on the background and not on any object
    private void BackgroundClick()
    {
        DesactivatePreviousOnes();

        DisableEnemyInformation();
        DisableTowerInformation();
        DisableTowerChooseButton();
        DisableTowerSellButton();

        ResetPrevious();
    }


    //Method used to reset previous selected objects
    private void ResetPrevious()
    {
        _previousEnemy = null;

        _previousSlot = null;

        _previousTower = null;
    }
    #endregion
}