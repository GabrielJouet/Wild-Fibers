using System.Collections.Generic;
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
    /// List of tower icons in the library.
    /// </summary>
    [SerializeField]
    private List<TowerIcon> _towerIcons;

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
    private Text _towerName;

    /// <summary>
    /// Tower description text component.
    /// </summary>
    [SerializeField]
    private Text _towerDescription;

    /// <summary>
    /// Tower price text component.
    /// </summary>
    [SerializeField]
    private Text _towerPrice;

    /// <summary>
    /// Tower damage text component.
    /// </summary>
    [SerializeField]
    private Text _towerDamage;

    /// <summary>
    /// Tower fire rate text component.
    /// </summary>
    [SerializeField]
    private Text _towerFireRate;

    /// <summary>
    /// Tower armor through text component.
    /// </summary>
    [SerializeField]
    private Text _towerArmorThrough;

    /// <summary>
    /// Tower dot text component.
    /// </summary>
    [SerializeField]
    private Text _towerDot;

    /// <summary>
    /// Tower special text component.
    /// </summary>
    [SerializeField]
    private Text _towerSpecial;

    /// <summary>
    /// Tower number of shots text component.
    /// </summary>
    [SerializeField]
    private Text _towerShots;


    [Header("Enemy related")]

    /// <summary>
    /// Enemy name text component.
    /// </summary>
    [SerializeField]
    private Text _enemyName;

    /// <summary>
    /// Enemy description text component.
    /// </summary>
    [SerializeField]
    private Text _enemyDescription;

    /// <summary>
    /// Enemy gold gained text component.
    /// </summary>
    [SerializeField]
    private Text _enemyGold;

    /// <summary>
    /// Enemy health text component.
    /// </summary>
    [SerializeField]
    private Text _enemyHealth;

    /// <summary>
    /// Enemy speed text component.
    /// </summary>
    [SerializeField]
    private Text _enemySpeed;

    /// <summary>
    /// Enemy armor text component.
    /// </summary>
    [SerializeField]
    private Text _enemyArmor;

    /// <summary>
    /// Enemy resistance text component.
    /// </summary>
    [SerializeField]
    private Text _enemyResistance;

    /// <summary>
    /// Enemy lives lost text component.
    /// </summary>
    [SerializeField]
    private Text _enemyLivesLost;

    /// <summary>
    /// Enemy special text component.
    /// </summary>
    [SerializeField]
    private Text _enemySpecial;

    /// <summary>
    /// Color used for desactivated tabs.
    /// </summary>
    private Color _desactivatedColor = new Color(0.7f, 0.7f, 0.7f);


    /// <summary>
    /// Awake method, called at first.
    /// </summary>
    private void Awake()
    {
        List<TowerData> towers = Controller.Instance.SquadControl.CurrentSquad.Towers;

        SaveController saveController = Controller.Instance.SaveControl;

        List<bool> enemiesFound = saveController.SaveFile.EnemiesUnlocked;
        List<Enemy> enemies = Controller.Instance.EnemyControl.Enemies;

        for (int i = 0; i < enemiesFound.Count; i ++)
        {
            EnemyInfo newEnemy = Instantiate(_enemyInfoPrefab, _enemyList.transform).GetComponent<EnemyInfo>();

            if (enemiesFound[i]) 
                newEnemy.Enemy = enemies[i];

            if (i == 0 && enemiesFound[i])
                ShowEnemyInfo(newEnemy);
        }

        int maxLevel = saveController.SaveFile.CurrentSquad.TowerLevelMax;
        for (int i = 0; i < towers.Count; i++)
            _towerIcons[i].Populate(towers[i], maxLevel);

        ShowTowerInfo(_towerIcons[0].First);

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
    /// Method called to show a tower info.
    /// </summary>
    /// <param name="newInfo">The info tower to display</param>
    public void ShowTowerInfo(TowerInfo newInfo)
    {
        TowerData newTower = newInfo.Tower;

        _towerName.text = newTower.name;
        _towerDescription.text = newTower.Description;
        _towerPrice.text = newTower.PriceInfo;
        _towerDamage.text = newTower.DamageInfo;
        _towerFireRate.text = newTower.FireRateInfo;
        _towerArmorThrough.text = newTower.ArmorThroughInfo;
        _towerDot.text = newTower.DotInfo;
        _towerSpecial.text = newTower.Special;
        _towerShots.text = newTower.ShotsInfo;
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
        _enemyHealth.text = newEnemy.HealthInfo;
        _enemySpeed.text = newEnemy.SpeedInfo;
        _enemyArmor.text = newEnemy.ArmorInfo;
        _enemyResistance.text = newEnemy.ResistanceInfo;
        _enemyLivesLost.text = newEnemy.LivesTakenInfo;
        _enemySpecial.text = newEnemy.Special;
    }
}