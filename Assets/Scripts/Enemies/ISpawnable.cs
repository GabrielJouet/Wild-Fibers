using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable
{
    /// <summary>
    /// The enemy spawned.
    /// </summary>
    Enemy Spawnling { get; }

    /// <summary>
    /// Time between each wave of spawned enemies.
    /// </summary>
    float TimeBetweenSpawn { get; }

    /// <summary>
    /// Time between start and end of spawn.
    /// </summary>
    float SpawnTime { get; }

    /// <summary>
    /// Does the boss stop while spawning other enemies?
    /// </summary>
    bool StopWhileSpawning { get; }

    /// <summary>
    /// How many enemies are spawned during each spawn.
    /// </summary>
    int NumberOfEnemiesPerSpawn { get; }

    /// <summary>
    /// The number of paths used by new enemies.
    /// </summary>
    int PathsWanted { get; }

    /// <summary>
    /// Available paths generated.
    /// </summary>
    List<List<Vector2>> AvailablePaths { get; set; }


    /// <summary>
    /// Coroutine used to delay each spawn wave.
    /// </summary>
    IEnumerator DelaySpawn();
}
