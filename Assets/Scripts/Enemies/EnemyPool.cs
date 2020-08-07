using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private GameObject _enemyPrefab;

    private readonly Stack<GameObject> _enemyPool = new Stack<GameObject>();

    private RessourceController _ressourceController;

    private readonly List<GameObject> _livingEnemies = new List<GameObject>();

    private bool _waitForEnd = false;

    private Spawner _spawner;



    public void Initialize(GameObject newPrefab, RessourceController newRessourceController)
    {
        _ressourceController = newRessourceController;
        _enemyPrefab = newPrefab;
    }


    public GameObject GetOneEnemy()
    {
        GameObject enemyBuffered;

        if (_enemyPool.Count > 0)
        {
            enemyBuffered = _enemyPool.Pop();
            _livingEnemies.Add(enemyBuffered);
            return enemyBuffered;
        }
        else
        {
            enemyBuffered = Instantiate(_enemyPrefab, transform);
            _livingEnemies.Add(enemyBuffered);
            return enemyBuffered;
        }
    }


    public void AddOneEnemy(GameObject newEnemy, bool stillAlive, int livesLostOrGoldGained) 
    {
        if (_livingEnemies.Contains(newEnemy))
            _livingEnemies.Remove(newEnemy);

        newEnemy.SetActive(false);
        _enemyPool.Push(newEnemy);

        if (stillAlive)
            _ressourceController.RemoveLives(livesLostOrGoldGained);
        else
            _ressourceController.AddGold(livesLostOrGoldGained);

        if (_waitForEnd && _livingEnemies.Count == 0)
            _spawner.EnemiesKilled();
    }


    public GameObject GetPrefab() { return _enemyPrefab; }


    public void RecordLevelEnd(Spawner newSpawner)
    {
        _waitForEnd = true;
        _spawner = newSpawner;

        if (_livingEnemies.Count == 0)
            _spawner.EnemiesKilled();
    }
}