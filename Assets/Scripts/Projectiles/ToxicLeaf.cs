using UnityEngine;

public class ToxicLeaf : Projectile
{
    protected bool _following;

    protected float _angle;

    protected Vector2 _initialPosition;

    private bool _slowDown;



    //Method used to initialize class (like a constructor)
    public void Initialize(ProjectilePool newPool, Vector2 newPosition, bool slowDown)
    {
        _following = false;
        _angle = Random.Range(0, 360);

        StopAllCoroutines();

        _projectilePool = newPool;

        _initialPosition = newPosition;
        transform.position = _initialPosition;
        transform.localScale = new Vector3(1, 1, 1);

        _slowDown = slowDown;
    }


    public void StartFollowing(Enemy newEnemy, Attack newData)
    {
        if (!_following)
        {
            _attack = newData;
            _following = true;
            _enemyTracked = newEnemy;
        }
    }


    protected override void Update()
    {
        if (_following)
        {
            if (_enemyTracked != null)
                TrackEnemy();
            else if (FollowPoint(_goalPosition, true))
            {
                transform.parent = _projectilePool.transform;
                StopProjectile();
            }
        }
        else
        {
            _angle += Time.deltaTime;
            transform.localPosition = _initialPosition + new Vector2(Mathf.Sin(_angle) * 0.15f, Mathf.Cos(_angle) * 0.15f);
            transform.RotateAround(transform.position, new Vector3(0, 0, 1), _projectileSpeed * 4 * Time.deltaTime);
        }
    }


    /// <summary>
    /// Method called to hurt an enemy.
    /// </summary>
    /// <param name="enemy">The related enemy</param>
    protected override void AttackEnemy(Enemy enemy)
    {
        if (enemy != null)
        {
            if (_slowDown)
                enemy.ApplySlowDown(10, 2f);

            enemy.TakeDamage(_attack);
        }
    }
}
