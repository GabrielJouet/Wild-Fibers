using System.Collections;
using UnityEngine;

public class Boss : Enemy
{
    [Header("Boss related")]
    [SerializeField]
    protected Enemy _enemySpawnedPrefab;

    [SerializeField]
    protected float _timeBetweenSpawn;

    [SerializeField]
    protected float _spawnTime;

    [SerializeField]
    protected bool _stopWhileSpawning;

    [SerializeField]
    protected int _numberOfEnemiesPerSpawn;


    protected LevelController _levelController;



    public override void Initialize(Path newPath, EnemyPool newPool, int pathIndex)
    {
        base.Initialize(newPath, newPool, pathIndex);
        _levelController = FindObjectOfType<LevelController>();

        StartCoroutine(DelaySpawn());
    }



    protected IEnumerator DelaySpawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(_timeBetweenSpawn);

            _moving = false;
            for(int i = 0; i < _numberOfEnemiesPerSpawn; i ++)
            {
                Enemy hatchling = _levelController.RecoverPool(_enemySpawnedPrefab).GetOneEnemy();
                hatchling.Initialize(_path, _enemyPool, _pathIndex);
                yield return new WaitForSeconds(_spawnTime);
            }
            _moving = true;
        }
    }



    public Enemy GetSpawnedEnemy()
    {
        return _enemySpawnedPrefab;
    }
}
