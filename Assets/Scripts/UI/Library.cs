using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Library : MonoBehaviour
{
    private List<TowerData> _towers;

    [SerializeField]
    private List<Enemy> _enemies;

    [SerializeField]
    private GameObject _infoIconPrefab;

    [SerializeField]
    private GameObject _towerPanel;

    [SerializeField]
    private GameObject _enemyPanel;

    [SerializeField]
    private Transform _towerList;

    [SerializeField]
    private Transform _enemyList;

    [SerializeField]
    private List<TowerIcon> _towerIcons;



    private void Awake()
    {
        _towers = FindObjectOfType<SquadController>().Towers;

        foreach (Enemy current in _enemies)
        {
            InfoIcon newIcon = Instantiate(_infoIconPrefab, _enemyList).GetComponent<InfoIcon>();
            newIcon.Enemy = current;
            newIcon.GetComponent<Image>().sprite = current.ScreenShot;
        }

        for (int i = 0; i < _towers.Count; i++)
            _towerIcons[i].Populate(_towers[i]);

        ShowPanel(false);
    }


    public void ShowPanel(bool enemy)
    {
        _towerList.gameObject.SetActive(!enemy);
        _enemyList.gameObject.SetActive(enemy);

        _towerPanel.gameObject.SetActive(!enemy);
        _enemyPanel.gameObject.SetActive(enemy);
    }


    public void ShowSpecificInfo(InfoIcon newInfo)
    {
        Enemy newEnemy = newInfo.Enemy;
        TowerData newTower = newInfo.Tower;

        if (newEnemy != null)
        {
            _enemyPanel.transform.Find("Name").GetComponent<Text>().text = newEnemy.name;
            _enemyPanel.transform.Find("Description").GetComponent<Text>().text = newEnemy.Description;
            _enemyPanel.transform.Find("Zones").GetComponent<Text>().text = newEnemy.Zones;
            _enemyPanel.transform.Find("Health").GetComponent<Text>().text = newEnemy.HealthInfo;
            _enemyPanel.transform.Find("Speed").GetComponent<Text>().text = newEnemy.SpeedInfo;
            _enemyPanel.transform.Find("Armor").GetComponent<Text>().text = newEnemy.ArmorInfo;
            _enemyPanel.transform.Find("Resistance").GetComponent<Text>().text = newEnemy.ResistanceInfo;
            _enemyPanel.transform.Find("LivesLost").GetComponent<Text>().text = newEnemy.LivesTakenInfo;
            _enemyPanel.transform.Find("Special").GetComponent<Text>().text = newEnemy.Special;
        }
        else if (newInfo.Tower != null)
        {
            _towerPanel.transform.Find("Name").GetComponent<Text>().text = newTower.name;
            _towerPanel.transform.Find("Description").GetComponent<Text>().text = newTower.Description;
            _towerPanel.transform.Find("Price").GetComponent<Text>().text = newTower.PriceInfo;
            _towerPanel.transform.Find("Damage").GetComponent<Text>().text = newTower.DamageInfo;
            _towerPanel.transform.Find("FireRate").GetComponent<Text>().text = newTower.FireRateInfo;
            _towerPanel.transform.Find("ArmorThrough").GetComponent<Text>().text = newTower.ArmorThroughInfo;
            _towerPanel.transform.Find("Dot").GetComponent<Text>().text = newTower.DotInfo;
            _towerPanel.transform.Find("Special").GetComponent<Text>().text = newTower.Special;
            _towerPanel.transform.Find("Shots").GetComponent<Text>().text = newTower.ShotsInfo;
        }
    }
}
