using UnityEngine;

/*
 * Class used alongside Mark Tower, it is the mark tower dot projectile
 * TO CHANGE, SHOULD BE DOT INSTEAD
 */
public class MarkDot : Projectile
{
    //Speed of movement of projectile
    [SerializeField]
    private float _speed;

    //Which icon will be displayed in the enemy?
    [SerializeField]
    private Sprite _dotIcon;


    //Variables given by the mark tower
    private float _armorThroughMalus;
    private float _damageOverTime;
    private float _dotDuration;

    private float _damage;
    private float _armorThrough;


    //Enemy tracked
    private Enemy _enemyToTrack;

    private Vector3 _goalPosition;



    //Method used to initialize class (like a constructor)
    //
    //Parameters => newEnemy, new enemy to track
    //              newParent, new parent tower
    //              newPool, new Projectile pool
    public void Initialize(TowerData newData, Enemy newEnemy, Transform newParent, ProjectilePool newPool)
    {
        transform.position = newParent.position;
        _enemyToTrack = newEnemy;
        _damage = newData.Damage;
        _projectilePool = newPool;
        _armorThrough = newData.ArmorThrough;

        _armorThroughMalus = newData.ArmorThroughMalus;
        _damageOverTime = newData.Dot;
        _dotDuration = newData.DotDuration;
    }


    //Update method, called every frame
    private void Update()
    {
        if (_enemyToTrack != null)
            TrackEnemy();
        else if(FollowPoint(_goalPosition))
            StopProjectile();
    }


    //Method used to track an enemy moving 
    private void TrackEnemy()
    {
        if (_enemyToTrack.gameObject.activeSelf)
        {
            if (FollowPoint(_enemyToTrack.DamagePosition))
            {
                _enemyToTrack.TakeDamage(_damage, _armorThrough);

                if (_enemyToTrack.gameObject.activeSelf)
                    _enemyToTrack.ApplyDot(_armorThroughMalus, _damageOverTime, _dotDuration, _dotIcon);

                StopProjectile();
            }
        }
        else
        {
            _goalPosition = _enemyToTrack.DamagePosition;
            _enemyToTrack = null;
        }
    }


    //Method used to follow a point moving in space
    //
    //Parameter => the point position in a vector 3
    private bool FollowPoint(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, _speed * Time.deltaTime);

        Vector3 vectorToTarget = position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        return (transform.position - position).magnitude < 0.025f;
    }
}