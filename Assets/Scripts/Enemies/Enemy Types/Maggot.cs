using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Enemy_Types
{
    /// <summary>
    /// Maggot enemy type, will cocoon itself to revive as another enemy.
    /// </summary>
    public class Maggot : Enemy
    {
        [Header("Hatching related")]

        /// <summary>
        /// The newborn enemy.
        /// </summary>
        [SerializeField]
        protected GameObject hatching;

        /// <summary>
        /// Time before the maggot cocoon itself.
        /// </summary>
        [SerializeField]
        protected float hatchingTime;


        [Header("Shield related")]
    
        /// <summary>
        /// Shield value when not protecting.
        /// </summary>
        [SerializeField]
        protected int baseShieldValue;



        /// <summary>
        /// Method called for initialization.
        /// </summary>
        /// <param name="newPath">The new path used</param>
        /// <param name="newPool">The pool used when dying or finishing path</param>
        /// <param name="pathIndex">Current progression on the path</param>
        public override void Initialize(List<Vector2> newPath, int pathIndex, Spawner spawner)
        {
            base.Initialize(newPath, pathIndex, spawner);
            Armor = baseShieldValue;

            StartCoroutine(DelaySpawn());
        }


        /// <summary>
        /// Coroutine used to delay the hatch of the maggot.
        /// </summary>
        /// <returns>Yield the hatchling time and cocooning time</returns>
        protected IEnumerator DelaySpawn()
        {
            yield return new WaitForSeconds(hatchingTime + Random.Range(-hatchingTime / 20, hatchingTime / 20));
            animator.SetTrigger("cocoon");

            _moving = false;

            if (_dots.Count > 0)
                Armor = ArmorMax - (ArmorMax - Armor);
            else
                Armor = ArmorMax;

            yield return new WaitForSeconds((animator.runtimeAnimatorController.animationClips[1].length / 0.3f) + 0.05f);

            Instantiate(hatching).GetComponent<Enemy>().Initialize(_path, _pathIndex, _spawner);

            GoldGained = 0;
            Die(false);
        }
    }
}