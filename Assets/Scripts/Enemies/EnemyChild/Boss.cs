using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [Header("Boss related")]
    [SerializeField]
    protected Enemy _enemySpawnedPrefab;
    public Enemy Spawnling { get => _enemySpawnedPrefab; }

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
    public int PathsWanted { get => _numberOfPathsWanted; }


    [SerializeField]
    protected Particle _walkDirtParticle;


    protected LevelController _levelController;

    public List<List<Vector2>> AvailablePaths { get; set; } = new List<List<Vector2>>();



    public override void Initialize(List<Vector2> newPath, EnemyPool newPool, int pathIndex)
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
                hatchling.Initialize(AvailablePaths[Random.Range(0, AvailablePaths.Count)], _enemyPool, _pathIndex);

                foreach (Particle current in _particleController.GetParticle(_walkDirtParticle, 3))
                    current.Initialize(transform.position);

                _animator.SetTrigger("lay");
                yield return new WaitForSeconds(_spawnTime);
            }
            _moving = true;
        }
    }
}
