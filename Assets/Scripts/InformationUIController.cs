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
    private Image _enemyIcon;

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

    private readonly List<TowerSlot> _otherSlots = new List<TowerSlot>();


    public void SetTowerInformation(Sprite newTowerIcon, string newTowerName, int newDamageValue, float newBreakArmorValue, float newFireRateValue)
    {
        if (_enemyInformationPanel.activeSelf)
            DisableEnemyInformation();

        _towerInformationPanel.SetActive(true);

        _towerIcon.sprite = newTowerIcon;
        _towerName.text = newTowerName;
        _damageText.text = newDamageValue.ToString();
        _breakArmorText.text = newBreakArmorValue.ToString();
        _fireRateText.text = newFireRateValue.ToString();
    }


    public void DisableTowerInformation()
    {
        _towerInformationPanel.SetActive(false);
    }



    public void SetEnemyInformation(Sprite newEnemyIcon, string newEnemyName, int newLifeValue, float newArmorValue, int newLivesLostValue)
    {
        if (_towerInformationPanel.activeSelf)
            DisableTowerInformation();

        _enemyInformationPanel.SetActive(true);

        _enemyIcon.sprite = newEnemyIcon;
        _enemyName.text = newEnemyName;
        _lifeValue.text = newLifeValue.ToString();
        _armorValue.text = newArmorValue.ToString();
        _livesLostValue.text = newLivesLostValue.ToString();
    }


    public void DisableEnemyInformation()
    {
        _enemyInformationPanel.SetActive(false);
    }


    public void DisableTowerSellButton()
    {
        _sellButton.SetActive(false);

        if (_otherSlots.Count == 0)
            RecoverTowerSlots();

        foreach (TowerSlot current in _otherSlots)
            current.ResetChooser();
    }


    public void DisableTowerChooseButton()
    {
        _chooseButton.SetActive(false);

        if (_otherSlots.Count == 0)
            RecoverTowerSlots();

        foreach (TowerSlot current in _otherSlots)
            current.ResetChooser();
    }


    private void RecoverTowerSlots()
    {
        //We recover every other slots in order to desactivate them when we click
        foreach (TowerSlot current in FindObjectsOfType<TowerSlot>())
            if (current != this)
                _otherSlots.Add(current);
    }
}