﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles boss like behavior, like spawning other enemies.
/// </summary>
public class Boss : Enemy
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

    /// <summary>
    /// Time between start and end of spawn.
    /// </summary>
    [SerializeField]
    protected float _spawnTime;

    /// <summary>
    /// Does the boss stop while spawning other enemies?
    /// </summary>
    [SerializeField]
    protected bool _stopWhileSpawning;

    /// <summary>
    /// How many enemies are spawned during each spawn.
    /// </summary>
    [SerializeField]
    protected int _numberOfEnemiesPerSpawn;

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


    protected Spawn _spawn;



    /// <summary>
    /// Initialize method.
    /// </summary>
    /// <param name="newPath">New path used</param>
    /// <param name="newPool">Pool used for the current enemy</param>
    /// <param name="pathIndex">Current progression on the path</param>
    public override void Initialize(List<Vector2> newPath, PoolController newPool, int pathIndex)
    {
        base.Initialize(newPath, newPool, pathIndex);

        if(_spawn == null)
            _spawn = new Spawn(_poolController, AvailablePaths, Spawnling);
        
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
            yield return new WaitForSeconds(_timeBetweenSpawn + Random.Range(-_timeBetweenSpawn / 20, _timeBetweenSpawn / 20));

            Moving = false;
            _animator.SetTrigger("lay");
            _spawn.SpawnSpawnling(_numberOfEnemiesPerSpawn, _pathIndex, _spawnTime);
            Moving = true;
        }
    }
}
