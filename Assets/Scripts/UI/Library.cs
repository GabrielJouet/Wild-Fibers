using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Library : MonoBehaviour
{
    [Header("Components")]

    [Space(10)]
    [SerializeField]
    private GameObject _enemyInfoPrefab;

    [SerializeField]
    private GameObject _towerPanel;

    [SerializeField]
    private GameObject _enemyPanel;

    [SerializeField]
    private GameObject _towerList;

    [SerializeField]
    private GameObject _enemyList;

    [SerializeField]
    private List<TowerIcon> _towerIcons;


    [Header("Tower related")]

    [SerializeField]
    private Text _towerName;

    [SerializeField]
    private Text _towerDescription;

    [SerializeField]
    private Text _towerPrice;

    [SerializeField]
    private Text _towerDamage;

    [SerializeField]
    private Text _towerFireRate;

    [SerializeField]
    private Text _towerArmorThrough;

    [SerializeField]
    private Text _towerDot;

    [SerializeField]
    private Text _towerSpecial;

    [SerializeField]
    private Text _towerShots;


    [Header("Enemy related")]

    [SerializeField]
    private Text _enemyName;

    [SerializeField]
    private Text _enemyDescription;

    [SerializeField]
    private Text _enemyGold;

    [SerializeField]
    private Text _enemyHealth;

    [SerializeField]
    private Text _enemySpeed;

    [SerializeField]
    private Text _enemyArmor;

    [SerializeField]
    private Text _enemyResistance;

    [SerializeField]
    private Text _enemyLivesLost;

    [SerializeField]
    private Text _enemySpecial;


    private void Awake()
    {
        List<TowerData> towers = FindObjectOfType<SquadController>().Towers;

        SaveController saveController = FindObjectOfType<SaveController>();

        List<bool> enemiesFound = saveController.SaveFile.EnemiesUnlocked;
        List<Enemy> enemies = FindObjectOfType<EnemyController>().Enemies;

        for (int i = 0; i < enemiesFound.Count; i ++)
        {
            EnemyInfo newEnemy = Instantiate(_enemyInfoPrefab, _enemyList.transform).GetComponent<EnemyInfo>();

            if (enemiesFound[i]) 
                newEnemy.Enemy = enemies[i];
        }

        int maxLevel = saveController.SaveFile.TowerLevelMax;
        for (int i = 0; i < towers.Count; i++)
            _towerIcons[i].Populate(towers[i], maxLevel);

        ShowPanel(false);
    }


    public void ShowPanel(bool enemy)
    {
        _towerList.SetActive(!enemy);
        _enemyList.SetActive(enemy);

        _towerPanel.SetActive(!enemy);
        _enemyPanel.SetActive(enemy);
    }


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
