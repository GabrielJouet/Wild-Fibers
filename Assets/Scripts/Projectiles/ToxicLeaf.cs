using UnityEngine;

public class ToxicLeaf : Projectile
{
    protected bool _following;

    protected float _angle;

    protected Vector2 _initialPosition;

    private bool _slowDown;

    private bool _bigger;



    //Method used to initialize class (like a constructor)
    public void Initialize(TowerData newData, ProjectilePool newPool, Vector2 newPosition, bool slowDown, bool bigger)
    {
        _following = false;
        _angle = Random.Range(0, 360);

        StopAllCoroutines();

        _data = newData;
        _projectilePool = newPool;

        _initialPosition = newPosition;
        transform.position = _initialPosition;
        transform.localScale = new Vector3(1, 1, 1);

        _slowDown = slowDown;
        _bigger = bigger;

        if (bigger)
            transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
    }


    public void StartFollowing(Enemy newEnemy, TowerData newData)
    {
        if (!_following)
        {
            _data = newData;
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
            transform.RotateAround(transform.position, new Vector3(0, 0, 1), _data.ProjectileSpeed * 4 * Time.deltaTime);
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
            enemy.TakeDamage(_data.ArmorThrough, _data.Damage);

            if (_data.DotIcon != null)
            {
                if (_bigger)
                    enemy.ApplyDot(_data.ArmorThroughMalus * 1.75f, Mathf.FloorToInt(_data.Dot * 1.5f), _data.DotDuration, _data.DotIcon);
                else 
                    enemy.ApplyDot(_data.ArmorThroughMalus, _data.Dot, _data.DotDuration, _data.DotIcon);

                if (_slowDown)
                    enemy.ApplySlowDown(10, 2f);
            }
        }
    }
}
