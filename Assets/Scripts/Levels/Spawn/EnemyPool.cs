using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private GameObject _enemyPrefab;

    private readonly Stack<GameObject> _enemyPool = new Stack<GameObject>();

    private RessourceController _ressourceController;


    public void Initialize(GameObject newPrefab, RessourceController newRessourceController)
    {
        _ressourceController = newRessourceController;
        _enemyPrefab = newPrefab;
    }


    public GameObject GetOneEnemy()
    {
        if (_enemyPool.Count > 0)
            return _enemyPool.Pop();
        else
            return Instantiate(_enemyPrefab);
    }


    public void AddOneEnemy(GameObject newEnemy, bool stillAlive, int livesLostOrGoldGained) 
    {
        newEnemy.SetActive(false);
        _enemyPool.Push(newEnemy);

        if (stillAlive)
            _ressourceController.RemoveLives(livesLostOrGoldGained);
        else
            _ressourceController.AddGold(livesLostOrGoldGained);
    }


    public GameObject GetPrefab() { return _enemyPrefab; }
}