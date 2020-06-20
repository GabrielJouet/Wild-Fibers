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


    public void SetTowerInformation(Sprite newTowerIcon, string newTowerName, int newDamageValue, int newBreakArmorValue, int newFireRateValue)
    {
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



    public void SetEnemyInformation(Sprite newEnemyIcon, string newEnemyName, int newLifeValue, int newArmorValue, int newLivesLostValue)
    {
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
}