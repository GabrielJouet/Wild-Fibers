using System;
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
    private bool _waveFinished = false;


    //Enemy group used in spawn
    private EnemyGroup _enemyGroup;

    //Level controller used when no enemies are left or wave is finished
    private LevelController _levelController;

    //Enemy pool of the current wave
    private EnemyPool _enemyPool;


    //Available paths for enemies
    private List<Path> _paths = new List<Path>();


    //Does every enemy is dead?
    private bool _enemiesKilled = false;


    private DateTime _coroutineStartTime;

    private float _coroutineTimeNeeded = 0f;

    private bool _spawnPaused = false;



    //Method used by LevelController to set a new enemy group and start spawning entities
    //
    //Parameters => newGroup, the new group of enemy for this wave
    //              newLevelController, the level controller of the current level
    //              newPaths, available paths on this level
    //              newEnemyPool, enemy pool to retrieve already instantiated enemies
    public void SetNewGroup(EnemyGroup newGroup, LevelController newLevelController, List<Path> newPaths, EnemyPool newEnemyPool)
    {
        //We reset and set variables
        _levelController = newLevelController;
        _enemyPool = newEnemyPool;

        _enemyGroup = newGroup;
        _paths = newPaths;

        _waveFinished = false;

        //And we launch enemies spawn
        StartCoroutine(SpawnEnemies());
    }


    //Coroutine used to spawn enemies in group
    private IEnumerator SpawnEnemies()
    {
        //If the coroutine was previously stopped
        if(_coroutineTimeNeeded != 0)
        {
            _coroutineStartTime = DateTime.Now;
            yield return new WaitForSeconds(_coroutineTimeNeeded);
        }
        else
        {
            _coroutineStartTime = DateTime.Now;
            _coroutineTimeNeeded = 2f;
            yield return new WaitForSeconds(2f);
        }

        while (!_waveFinished)
        {
            //If we are not at the end of the pattern
            if (_enemyIndex < _enemyGroup.GetEnemyPattern(_patternIndex).GetNumberOfEnemies())
            {
                _enemyIndex++;
                _enemyPool.GetOneEnemy().GetComponent<Enemy>().Initialize(_paths[_enemyGroup.GetPathIndex()], _enemyPool);

                _coroutineStartTime = DateTime.Now;
                _coroutineTimeNeeded = _enemyGroup.GetEnemyPattern(_patternIndex).GetTimeBetweenEnemies();
                yield return new WaitForSeconds(_enemyGroup.GetEnemyPattern(_patternIndex).GetTimeBetweenEnemies());
            }
            //Else if the pattern is finished
            else
            {
                //If the wave is not finished
                if (_patternIndex + 1 < _enemyGroup.GetEnemyPatternCount())
                {
                    _patternIndex++;
                    _enemyIndex = 0;

                    _coroutineStartTime = DateTime.Now;
                    _coroutineTimeNeeded = _enemyGroup.GetTimeBetweenPattern();
                    yield return new WaitForSeconds(_enemyGroup.GetTimeBetweenPattern());
                }
                //If the wave is finished
                else
                    EndSpawn();
            }
        }
    }


    //Method used to pause spawn enemy when Pause Controller is called
    public void PauseSpawn()
    {
        if (!_spawnPaused)
        {
            StopAllCoroutines();
            _coroutineTimeNeeded -= (float)(DateTime.Now - _coroutineStartTime).TotalSeconds;
        }
        else
            StartCoroutine(SpawnEnemies());

        _spawnPaused = !_spawnPaused;
    }


    //Method used when the wave is finished to contact LevelController
    private void EndSpawn()
    {
        _patternIndex = 0;
        _enemyIndex = 0;
        _enemyGroup = null;
        _waveFinished = true;

        _levelController.EndWave();
    }


    //Method used when all waves are spawned and level is finished
    public void NotifyPool()
    {
        _enemyPool.RecordLevelEnd(this);
    }


    //Method used when every enemy of the group is killed
    public void EnemiesKilled()
    {
        _enemiesKilled = true;
        _levelController.EndLevel();
    }



    //Getters
    public bool GetEnemiesKilled() { return _enemiesKilled; }

    public bool GetWaveFinished() { return _waveFinished; }
}