using Enemies.Enemy_Types;
using UnityEngine;

namespace Towers.Projectiles
{
    /// <summary>
    /// Class used by projectile like object.
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        /// <summary>
        /// Speed of this projectile.
        /// </summary>
        [SerializeField]
        protected float projectileSpeed;


        /// <summary>
        /// Attack linked.
        /// </summary>
        protected Attack _attack;

        /// <summary>
        /// Which enemy is tracked by this projectile?
        /// </summary>
        protected Enemy _enemyTracked;

        /// <summary>
        /// The actual goal position of this projectile.
        /// </summary>
        protected Vector3 _goalPosition;

        /// <summary>
        /// Last position known of this enemy.
        /// </summary>
        protected Vector3 _lastKnownPosition;



        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="newData">Tower Data used to populate the class</param>
        /// <param name="newEnemy">New Enemy tracked</param>
        /// <param name="newTransform">The parent tower</param>
        public void Initialize(Attack newData, Enemy newEnemy, Transform newTransform)
        {
            transform.position = newTransform.position;
            _attack = newData;

            _enemyTracked = newEnemy;
        }


        /// <summary>
        /// Update is called each frame.
        /// </summary>
        protected virtual void Update()
        {
            if (_enemyTracked)
                TrackEnemy();
            else if (FollowPoint(_lastKnownPosition, true, false))
                StopProjectile();
        }



        /// <summary>
        /// Method used to track a specific enemy.
        /// </summary>
        protected virtual void TrackEnemy()
        {
            if (FollowPoint(_enemyTracked.DamageTransform.position, true))
            {
                AttackEnemy(_enemyTracked);
                StopProjectile();
            }
        }


        /// <summary>
        /// Method used to follow a point in space.
        /// </summary>
        /// <param name="position">The new position to move</param>
        /// <param name="rotate">Does the projectile rotate during move?</param>
        /// <param name="hasAnEnemyToTargets">Does the projectile follow an enemy?</param>
        protected bool FollowPoint(Vector3 position, bool rotate, bool hasAnEnemyToTargets = true)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, projectileSpeed * Time.deltaTime);

            if (rotate)
            {
                Vector3 vectorToTarget = position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            if (hasAnEnemyToTargets && _enemyTracked)
                _lastKnownPosition = position;
            
            if (!hasAnEnemyToTargets)
                Debug.Log(position);

            return (transform.position - position).magnitude < 0.025f;
        }


        /// <summary>
        /// Method called to hurt an enemy.
        /// </summary>
        /// <param name="enemy">The related enemy</param>
        protected virtual void AttackEnemy(Enemy enemy)
        {
            if (enemy)
                enemy.TakeDamage(_attack);
        }


        /// <summary>
        /// Method called when a projectile hurt an enemy or stop moving.
        /// </summary>
        public void StopProjectile()
        {
            Destroy(gameObject);
        }
    }
}