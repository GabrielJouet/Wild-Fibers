using UnityEngine;

public class ToxicLeaf : Projectile
{
    protected bool _following;

    protected float _angle;



    //Method used to initialize class (like a constructor)
    public void Initialize(TowerData newData, ProjectilePool newPool, Vector2 newPosition)
    {
        _following = false;
        _angle = Random.Range(0, 360);

        StopAllCoroutines();

        _data = newData;
        _projectilePool = newPool;

        transform.position = newPosition;
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
            transform.localPosition = new Vector3(Mathf.Sin(_angle) * 0.15f, Mathf.Cos(_angle) * 0.15f, 0);
            transform.RotateAround(transform.position, new Vector3(0, 0, 1), _data.ProjectileSpeed * 4 * Time.deltaTime);
        }
    }
}
