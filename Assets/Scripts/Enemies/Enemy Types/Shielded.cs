using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Enemy_Types
{
    /// <summary>
    /// Shielded enemy, from time to time this enemy will stop and protects itself.
    /// </summary>
    public class Shielded : Enemy
    {
        [Header("Shield related")]

        /// <summary>
        /// Time between every shield.
        /// </summary>
        [SerializeField]
        protected float timeBetweenShield;

        /// <summary>
        /// Shield value when not shielding.
        /// </summary>
        [SerializeField]
        protected int baseShieldValue;



        /// <summary>
        /// Initialize method.
        /// </summary>
        /// <param name="newPath">New path used</param>
        /// <param name="pathIndex">Current progression on the path</param>
        public override void Initialize(List<Vector2> newPath, int pathIndex, Spawner spawner)
        {
            base.Initialize(newPath, pathIndex, spawner);
            Armor = baseShieldValue;
            StartCoroutine(DelayShield());
        }


        /// <summary>
        /// Coroutine used to delay every shield use.
        /// </summary>
        protected IEnumerator DelayShield()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBetweenShield + Random.Range(-timeBetweenShield / 20, timeBetweenShield / 20));

                _moving = false;

                if (_dots.Count > 0)
                    Armor = ArmorMax - (ArmorMax - Armor);
                else
                    Armor = ArmorMax;
                animator.SetTrigger("shield");

                yield return new WaitForSeconds(animator.runtimeAnimatorController.animationClips[1].length / 0.5f);

                if (_dots.Count > 0)
                    Armor = baseShieldValue - (ArmorMax - Armor);
                else
                    Armor = baseShieldValue;

                _moving = true;
            }
        }
    }
}