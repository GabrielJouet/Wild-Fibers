﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to handles every enemy, active or not.
/// </summary>
public class EnemyPool : MonoBehaviour
{
    /// <summary>
    /// Enemy prefab used
    /// </summary>
    public Enemy Enemy { get; private set; }

    /// <summary>
    /// Pool of enemies.
    /// </summary>
    private readonly Stack<Enemy> _enemyPool = new Stack<Enemy>();

    /// <summary>
    /// Ressource controller used when an enemy reaches the end.
    /// </summary>
    public RessourceController RessourceController { get; set; }

    /// <summary>
    /// List of living enemies, in order to knowing when the wave is done.
    /// </summary>
    private readonly List<Enemy> _livingEnemies = new List<Enemy>();

    /// <summary>
    /// Does the pool is flagged and wait end?
    /// </summary>
    private bool _waitForEnd = false;

    /// <summary>
    /// The spawner related to current pool.
    /// </summary>
    private List<Spawner> _spawners;


    /// <summary>
    /// Method used to populate the pool.
    /// </summary>
    /// <param name="newPrefab">The new enemy prefab to used</param>
    /// <param name="newRessourceController">The new resource controller to call</param>
    public void Initialize(Enemy newPrefab, RessourceController newRessourceController)
    {
        RessourceController = newRessourceController;

        if(newPrefab != null)
            Enemy = newPrefab;

        _spawners = new List<Spawner>();
        _livingEnemies.Clear();
        _waitForEnd = false;
    }


    /// <summary>
    /// Method to recover one enemy.
    /// </summary>
    /// <returns>One enemy, either freshly spawned or old one desactivated</returns>
    public Enemy GetOneEnemy()
    {
        if (_enemyPool.Count > 0)
        {
            Enemy enemyBuffered = _enemyPool.Pop();
            _livingEnemies.Add(enemyBuffered);

            return enemyBuffered;
        }
        else
        {
            Enemy enemyBuffered = Instantiate(Enemy, transform);
            _livingEnemies.Add(enemyBuffered);

            return enemyBuffered;
        }
    }



    /// <summary>
    /// Method to add one enemy to the pool
    /// </summary>
    /// <param name="newEnemy">The enemy to recover</param>
    /// <param name="stillAlive">Does the enemy reaches the end?</param>
    /// <param name="livesLostOrGoldGained">How many lives were lost or how many gold was gained</param>
    public void AddOneEnemy(Enemy newEnemy, bool stillAlive, int livesLostOrGoldGained) 
    {
        _livingEnemies.Remove(newEnemy); 

        if (_waitForEnd && _livingEnemies.Count == 0)
            foreach (Spawner spawner in _spawners)
                spawner.AllEnemiesKilled();

        newEnemy.gameObject.SetActive(false);
        _enemyPool.Push(newEnemy);

        if (stillAlive)
            RessourceController.RemoveLives(livesLostOrGoldGained);
        else
            RessourceController.AddGold(livesLostOrGoldGained, true);
    }



    /// <summary>
    /// Method to add one enemy to the pool
    /// </summary>
    /// <param name="newEnemy">The enemy to recover</param>
    public void AddOneEnemy(Enemy newEnemy)
    {
        _livingEnemies.Remove(newEnemy);

        newEnemy.gameObject.SetActive(false);
        _enemyPool.Push(newEnemy);
    }


    /// <summary>
    /// Method called by spawner when the wave is finished spawning.
    /// </summary>
    /// <param name="newSpawner">New spawner to callback</param>
    public void RecordLevelEnd(Spawner newSpawner)
    {
        _waitForEnd = true;
        _spawners.Add(newSpawner);

        if (_livingEnemies.Count == 0)
            foreach (Spawner spawner in _spawners)
                spawner.AllEnemiesKilled();
    }


    /// <summary>
    /// Method called by pool controller when the level is either loading or unloading.
    /// </summary>
    public void DesactivateEnemies()
    {
        List<Enemy> bufferList = new List<Enemy>(_livingEnemies);

        foreach (Enemy current in bufferList)
            AddOneEnemy(current);
    }
}