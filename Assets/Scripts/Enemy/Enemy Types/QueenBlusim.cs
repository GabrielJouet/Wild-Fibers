using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles boss like behavior, like spawning other enemies.
/// </summary>
public class QueenBlusim : Enemy
{
    [Header("Boss related")]

    /// <summary>
    /// The enemy spawned.
    /// </summary>
    [SerializeField]
    protected Enemy _enemySpawnedPrefab;

    /// <summary>
    /// Time between each wave of spawned enemies.
    /// </summary>
    [SerializeField]
    protected float _timeBetweenSpawn;

    /// <summary>
    /// Time between start and end of spawn.
    /// </summary>
    [SerializeField]
    protected float _spawnTime;

    /// <summary>
    /// How many enemies are spawned during each spawn.
    /// </summary>
    [SerializeField]
    protected int _numberOfEnemiesPerSpawn;



    /// <summary>
    /// Initialize method.
    /// </summary>
    /// <param name="newPath">New path used</param>
    /// <param name="newPool">Pool used for the current enemy</param>
    /// <param name="pathIndex">Current progression on the path</param>
    public override void Initialize(List<Vector2> newPath, PoolController newPool, int pathIndex)
    {
        base.Initialize(newPath, newPool, pathIndex);
        
        StartCoroutine(DelaySpawn());
    }


    /// <summary>
    /// Coroutine used to delay each spawn wave.
    /// </summary>
    /// <returns>Yield time between spawn and spawn times</returns>
    protected IEnumerator DelaySpawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(_timeBetweenSpawn + Random.Range(-_timeBetweenSpawn / 20, _timeBetweenSpawn / 20));

            Moving = false;
            _animator.SetBool("lay", true);

            for (int i = 0; i < _numberOfEnemiesPerSpawn; i++)
            {
                _poolController.RecoverEnemyPool(_enemySpawnedPrefab).GetOneEnemy().Initialize(_path, _poolController, _pathIndex);

                yield return new WaitForSeconds(_spawnTime + Random.Range(-_spawnTime / 20, _spawnTime / 20));
            }

            _animator.SetBool("lay", false);
            Moving = true;
        }
    }
}
