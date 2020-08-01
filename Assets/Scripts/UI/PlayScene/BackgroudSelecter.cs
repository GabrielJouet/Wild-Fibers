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
                if(!hit.collider.GetComponent<Button>())
                    DesactivatePreviousOnes();

                if (hit.collider.TryGetComponent(out Enemy selectedEnemy))
                    ActivateEnemy(selectedEnemy);
                else if(hit.collider.TryGetComponent(out Tower selectedTower))
                    ActivateTower(selectedTower);
                else if (hit.collider.TryGetComponent(out TowerSlot selectedTowerSlot))
                    ActivateTowerSlot(selectedTowerSlot);
                else if(hit.collider.gameObject == gameObject)
                    BackgroundClick();
            }
        }
    }


                        /*Enemy related*/
    private void ActivateEnemy(Enemy selectedEnemy)
    {
        if (_previousEnemy == selectedEnemy)
            DesactivateEnemy();
        else
        {
            SetEnemyInformation(selectedEnemy);
            selectedEnemy.SetSelector();

            _previousEnemy = selectedEnemy;
        }
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




                        /*Tower related*/
    private void ActivateTower(Tower selectedTower)
    {
        if (_previousTower == selectedTower)
            DesactivateTower();
        else
        {
            Vector2 cameraScreen = Camera.main.WorldToScreenPoint(selectedTower.transform.position);
            Vector2 finalPosition = new Vector2(cameraScreen.x - Screen.width / 2, cameraScreen.y - Screen.height / 2);

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


                                /*Tower slot and buttons related*/
    private void ActivateTowerSlot(TowerSlot selectedSlot)
    {
        if (_previousSlot == selectedSlot)
            DesactivateTowerSlot();
        else
        {
            if (selectedSlot.GetCurrentTower() == null)
            {
                Vector2 cameraScreen = Camera.main.WorldToScreenPoint(selectedSlot.transform.position);
                Vector2 finalPosition = new Vector2(cameraScreen.x - Screen.width / 2, cameraScreen.y - Screen.height / 2);

                if (!selectedSlot.GetChooserActive())
                    ActivateTowerChooseButton(finalPosition, selectedSlot);
                else
                    DisableTowerChooseButton();

                selectedSlot.RevertChooserActive();

                _previousSlot = selectedSlot;
            }
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



                            /*Desactivate all*/
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
}