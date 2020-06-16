using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private GameObject _enemyPrefab;

    private Stack<GameObject> _enemyPool = new Stack<GameObject>();


    public GameObject GetOneEnemy()
    {
        if (_enemyPool.Count > 0)
            return _enemyPool.Pop();
        else
            return Instantiate(_enemyPrefab);
    }


    public void AddOneEnemy(GameObject newEnemy) { _enemyPool.Push(newEnemy); }

    public void SetEnemyPrefab(GameObject newEnemyPrefab) { _enemyPrefab = newEnemyPrefab; }
}