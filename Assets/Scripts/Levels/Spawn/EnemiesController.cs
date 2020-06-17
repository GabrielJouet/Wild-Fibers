using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [Header("Enemy-related prefabs")]
    [SerializeField]
    private List<Enemy> _enemiesAvailables;

    [SerializeField]
    private EnemyPool _enemyPoolPrefab;


    private readonly List<EnemyPool> _enemyPools = new List<EnemyPool>();



    private void Awake()
    {
        for (int i = 0; i < _enemiesAvailables.Count; i++)
        {
            EnemyPool newEnemyPool = Instantiate(_enemyPoolPrefab, transform);
            newEnemyPool.SetEnemyPrefab(_enemiesAvailables[i].gameObject);

            _enemyPools.Add(newEnemyPool);
        }
    }


    public GameObject RecoverOneEnemy(Enemy enemyType)
    {
        return _enemyPools[_enemiesAvailables.IndexOf(enemyType)].GetOneEnemy();
    }


    public void AddOneEnemy(Enemy enemyAdded)
    {
        Enemy enemyType = null;

        foreach(Enemy current in _enemiesAvailables)
            if (enemyAdded.GetType() == current.GetType())
                enemyType = current;

        _enemyPools[_enemiesAvailables.IndexOf(enemyType)].AddOneEnemy(enemyAdded.gameObject);
    }
}