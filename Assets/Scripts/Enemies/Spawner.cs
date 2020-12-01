using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is used to control one enemy pool and enemy spawn
 */
public class Spawner : MonoBehaviour
{
    //Index of the enemy pattern inside a wave
    private int _patternIndex = 0;

    //Index of enemy inside a pattern
    private int _enemyIndex = 0;


    //Does the wave finished?
    public bool WaveFinished { get; private set; } = false;


    //Enemy group used in spawn
    private EnemyGroup _enemyGroup;

    //Level controller used when no enemies are left or wave is finished
    private LevelController _levelController;

    //Enemy pool of the current wave
    private EnemyPool _enemyPool;

    private RandomPath _randomPath;


    //Does every enemy is dead?
    public bool EnemiesKilled { get; private set; } = false;



    //Method used by LevelController to set a new enemy group and start spawning entities
    //
    //Parameters => newGroup, the new group of enemy for this wave
    //              newLevelController, the level controller of the current level
    //              newEnemyPool, enemy pool to retrieve already instantiated enemies
    public void SetNewGroup(RandomPath newRandomPath, EnemyGroup newGroup, LevelController newLevelController, EnemyPool newEnemyPool)
    {
        //We reset and set variables
        _levelController = newLevelController;
        _enemyPool = newEnemyPool;

        _enemyGroup = newGroup;
        _randomPath = newRandomPath;

        WaveFinished = false;

        //And we launch enemies spawn
        StartCoroutine(SpawnEnemies());
    }


    //Coroutine used to spawn enemies in group
    private IEnumerator SpawnEnemies()
    {
        while (!WaveFinished)
        {
            //If we are not at the end of the pattern
            if (_enemyIndex < _enemyGroup.Patterns[_patternIndex].EnemiesCount)
            {
                _enemyIndex++;
                Enemy buffer = _enemyPool.GetOneEnemy();
                buffer.Initialize(_randomPath.CalculateRandomPath(), _enemyPool, 0);

                //If the enemy is a boss we add it more paths to spawn enemies
                if (buffer.TryGetComponent(out Boss bossComponent))
                {
                    List<List<Vector2>> newPaths = new List<List<Vector2>>();
                    for (int i = 0; i < bossComponent.PathsWanted; i++)
                        newPaths.Add(_randomPath.CalculateRandomPath());
                    bossComponent.AvailablePaths = newPaths;
                }

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


    //Method used when the wave is finished to contact LevelController
    private void EndSpawn()
    {
        _patternIndex = 0;
        _enemyIndex = 0;
        _enemyGroup = null;
        WaveFinished = true;

        _levelController.EndWave();
    }


    //Method used when all waves are spawned and level is finished
    public void NotifyPool()
    {
        _enemyPool.RecordLevelEnd(this);
    }


    //Method used when every enemy of the group is killed
    public void AllEnemiesKilled()
    {
        EnemiesKilled = true;
        _levelController.EndLevel();
    }
}