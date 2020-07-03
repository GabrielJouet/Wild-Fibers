using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationUIController : MonoBehaviour
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


    private List<TowerSlot> _otherSlots = new List<TowerSlot>();



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



    public void SetEnemyInformation(string newEnemyName, int newLifeValue, int newLifeMaxValue, float newArmorValue, int newLivesLostValue)
    {
        if (_towerInformationPanel.activeSelf)
            DisableTowerInformation();

        _enemyInformationPanel.SetActive(true);

        _enemyName.text = newEnemyName;
        _lifeValue.text = newLifeValue + " / " + newLifeMaxValue;
        _armorValue.text = newArmorValue + " %";
        _livesLostValue.text = newLivesLostValue.ToString();
    }


    public void DisableEnemyInformation()
    {
        _enemyInformationPanel.SetActive(false);
    }


    public void ActivateTowerChooseButton(Vector2 newPosition, TowerSlot activatedSlot)
    {
        ResetSlots();

        _chooseButton.SetActive(true);
        _chooseButton.GetComponent<ChooseButton>().Activate(newPosition, activatedSlot);
    }


    public void DisableTowerChooseButton()
    {
        _chooseButton.SetActive(false);
    }


    public void ActivateTowerSellButton(Vector2 newPosition, TowerSlot activatedSlot, int price)
    {
        ResetSlots();

        _sellButton.SetActive(true);
        _sellButton.GetComponent<SellButton>().Activate(newPosition, activatedSlot, price);
    }



    public void DisableTowerSellButton()
    {
        _sellButton.SetActive(false);
    }


    public void BackgroundClick()
    {
        DisableEnemyInformation();
        DisableTowerInformation();
        DisableTowerChooseButton();
        DisableTowerSellButton();

        ResetSlots();
        ResetTowers();
        ResetEnemies();
    }


    public void ResetSlots()
    {
        if (_otherSlots.Count == 0)
            _otherSlots = new List<TowerSlot>(FindObjectsOfType<TowerSlot>());

        foreach (TowerSlot current in _otherSlots)
            current.ResetChooser();
    }


    public void ResetEnemies()
    {
        foreach (Enemy current in FindObjectsOfType<Enemy>())
            current.ResetSelector();
    }
}