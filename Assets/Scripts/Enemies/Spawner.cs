﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private int _patternIndex = 0;
    private int _enemyIndex = 0;

    private bool _waveFinished = false;

    private EnemyGroup _enemyGroup;
    private LevelController _levelController;
    private EnemyPool _enemyPool;

    private List<Path> _paths = new List<Path>();

    private bool _enemiesKilled = false;



    //Method used by LevelController to set a new enemy group and start spawning entities
    public void SetNewGroup(EnemyGroup newGroup, LevelController newLevelController, List<Path> newPaths, EnemyPool newEnemyPool)
    {
        _levelController = newLevelController;
        _enemyPool = newEnemyPool;

        _enemyGroup = newGroup;
        _paths = newPaths;

        _waveFinished = false;

        StartCoroutine(SpawnEnemies());
    }


    //Coroutine used to spawn enemies in group
    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(2f);

        while(!_waveFinished)
        {
            //If we are not at the end of the pattern
            if (_enemyIndex < _enemyGroup.GetEnemyPattern(_patternIndex).GetNumberOfEnemies())
            {
                _enemyIndex ++;
                _enemyPool.GetOneEnemy().GetComponent<Enemy>().Initialize(_paths[_enemyGroup.GetPathIndex()], _enemyPool);
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

                    yield return new WaitForSeconds(_enemyGroup.GetTimeBetweenPattern());
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
        _waveFinished = true;

        _levelController.EndWave();
    }


    public void NotifyPool()
    {
        _enemyPool.RecordLevelEnd(this);
    }


    public void EnemiesKilled()
    {
        _enemiesKilled = true;
        _levelController.EndLevel();
    }


    public bool GetEnemiesKilled() { return _enemiesKilled; }

    public bool GetWaveFinished() { return _waveFinished; }
}