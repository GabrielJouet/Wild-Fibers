using System.Collections;

/// <summary>
/// Interface used for spawn of entity.
/// </summary>
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
    /// Coroutine used to delay each spawn wave.
    /// </summary>
    /// <returns>Yield time between spawn and spawn times</returns>
    IEnumerator DelaySpawn();
}
