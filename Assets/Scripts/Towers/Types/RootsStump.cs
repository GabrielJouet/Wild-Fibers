using System.Collections;
using System.Collections.Generic;
using Enemies.Enemy_Types;
using Towers.Projectiles;
using UnityEngine;

namespace Towers.Types
{
    /// <summary>
    /// Class used by roots-like towers for behavior modification purposes.
    /// </summary>
    public class RootsStump : Tower
    {
        /// <summary>
        /// All roots projectiles available.
        /// </summary>
        protected List<ChocSpikes> _availableSpikes = new List<ChocSpikes>();

        /// <summary>
        /// Next root spawn index.
        /// </summary>
        protected int _rootsIndex = 0;



        /// <summary>
        /// Method used to update special behavior.
        /// </summary>
        protected override void SpecialBehavior()
        {
            for (int i = 0; i < Shots; i ++)
                StartCoroutine(SummonSpike(0.25f));
        }


        /// <summary>
        /// FixedUpdate, called 50 times a second.
        /// </summary>
        protected override void FixedUpdate()
        {
            if (_availableEnemies.Count > 0 && _availableSpikes.Count > 0)
            {
                List<Enemy> enemies = RecoverAvailableEnemies(_availableSpikes.Count);

                for (int i = 0; i < enemies.Count; i++)
                {
                    StartCoroutine(SummonSpike(1));

                    _availableSpikes[0].StartFollowing(enemies[i], _nextAttack.Dequeue(), AugmentationLevel > 1);
                    _availableSpikes.RemoveAt(0);
                }
            }
        }


        /// <summary>
        /// Method called before an attack occurs to apply modifications on the current attack.
        /// </summary>
        /// <param name="enemy">The enemy targeted</param>
        /// <returns>The new attack with new parameters</returns>
        protected override Attack ChangeNextAttack(Enemy enemy)
        {
            Attack newAttack = new Attack(_attack);

            if (AugmentationLevel > 2)
                newAttack.Damage *= (enemy.IsDotted ? 1.25f : 1);

            if (AugmentationLevel > 4)
                newAttack.ArmorThrough *= (enemy.ArmorMax < 25 ? 1.15f : 1);

            if (AugmentationLevel > 3)
                newAttack.Damage *= Random.Range(0, 100) < 5 ? 2 : 1;

            return newAttack;
        }


        /// <summary>
        /// Method used to update upgrade special behavior.
        /// </summary>
        protected override void UpgradeSpecialBehavior()
        {
            StopAllCoroutines();

            foreach (ChocSpikes current in _availableSpikes)
                current.StopProjectile();
        }


        /// <summary>
        /// Method called when the tower is resell.
        /// </summary>
        protected override void ResellSpecialBehavior()
        {
            foreach (ChocSpikes current in _availableSpikes)
                current.StopProjectile();
        }


        /// <summary>
        /// Method used to summon projectile needed.
        /// </summary>
        /// <param name="multiplier">Coroutine execution time multiplier</param>
        protected IEnumerator SummonSpike(float multiplier)
        {
            yield return new WaitForSeconds(TimeShots * multiplier);

            ChocSpikes buffer = Instantiate(Projectile).GetComponent<ChocSpikes>();
            buffer.Initialize(GetSpikePosition(_rootsIndex));

            _rootsIndex = _rootsIndex == Shots - 1? 0 : _rootsIndex + 1 ;

            _availableSpikes.Add(buffer);
        }


        /// <summary>
        /// Recover spike positions.
        /// </summary>
        /// <param name="index">The root index needed</param>
        /// <returns>Return the position of the root needed</returns>
        protected Vector2 GetSpikePosition(int index)
        {
            Vector2 localPosition = transform.localPosition;
            switch (index)
            {
                case 0:
                    return new Vector2(localPosition.x - 0.2f, localPosition.y + 0.1f);

                case 1:
                    return new Vector2(localPosition.x + 0.2f, localPosition.y + 0.1f);

                case 2:
                {
                    if (Shots == 3)
                        return new Vector2(localPosition.x, localPosition.y - 0.15f);
                    
                    if (Shots == 4)
                        return new Vector2(localPosition.x + 0.2f, localPosition.y - 0.1f);
                    
                    break;
                }

                case 3:
                    return new Vector2(localPosition.x - 0.2f, localPosition.y - 0.1f);

                default:
                    return Vector2.zero;
            }

            return Vector2.zero;
        }
    }
}