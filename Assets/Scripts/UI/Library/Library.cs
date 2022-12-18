using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to handle enemy and tower info in library.
/// </summary>
public class Library : MonoBehaviour
{
    [Header("Components")]

    [Space(10)]

    /// <summary>
    /// Enemy info prefab component.
    /// </summary>
    [SerializeField]
    private GameObject _enemyInfoPrefab;

    /// <summary>
    /// Tower panel parent object.
    /// </summary>
    [SerializeField]
    private GameObject _towerPanel;

    /// <summary>
    /// Enemy panel parent object.
    /// </summary>
    [SerializeField]
    private GameObject _enemyPanel;

    /// <summary>
    /// Tower list game object.
    /// </summary>
    [SerializeField]
    private GameObject _towerList;

    /// <summary>
    /// Enemy list game object.
    /// </summary>
    [SerializeField]
    private GameObject _enemyList;

    /// <summary>
    /// Tower tree prefab, used for tower display.
    /// </summary>
    [SerializeField]
    private GameObject _towerTreePrefab;

    /// <summary>
    /// Spearator image component.
    /// </summary>
    [SerializeField]
    private Transform _separator;

    /// <summary>
    /// Towers tab image component.
    /// </summary>
    [SerializeField]
    private Image _towerTab;

    /// <summary>
    /// Enemies tab image component.
    /// </summary>
    [SerializeField]
    private Image _enemiesTab;


    [Header("Tower related")]

    /// <summary>
    /// Tower name text component.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _towerName;

    /// <summary>
    /// Tower description text component.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _towerDescription;

    /// <summary>
    /// Tower price text component.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _towerPrice;

    /// <summary>
    /// Tower damage text component.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _towerDamage;

    /// <summary>
    /// Tower fire rate text component.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _towerFireRate;

    /// <summary>
    /// Tower armor through text component.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _towerArmorThrough;

    /// <summary>
    /// Tower dot text component.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _towerDot;

    /// <summary>
    /// Tower special text component.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _towerSpecial;

    /// <summary>
    /// Tower number of shots text component.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _towerShots;


    [Header("Enemy related")]

    /// <summary>
    /// Enemy name text component.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _enemyName;

    /// <summary>
    /// Enemy description text component.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _enemyDescription;

    /// <summary>
    /// Enemy gold gained text component.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _enemyGold;

    /// <summary>
    /// Enemy health text component.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _enemyHealth;

    /// <summary>
    /// Enemy speed text component.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _enemySpeed;

    /// <summary>
    /// Enemy armor text component.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _enemyArmor;

    /// <summary>
    /// Enemy resistance text component.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _enemyResistance;

    /// <summary>
    /// Enemy lives lost text component.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _enemyLivesLost;

    /// <summary>
    /// Enemy special text component.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _enemySpecial;

    /// <summary>
    /// Color used for desactivated tabs.
    /// </summary>
    private Color _desactivatedColor = new Color(0.7f, 0.7f, 0.7f);



    /// <summary>
    /// Awake method, called at first.
    /// </summary>
    private void Awake()
    {
        List<Tower> towers = Controller.Instance.SquadController.CurrentSquad.Towers;

        SaveController saveController = Controller.Instance.SaveController;

        List<bool> enemiesFound = saveController.SaveFile.EnemiesUnlocked;
        List<Enemy> enemies = Controller.Instance.EnemyController.Enemies;

        for (int i = 0; i < enemiesFound.Count; i ++)
        {
            EnemyInfo newEnemy = Instantiate(_enemyInfoPrefab, _enemyList.transform).GetComponent<EnemyInfo>();

            if (enemiesFound[i]) 
                newEnemy.Enemy = enemies[i];

            if (i == 0 && enemiesFound[i])
                ShowEnemyInfo(newEnemy);
        }

        int maxLevel = saveController.SaveFile.CurrentSquad.TowerLevelMax;

        TowerIcon firstIcon = Instantiate(_towerTreePrefab, _towerList.transform).GetComponent<TowerIcon>();
        firstIcon.Populate(towers[0], maxLevel, false);

        TowerIcon newIcon = Instantiate(_towerTreePrefab, _towerList.transform).GetComponent<TowerIcon>();
        newIcon.Populate(towers[1], maxLevel, false);

        newIcon = Instantiate(_towerTreePrefab, _towerList.transform).GetComponent<TowerIcon>();
        newIcon.Populate(towers[2], maxLevel, false);

        newIcon = Instantiate(_towerTreePrefab, _towerList.transform).GetComponent<TowerIcon>();
        newIcon.Populate(towers[3], maxLevel, true);

        RectTransform towerPanel = _towerList.GetComponent<RectTransform>();
        VerticalLayoutGroup towerLayout = _towerList.GetComponent<VerticalLayoutGroup>();
        towerPanel.sizeDelta = new Vector2(towerPanel.sizeDelta.x, towerLayout.padding.top * 2 + towerLayout.spacing * 3 + 4 * _towerTreePrefab.GetComponent<RectTransform>().sizeDelta.y);

        ShowTowerInfo(firstIcon.First);

        ShowPanel(false);
    }



    /// <summary>
    /// Method used to show the panels.
    /// </summary>
    /// <param name="enemy">Does the panel shown is the enemy one?</param>
    public void ShowPanel(bool enemy)
    {
        _towerList.SetActive(!enemy);
        _enemyList.SetActive(enemy);

        _separator.localScale = new Vector3(enemy ? -1 : 1, 1, 1);
        _towerTab.color = enemy ? _desactivatedColor : Color.white;
        _enemiesTab.color = !enemy ? _desactivatedColor : Color.white;

        _towerPanel.SetActive(!enemy);
        _enemyPanel.SetActive(enemy);
    }


    /// <summary>
    /// Method called to show an object info.
    /// </summary>
    /// <param name="newInfo">The info object to display</param>
    public void ShowInfo(ObjectInfo newInfo)
    {
        if (newInfo.TryGetComponent(out TowerInfo towerInfo))
            ShowTowerInfo(towerInfo);
        else if (newInfo.TryGetComponent(out EnemyInfo enemyInfo))
            ShowEnemyInfo(enemyInfo);
    }


    /// <summary>
    /// Method called to show a tower info.
    /// </summary>
    /// <param name="newInfo">The info tower to display</param>
    public void ShowTowerInfo(TowerInfo newInfo)
    {
        Tower newTower = newInfo.Tower;

        _towerName.text = newTower.name;
        _towerDescription.text = newTower.LibraryDescription;
        _towerPrice.text = newTower.Price.ToString();
        _towerDamage.text = newTower.Damage.ToString();
        _towerFireRate.text = newTower.FireRateInfo;
        _towerArmorThrough.text = newTower.ArmorThroughInfo;
        _towerDot.text = newTower.DotInfo;
        _towerSpecial.text = newTower.Special;
        _towerShots.text = newTower.Shots.ToString();
    }


    /// <summary>
    /// Method called to show an enemy info.
    /// </summary>
    /// <param name="newInfo">The info enemy to show</param>
    public void ShowEnemyInfo(EnemyInfo newInfo)
    {
        Enemy newEnemy = newInfo.Enemy;

        _enemyName.text = newEnemy.name;
        _enemyDescription.text = newEnemy.Description;
        _enemyGold.text = newEnemy.GoldInfo;
        _enemyHealth.text = newEnemy.HealthMax.ToString();
        _enemySpeed.text = newEnemy.SpeedInfo;
        _enemyArmor.text = newEnemy.ArmorInfo;
        _enemyResistance.text = newEnemy.ResistanceInfo;
        _enemyLivesLost.text = newEnemy.LivesTaken.ToString();
        _enemySpecial.text = newEnemy.Special;
    }
}