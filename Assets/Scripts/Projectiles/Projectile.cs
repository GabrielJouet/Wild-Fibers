using UnityEngine;

/// <summary>
/// Class used by projectile like object.
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected float _projectileSpeed;

    protected Attack _attack;

    /// <summary>
    /// The related pool.
    /// </summary>
    protected ProjectilePool _projectilePool;

    protected Enemy _enemyTracked;

    protected Vector3 _goalPosition;


    /// <summary>
    /// Constructor of the class.
    /// </summary>
    /// <param name="newData">Tower Data used to populate the class</param>
    /// <param name="newEnemy">New Enemy tracked</param>
    /// <param name="newPool">The projectile pool used</param>
    /// <param name="newTransform">The parent tower</param>
    public virtual void Initialize(Attack newData, Enemy newEnemy, ProjectilePool newPool, Transform newTransform)
    {
        transform.position = newTransform.position;
        _attack = newData;
        _projectilePool = newPool;

        _enemyTracked = newEnemy;
    }


    protected virtual void Update()
    {
        if (_enemyTracked != null)
            TrackEnemy();
        else if (FollowPoint(_goalPosition, true))
            StopProjectile();
    }


    //Method used to track an enemy moving 
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


    //Method used to follow a point moving in space
    //
    //Parameter => the point position in a vector 3
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
    /// Method called when a projectile hurt an enemy.
    /// </summary>
    public void StopProjectile()
    {
        gameObject.SetActive(false);
        _projectilePool.AddOneProjectile(this);
    }
}
