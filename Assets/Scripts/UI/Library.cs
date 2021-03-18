using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Library : MonoBehaviour
{
    [SerializeField]
    private List<TowerInfo> _towers;

    [SerializeField]
    private List<EnemyInfo> _enemies;

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


    public void ShowEnemies()
    {
        _towerList.gameObject.SetActive(false);
        _enemyList.gameObject.SetActive(true);

        _towerPanel.gameObject.SetActive(false);
        _enemyPanel.gameObject.SetActive(true);

        if (_enemyList.childCount == 0)
        {
            foreach (EnemyInfo current in _enemies)
            {
                InfoIcon newIcon = Instantiate(_infoIconPrefab, _enemyList).GetComponent<InfoIcon>();
                newIcon.Enemy = current;
                newIcon.GetComponent<Image>().sprite = current.Sprite;
            }
        }
    }


    public void ShowTowers()
    {
        _towerList.gameObject.SetActive(true);
        _enemyList.gameObject.SetActive(false);

        _towerPanel.gameObject.SetActive(true);
        _enemyPanel.gameObject.SetActive(false);

        if (_towerList.childCount == 0)
        {
            foreach (TowerInfo current in _towers)
            {
                InfoIcon newIcon = Instantiate(_infoIconPrefab, _towerList).GetComponent<InfoIcon>();
                newIcon.Tower = current;
                newIcon.GetComponent<Image>().sprite = current.Sprite;
            }
        }
    }


    public void ShowSpecificInfo(InfoIcon newInfo)
    {
        EnemyInfo newEnemy = newInfo.Enemy;
        TowerInfo newTower = newInfo.Tower;

        if (newEnemy != null)
        {
            _enemyPanel.transform.Find("Name").GetComponent<Text>().text = newEnemy.name;
            _enemyPanel.transform.Find("Description").GetComponent<Text>().text = newEnemy.Description;
            _enemyPanel.transform.Find("Zones").GetComponent<Text>().text = newEnemy.Zones;
            _enemyPanel.transform.Find("Health").GetComponent<Text>().text = newEnemy.Health;
            _enemyPanel.transform.Find("Speed").GetComponent<Text>().text = newEnemy.Speed;
            _enemyPanel.transform.Find("Armor").GetComponent<Text>().text = newEnemy.Armor;
            _enemyPanel.transform.Find("Resistance").GetComponent<Text>().text = newEnemy.Resistance;
            _enemyPanel.transform.Find("LivesLost").GetComponent<Text>().text = newEnemy.LivesLost;
            _enemyPanel.transform.Find("Special").GetComponent<Text>().text = newEnemy.Special;
        }
        else if (newInfo.Tower != null)
        {
            _towerPanel.transform.Find("Name").GetComponent<Text>().text = newTower.name;
            _towerPanel.transform.Find("Description").GetComponent<Text>().text = newTower.Description;
            _towerPanel.transform.Find("Price").GetComponent<Text>().text = newTower.Price;
            _towerPanel.transform.Find("Damage").GetComponent<Text>().text = newTower.Damage;
            _towerPanel.transform.Find("FireRate").GetComponent<Text>().text = newTower.FireRate;
            _towerPanel.transform.Find("ArmorThrough").GetComponent<Text>().text = newTower.ArmorThrough;
            _towerPanel.transform.Find("Dot").GetComponent<Text>().text = newTower.Dot;
            _towerPanel.transform.Find("Special").GetComponent<Text>().text = newTower.Special;
            _towerPanel.transform.Find("Shots").GetComponent<Text>().text = newTower.Shots;
        }
    }
}
