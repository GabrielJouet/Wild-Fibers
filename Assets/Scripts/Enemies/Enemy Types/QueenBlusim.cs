using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Enemy_Types
{
    /// <summary>
    /// Class that handles boss like behavior, like spawning other enemies.
    /// </summary>
    public class QueenBlusim : Enemy
    {
        [Header("Boss related")]

        /// <summary>
        /// The enemy spawned.
        /// </summary>
        [SerializeField]
        protected GameObject enemySpawnedPrefab;

        /// <summary>
        /// Time between each wave of spawned enemies.
        /// </summary>
        [SerializeField]
        protected float timeBetweenSpawn;

        /// <summary>
        /// Time between start and end of spawn.
        /// </summary>
        [SerializeField]
        protected float spawnTime;

        /// <summary>
        /// How many enemies are spawned during each spawn.
        /// </summary>
        [SerializeField]
        protected int numberOfEnemiesPerSpawn;



        /// <summary>
        /// Initialize method.
        /// </summary>
        /// <param name="newPath">New path used</param>
        /// <param name="pathIndex">Current progression on the path</param>
        /// <param name="spawner">Spawner that spawns this enemy</param>
        public override void Initialize(List<Vector2> newPath, int pathIndex, Spawner spawner)
        {
            base.Initialize(newPath, pathIndex, spawner);
        
            StartCoroutine(DelaySpawn());
        }


        /// <summary>
        /// Coroutine used to delay each spawn wave.
        /// </summary>
        /// <returns>Yield time between spawn and spawn times</returns>
        protected IEnumerator DelaySpawn()
        {
            while(true)
            {
                yield return new WaitForSeconds(timeBetweenSpawn + Random.Range(-timeBetweenSpawn / 20, timeBetweenSpawn / 20));

                _moving = false;
                animator.SetBool("lay", true);

                for (int i = 0; i < numberOfEnemiesPerSpawn; i++)
                {
                    Instantiate(enemySpawnedPrefab).GetComponent<Enemy>().Initialize(_path, _pathIndex, _spawner);

                    yield return new WaitForSeconds(spawnTime + Random.Range(-spawnTime / 20, spawnTime / 20));
                }

                animator.SetBool("lay", false);
                _moving = true;
            }
        }
    }
}