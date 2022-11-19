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
    protected GameObject _enemySpawnedPrefab;

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
    /// <param name="pathIndex">Current progression on the path</param>
    /// <param name="spawner">Spawner that spawns this enemy</param>
    public override void Initialize(List<Vector2> newPath, int pathIndex, Spawner spawner)
    {
        base.Initialize(newPath, pathIndex, spawner);
        
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

            _moving = false;
            _animator.SetBool("lay", true);

            for (int i = 0; i < _numberOfEnemiesPerSpawn; i++)
            {
                Controller.Instance.PoolController.Out(_enemySpawnedPrefab.GetComponent<PoolableObject>()).GetComponent<Enemy>().Initialize(_path, _pathIndex, _spawner);

                yield return new WaitForSeconds(_spawnTime + Random.Range(-_spawnTime / 20, _spawnTime / 20));
            }

            _animator.SetBool("lay", false);
            _moving = true;
        }
    }
}