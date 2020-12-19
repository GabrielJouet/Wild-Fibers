using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn 
{
    protected LevelController _levelController;

    protected List<List<Vector2>> _paths;

    protected Animator _animator;

    protected EnemyPool _enemyPool;

    public Enemy Spawnling { get; private set; }



    public Spawn(LevelController newController, List<List<Vector2>> newPaths, Animator newAnimator, Enemy newEnemy, EnemyPool newPool)
    {
        _levelController = newController;
        _paths = newPaths;
        _animator = newAnimator;
        Spawnling = newEnemy;
        _enemyPool = newPool;
    }


    /// <summary>
    /// Coroutine used to delay each spawn wave.
    /// </summary>
    /// <returns>Yield time between spawn and spawn times</returns>
    public IEnumerator SpawnSpawnling(int numberOfEnemies, int pathIndex, float spawnTime)
    {
        _animator.SetTrigger("lay");
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Enemy hatchling = _levelController.RecoverPool(Spawnling).GetOneEnemy();
            hatchling.Initialize(_paths[Random.Range(0, _paths.Count)], _enemyPool, pathIndex);

            yield return new WaitForSeconds(spawnTime + Random.Range(-spawnTime / 20, spawnTime / 20));
        }
    }


    public void ChangeSpawnling(Enemy newSpawnling, EnemyPool newPool)
    {
        Spawnling = newSpawnling;
        _enemyPool = newPool;
    }
}
