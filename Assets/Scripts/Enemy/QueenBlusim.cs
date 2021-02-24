using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles boss like behavior, like spawning other enemies.
/// </summary>
public class QueenBlusim : Enemy, ISpawnable
{
    [Header("Boss related")]

    /// <summary>
    /// The enemy spawned.
    /// </summary>
    [SerializeField]
    protected Enemy _enemySpawnedPrefab;
    public Enemy Spawnling { get => _enemySpawnedPrefab; }

    /// <summary>
    /// Time between each wave of spawned enemies.
    /// </summary>
    [SerializeField]
    protected float _timeBetweenSpawn;
    public float TimeBetweenSpawn { get => _timeBetweenSpawn; }

    /// <summary>
    /// Time between start and end of spawn.
    /// </summary>
    [SerializeField]
    protected float _spawnTime;
    public float SpawnTime { get => _spawnTime; }

    /// <summary>
    /// Does the boss stop while spawning other enemies?
    /// </summary>
    [SerializeField]
    protected bool _stopWhileSpawning;
    public bool StopWhileSpawning { get => _stopWhileSpawning; }

    /// <summary>
    /// How many enemies are spawned during each spawn.
    /// </summary>
    [SerializeField]
    protected int _numberOfEnemiesPerSpawn;
    public int NumberOfEnemiesPerSpawn { get => _numberOfEnemiesPerSpawn; }

    /// <summary>
    /// The walk dirt particle used by the boss.
    /// </summary>
    [SerializeField]
    protected Particle _walkDirtParticle;

    /// <summary>
    /// The number of paths used by new enemies.
    /// </summary>
    public int PathWanted { get; } = 3;


    /// <summary>
    /// Available paths generated.
    /// </summary>
    public List<List<Vector2>> AvailablePaths { get; set; } = new List<List<Vector2>>();



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
    public IEnumerator DelaySpawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(TimeBetweenSpawn + Random.Range(-TimeBetweenSpawn / 20, TimeBetweenSpawn / 20));

            Moving = !StopWhileSpawning;
            _animator.SetBool("lay", true);

            for (int i = 0; i < NumberOfEnemiesPerSpawn; i++)
            {
                _poolController.RecoverEnemyPool(Spawnling).GetOneEnemy().Initialize(AvailablePaths[Random.Range(0, AvailablePaths.Count)], _poolController, _pathIndex);

                yield return new WaitForSeconds(SpawnTime + Random.Range(-SpawnTime / 20, SpawnTime / 20));
            }

            _animator.SetBool("lay", false);
            Moving = true;
        }
    }
}
