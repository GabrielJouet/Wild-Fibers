using System.Collections;
using UnityEngine;

/// <summary>
/// A class that will handles enemy spawning.
/// </summary>
public class Spawner : MonoBehaviour
{
    /// <summary>
    /// Index of the current pattern.
    /// </summary>
    private int _patternIndex = 0;

    /// <summary>
    /// Index of the current enemy group.
    /// </summary>
    private int _enemyIndex = 0;

    /// <summary>
    /// Current enemy group loaded.
    /// </summary>
    private EnemyGroup _enemyGroup;

    /// <summary>
    /// Level controller component used to handles end of wave.
    /// </summary>
    private LevelController _levelController;
    
    /// <summary>
    /// Pool controller that handles enemies and resources.
    /// </summary>
    private PoolController _poolController;

    /// <summary>
    /// Enemy pool used to recover and spawns enemies.
    /// </summary>
    private EnemyPool _enemyPool;

    /// <summary>
    /// Random Path component.
    /// </summary>
    private RandomPath _randomPath;

    /// <summary>
    /// Does every enemy is dead?
    /// </summary>
    public bool EnemiesKilled { get; private set; } = false;

    /// <summary>
    /// Does this wave is finished?
    /// </summary>
    public bool WaveFinished { get; private set; } = false;



    /// <summary>
    /// Method used to populate Spawner and start spawning
    /// </summary>
    /// <param name="newRandomPath">The component that handles pathfinding</param>
    /// <param name="newGroup">The new loaded group</param>
    /// <param name="newLevelController">The level controller component</param>
    /// <param name="newEnemyPool">The new enemy pool component</param>
    public void SetNewGroup(RandomPath newRandomPath, EnemyGroup newGroup, LevelController newLevelController, PoolController newPoolController)
    {
        _levelController = newLevelController;
        _poolController = newPoolController;
        _enemyGroup = newGroup;
        _randomPath = newRandomPath;

        _enemyPool = _poolController.RecoverEnemyPool(_enemyGroup.Enemy.GetComponent<Enemy>());

        _patternIndex = 0;
        _enemyIndex = 0;
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
                Enemy buffer = _enemyPool.GetOneEnemy();
                buffer.Initialize(_randomPath.CalculateRandomPath(), _poolController, 0);

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
                    EndSpawn();
            }
        }
    }


    /// <summary>
    /// Method called when the wave is finished for this group.
    /// </summary>
    private void EndSpawn()
    {
        _enemyGroup = null;
        WaveFinished = true;

        _levelController.EndWave();
    }


    /// <summary>
    /// Method used to flag enemy pool to start tracking.
    /// </summary>
    public void NotifyPool()
    {
        _enemyPool.RecordLevelEnd(this);
    }


    /// <summary>
    /// Method called by pools to track when all enemies are dead.
    /// </summary>
    public void AllEnemiesKilled()
    {
        EnemiesKilled = true;
        _levelController.EndLevel(false);
    }
}