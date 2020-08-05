using UnityEngine;
using UnityEngine.UI;

public class BackgroudSelecter : MonoBehaviour
{
    [Header("Tower related objects")]
    [SerializeField]
    private GameObject _towerInformationPanel;
    [SerializeField]
    private Image _towerIcon;
    [SerializeField]
    private Text _towerName;
    [SerializeField]
    private Text _damageText;
    [SerializeField]
    private Text _breakArmorText;
    [SerializeField]
    private Text _fireRateText;


    [Header("Enemy related objects")]
    [SerializeField]
    private GameObject _enemyInformationPanel;
    [SerializeField]
    private Text _enemyName;
    [SerializeField]
    private Text _lifeValue;
    [SerializeField]
    private Text _armorValue;
    [SerializeField]
    private Text _livesLostValue;


    [Header("Tower buttons related")]
    [SerializeField]
    private GameObject _chooseButton;
    [SerializeField]
    private GameObject _sellButton;
    [SerializeField]
    private RectTransform _canvas;

    private Enemy _previousEnemy;

    private Tower _previousTower;

    private TowerSlot _previousSlot;


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 90);

            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out Enemy selectedEnemy))
                    TouchEnemy(selectedEnemy);
                else if(hit.collider.TryGetComponent(out Tower selectedTower))
                    TouchTower(selectedTower);
                else if (hit.collider.TryGetComponent(out TowerSlot selectedTowerSlot))
                    TouchSlot(selectedTowerSlot);
                else if(!hit.collider.GetComponent<Button>())
                    BackgroundClick();
            }
        }
    }


    /*Enemy related*/
    #region
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


    private void ActivateEnemy(Enemy enemy)
    {
        SetEnemyInformation(enemy);
        enemy.SetSelector();

        _previousEnemy = enemy;
    }


    private void DesactivateEnemy()
    {
        _previousEnemy.ResetSelector();

        DisableEnemyInformation();
        ResetPrevious();
    }


    public void SetEnemyInformation(Enemy enemyToDisplay)
    {
        if (_towerInformationPanel.activeSelf)
            DisableTowerInformation();

        _enemyInformationPanel.SetActive(true);

        _enemyName.text = enemyToDisplay.GetName();
        _lifeValue.text = enemyToDisplay.GetHealth() + " / " + enemyToDisplay.GetMaxHealth();
        _armorValue.text = enemyToDisplay.GetMaxArmor() + " %";
        _livesLostValue.text = enemyToDisplay.GetNumberOfLivesTaken().ToString();
    }


    public void DisableEnemyInformation()
    {
        _enemyInformationPanel.SetActive(false);
    }
    #endregion



    /*Tower related*/
    #region
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


    private void ActivateTower(Tower selectedTower)
    {
        Vector2 cameraScreen = Camera.main.WorldToScreenPoint(selectedTower.transform.position);
        Vector2 finalPosition = new Vector2(cameraScreen.x - _canvas.rect.width / 2, cameraScreen.y - _canvas.rect.height / 2);

        if (!selectedTower.GetSellerActive())
        {
            ActivateTowerSellButton(finalPosition, selectedTower, Mathf.FloorToInt(selectedTower.GetPrice() / 4));
            SetTowerInformation(selectedTower.GetIcon(), selectedTower.GetName(), selectedTower.GetDamage(), selectedTower.GetArmorThrough(), selectedTower.GetTimeBetweenShots());

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


    private void DesactivateTower()
    {
        _previousTower.ResetTowerDisplay();

        DisableTowerSellButton();
        DisableTowerChooseButton();
        DisableTowerInformation();

        ResetPrevious();
    }



    public void SetTowerInformation(Sprite newTowerIcon, string newTowerName, int newDamageValue, float newBreakArmorValue, float newFireRateValue)
    {
        if (_enemyInformationPanel.activeSelf)
            DisableEnemyInformation();

        _towerInformationPanel.SetActive(true);

        _towerIcon.sprite = newTowerIcon;
        _towerName.text = newTowerName;
        _damageText.text = newDamageValue.ToString();
        _breakArmorText.text = newBreakArmorValue + " % ";
        _fireRateText.text = 1 / newFireRateValue + " / sec";
    }


    public void DisableTowerInformation()
    {
        _towerInformationPanel.SetActive(false);
    }
    #endregion


    /*Tower slot and buttons related*/
    #region
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


    private void ActivateTowerSlot(TowerSlot selectedSlot)
    {
        if (selectedSlot.GetCurrentTower() == null)
        {
            Vector2 cameraScreen = Camera.main.WorldToScreenPoint(selectedSlot.transform.position);
            Vector2 finalPosition = new Vector2(cameraScreen.x - _canvas.rect.width / 2, cameraScreen.y - _canvas.rect.height / 2);

            if (!selectedSlot.GetChooserActive())
                ActivateTowerChooseButton(finalPosition, selectedSlot);
            else
                DisableTowerChooseButton();

            selectedSlot.RevertChooserActive();

            _previousSlot = selectedSlot;
        }
    }


    private void DesactivateTowerSlot()
    {
        DisableTowerChooseButton();

        _previousSlot.ResetChooserActive();

        ResetPrevious();
    }


    public void ActivateTowerChooseButton(Vector2 newPosition, TowerSlot activatedSlot)
    {
        _chooseButton.SetActive(true);
        _chooseButton.GetComponent<ChooseButton>().Activate(newPosition, activatedSlot);
    }


    public void DisableTowerChooseButton()
    {
        _chooseButton.SetActive(false);
    }


    public void ActivateTowerSellButton(Vector2 newPosition, Tower activatedTower, int price)
    {
        _sellButton.SetActive(true);
        _sellButton.GetComponent<SellButton>().Activate(newPosition, activatedTower, price);
    }


    public void DisableTowerSellButton()
    {
        _sellButton.SetActive(false);
    }
    #endregion


    /*Desactivate all*/
    #region
    private void DesactivatePreviousOnes()
    {
        if (_previousEnemy)
            DesactivateEnemy();

        if (_previousTower)
            DesactivateTower();

        if (_previousSlot)
            DesactivateTowerSlot();
    }


    private void BackgroundClick()
    {
        DesactivatePreviousOnes();

        DisableEnemyInformation();
        DisableTowerInformation();
        DisableTowerChooseButton();
        DisableTowerSellButton();

        ResetPrevious();
    }


    private void ResetPrevious()
    {
        _previousEnemy = null;

        _previousSlot = null;

        _previousTower = null;
    }
    #endregion
}