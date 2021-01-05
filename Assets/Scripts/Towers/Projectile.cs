using UnityEngine;

/// <summary>
/// Class used by projectile like object.
/// </summary>
public class Projectile : MonoBehaviour
{
    protected TowerData _data;

    /// <summary>
    /// The related pool.
    /// </summary>
    protected ProjectilePool _projectilePool;

    protected Enemy _enemyTracked;

    private Vector3 _goalPosition;


    /// <summary>
    /// Constructor of the class.
    /// </summary>
    /// <param name="newData">Tower Data used to populate the class</param>
    /// <param name="newEnemy">New Enemy tracked</param>
    /// <param name="newPool">The projectile pool used</param>
    public virtual void Initialize(TowerData newData, Enemy newEnemy, ProjectilePool newPool)
    {
        _data = newData;
        _projectilePool = newPool;

        _enemyTracked = newEnemy;
    }


    /// <summary>
    /// Constructor of the class.
    /// </summary>
    /// <param name="newData">Tower Data used to populate the class</param>
    /// <param name="newPool">The projectile pool used</param>
    public virtual void Initialize(TowerData newData, ProjectilePool newPool)
    {
        _data = newData;
        _projectilePool = newPool;
    }


    protected virtual void Update()
    {
        if (_enemyTracked != null)
            TrackEnemy();
        else if (FollowPoint(_goalPosition))
            StopProjectile();
    }


    //Method used to track an enemy moving 
    private void TrackEnemy()
    {
        if (_enemyTracked.gameObject.activeSelf)
        {
            if (FollowPoint(_enemyTracked.DamagePosition))
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
    private bool FollowPoint(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, _data.ProjectileSpeed * Time.deltaTime);

        Vector3 vectorToTarget = position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        return (transform.position - position).magnitude < 0.025f;
    }


    /// <summary>
    /// Method called to hurt an enemy.
    /// </summary>
    /// <param name="enemy">The related enemy</param>
    protected void AttackEnemy(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.TakeDamage(_data.Damage, _data.ArmorThrough);

            if (_data.DotIcon != null)
                enemy.ApplyDot(_data.ArmorThroughMalus, _data.Dot, _data.DotDuration, _data.DotIcon);
        }
    }


    /// <summary>
    /// Method called when a projectile hurt an enemy.
    /// </summary>
    protected void StopProjectile()
    {
        gameObject.SetActive(false);
        _projectilePool.AddOneProjectile(this);
    }
}
