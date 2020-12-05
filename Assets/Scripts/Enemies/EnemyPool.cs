using System.Collections.Generic;
using UnityEngine;

/*
 * Class used to handle enemy initialisation and memory management0
 * Each instance of this class is used for one type of enemy
 */
public class EnemyPool : MonoBehaviour
{
    //Base enemy prefab
    public Enemy Enemy { get; private set; }

    //List of non used enemies but yet instanciated and desactivated
    private readonly Stack<Enemy> _enemyPool = new Stack<Enemy>();

    //Ressources controller used when an enemy dies or reaches the end
    private RessourceController _ressourceController;

    //List of living enemies, used to know when a wave is done
    private readonly List<Enemy> _livingEnemies = new List<Enemy>();

    //Does the pool wait for the end of the wave?
    private bool _waitForEnd = false;

    //Spawner that uses the pool
    private Spawner _spawner;



    //Method used instead of using start (kinda a constructor)
    //
    //Parameters => newPrefab, the new enemy prefab used in spawn
    //              newRessourceController, the ressource controller used to records and save life and gold
    public void Initialize(Enemy newPrefab, RessourceController newRessourceController)
    {
        _ressourceController = newRessourceController;
        Enemy = newPrefab;
    }


    //Method used to recover one enemy
    //
    //Return a new enemy either already instanciated or newly created
    public Enemy GetOneEnemy()
    {
        Enemy enemyBuffered;

        //If enemies are available in the pool
        if (_enemyPool.Count > 0)
        {
            //We recover and return one
            enemyBuffered = _enemyPool.Pop();
            _livingEnemies.Add(enemyBuffered);
            return enemyBuffered;
        }
        //Either no enemy is available
        else
        {
            //We spawn a brand new one
            enemyBuffered = Instantiate(Enemy, transform);
            _livingEnemies.Add(enemyBuffered);
            return enemyBuffered;
        }
    }



    //Method used to add one enemy to the pool
    //
    //Parameters => newEnemy, game object of the enemy dead or desactivated
    //              stillAlive, does the enemy was still alive when retrieved?
    //              livesLostOrGoldGained, either a lives count lost or a money gained
    public void AddOneEnemy(Enemy newEnemy, bool stillAlive, int livesLostOrGoldGained) 
    {
        //If the enemy is in the living enemies list we remove it
        if (_livingEnemies.Contains(newEnemy))
        {
            _livingEnemies.Remove(newEnemy); 

            //If we wait for the end of the level and the living enemies list is empty we consider every enemy is dead
            if (_waitForEnd && _livingEnemies.Count == 0)
                _spawner.AllEnemiesKilled();
        }

        //We desactivate it
        newEnemy.gameObject.SetActive(false);
        _enemyPool.Push(newEnemy);

        //If the enemy was still alive it means it reaches the end of the level
        if (stillAlive)
            _ressourceController.RemoveLives(livesLostOrGoldGained);
        //Else the enemy was killed by a tower
        else
            _ressourceController.AddGold(livesLostOrGoldGained);
    }


    //Method used when all enemies are spawned and we want to track when the wave is finished
    //
    //Parameter => newSpawner, the spawner that uses the pool
    public void RecordLevelEnd(Spawner newSpawner)
    {
        _waitForEnd = true;
        _spawner = newSpawner;

        if (_livingEnemies.Count == 0)
            _spawner.AllEnemiesKilled();
    }
}