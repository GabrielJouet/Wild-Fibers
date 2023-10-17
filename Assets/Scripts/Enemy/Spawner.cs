using System.Collections;
using UnityEngine;

/// <summary>
/// A class that will handles enemy spawning.
/// </summary>
public class Spawner : MonoBehaviour
{
    /// <summary>
    /// How many enemies are spawned.
    /// </summary>
    private int _spawnedEnemies;

    /// <summary>
    /// Index of the current pattern.
    /// </summary>
    private int _patternIndex;

    /// <summary>
    /// Index of the current enemy group.
    /// </summary>
    private int _enemyIndex;

    /// <summary>
    /// Current enemy group loaded.
    /// </summary>
    private EnemyGroup _enemyGroup;

    /// <summary>
    /// Level controller component used to handles end of wave.
    /// </summary>
    private LevelController _levelController;

    /// <summary>
    /// Random Path component.
    /// </summary>
    private RandomPath _randomPath;

    /// <summary>
    /// Does every enemy is dead?
    /// </summary>
    public bool EnemiesKilled { get; private set; }

    /// <summary>
    /// Does this wave is finished?
    /// </summary>
    public bool WaveFinished { get; private set; }

    /// <summary>
    /// Does this spawner waits for the end of the wave?
    /// </summary>
    public bool WaitEnd { get; set; }



    /// <summary>
    /// Method used to populate Spawner and start spawning
    /// </summary>
    /// <param name="newRandomPath">The component that handles pathfinding</param>
    /// <param name="newGroup">The new loaded group</param>
    /// <param name="newLevelController">The level controller component</param>
    public void SetNewGroup(RandomPath newRandomPath, EnemyGroup newGroup, LevelController newLevelController)
    {
        _spawnedEnemies = 0;
        _levelController = newLevelController;
        _enemyGroup = newGroup;
        _randomPath = newRandomPath;

        _patternIndex = 0;
        _enemyIndex = 0;
        EnemiesKilled = false;
        WaveFinished = false;

        StartCoroutine(SpawnEnemies());
    }


    /// <summary>
    /// Coroutine used to spawn enemies of the current loaded group
    /// </summary>
    /// <returns>Yield the time between enemies or groups</returns>
    private IEnumerator SpawnEnemies()
    {
        while (!WaveFinished)
        {
            //If we are not at the end of the pattern
            if (_enemyIndex < _enemyGroup.Patterns[_patternIndex].EnemiesCount)
            {
                _enemyIndex++;
                Instantiate(_enemyGroup.Enemy).GetComponent<Enemy>().Initialize(_randomPath.GeneratedPath, 0, this);

                _spawnedEnemies++;
                yield return new WaitForSeconds(_enemyGroup.Patterns[_patternIndex].EnemiesTime);
            }
            //Else if the pattern is finished
            else
            {
                //If the wave is not finished
                if (_patternIndex + 1 < _enemyGroup.Patterns.Count)
                {
                    _patternIndex++;
                    _enemyIndex = 0;
                    yield return new WaitForSeconds(_enemyGroup.TimeBetweenPattern);
                }
                //If the wave is finished
                else
                {
                    WaveFinished = true;

                    _levelController.EndWave();
                }
            }
        }
    }


    /// <summary>
    /// Method called by pools to track when all enemies are dead.
    /// </summary>
    public void EnemyKilled()
    {
        _spawnedEnemies--;

        if (_spawnedEnemies == 0 && WaitEnd)
        {
            EnemiesKilled = true;
            _levelController.EndLevel(false);
        }
    }
}