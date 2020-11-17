using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    protected int _numberOfPathsWanted;

    [SerializeField]
    protected Particle _walkDirtParticle;


    protected LevelController _levelController;

    protected List<Path> _availablePaths = new List<Path>();



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
                hatchling.Initialize(_availablePaths[Random.Range(0, _availablePaths.Count)], _enemyPool, _pathIndex);

                foreach (Particle current in _particleController.GetParticle(_walkDirtParticle, 3))
                    current.Initialize(transform.position);

                yield return new WaitForSeconds(_spawnTime);
            }
            _moving = true;
        }
    }



    public Enemy GetSpawnedEnemy()
    {
        return _enemySpawnedPrefab;
    }

    public void SetRandomPaths(List<Path> newPaths)
    {
        _availablePaths = newPaths;
    }

    public int GetRandomPathLength()
    {
        return _numberOfPathsWanted;
    }
}
