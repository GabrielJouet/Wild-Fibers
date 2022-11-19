using UnityEngine;

/// <summary>
/// Class used by projectile like object.
/// </summary>
public class Projectile : PoolableObject
{
    /// <summary>
    /// Speed of this projectile.
    /// </summary>
    [SerializeField]
    protected float _projectileSpeed;


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
    /// Constructor of the class.
    /// </summary>
    /// <param name="newData">Tower Data used to populate the class</param>
    /// <param name="newEnemy">New Enemy tracked</param>
    /// <param name="newTransform">The parent tower</param>
    public virtual void Initialize(Attack newData, Enemy newEnemy, Transform newTransform)
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
        if (_enemyTracked != null)
            TrackEnemy();
        else if (FollowPoint(_goalPosition, true))
            StopProjectile();
    }



    /// <summary>
    /// Method used to track a specific enemy.
    /// </summary>
    protected virtual void TrackEnemy()
    {
        if (_enemyTracked.gameObject.activeSelf)
        {
            if (FollowPoint(_enemyTracked.DamagePosition, true))
            {
                AttackEnemy(_enemyTracked);
                StopProjectile();
            }
        }
        else
        {
            _goalPosition = _enemyTracked.DamagePosition;
            _enemyTracked = null;
        }
    }


    /// <summary>
    /// Method used to follow a point in space.
    /// </summary>
    /// <param name="position">The new position to move</param>
    /// <param name="rotate">Does the projectile rotate during move?</param>
    protected bool FollowPoint(Vector3 position, bool rotate)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, _projectileSpeed * Time.deltaTime);

        if (rotate)
        {
            Vector3 vectorToTarget = position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        return (transform.position - position).magnitude < 0.025f;
    }


    /// <summary>
    /// Method called to hurt an enemy.
    /// </summary>
    /// <param name="enemy">The related enemy</param>
    protected virtual void AttackEnemy(Enemy enemy)
    {
        if (enemy != null)
            enemy.TakeDamage(_attack);
    }


    /// <summary>
    /// Method called when a projectile hurt an enemy or stop moving.
    /// </summary>
    public void StopProjectile()
    {
        Controller.Instance.PoolController.In(GetComponent<PoolableObject>());
    }
}