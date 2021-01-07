using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn 
{
    protected PoolController _poolController;

    protected List<List<Vector2>> _paths = new List<List<Vector2>>();

    public Enemy Spawnling { get; private set; }



    public Spawn(PoolController newPoolController, List<List<Vector2>> newPaths, Enemy newEnemy)
    {
        _poolController = newPoolController;
        _paths = newPaths;
        Spawnling = newEnemy;
    }


    public Spawn(PoolController newPoolController, List<Vector2> newPath, Enemy newEnemy)
    {
        _poolController = newPoolController;

        _paths.Clear();
        _paths.Add(newPath);

        Spawnling = newEnemy;
    }


    /// <summary>
    /// Coroutine used to delay each spawn wave.
    /// </summary>
    /// <returns>Yield time between spawn and spawn times</returns>
    public IEnumerator SpawnSpawnling(int numberOfEnemies, int pathIndex, float spawnTime)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Enemy hatchling = _poolController.RecoverEnemyPool(Spawnling).GetOneEnemy();
            hatchling.Initialize(_paths[Random.Range(0, _paths.Count)], _poolController, pathIndex);

            yield return new WaitForSeconds(spawnTime + Random.Range(-spawnTime / 20, spawnTime / 20));
        }
    }

    public IEnumerator SpawnSpawnling(int pathIndex, float spawnTime)
    {
        Enemy hatchling = _poolController.RecoverEnemyPool(Spawnling).GetOneEnemy();
        hatchling.Initialize(_paths[Random.Range(0, _paths.Count)], _poolController, pathIndex);

        yield return new WaitForSeconds(spawnTime + Random.Range(-spawnTime / 20, spawnTime / 20));
    }


    public void SpawnSpawnling(int pathIndex)
    {
        Enemy hatchling = _poolController.RecoverEnemyPool(Spawnling).GetOneEnemy();
        hatchling.Initialize(_paths[Random.Range(0, _paths.Count)], _poolController, pathIndex);
    }


    public void ChangeSpawnling(Enemy newSpawnling)
    {
        Spawnling = newSpawnling;
    }
}
