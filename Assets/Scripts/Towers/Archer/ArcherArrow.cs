using UnityEngine;

/*
 * Projectile of the archer tower
 */
public class ArcherArrow : Projectile
{
    //Speed of movement
    [SerializeField]
    private float _speed;


    //Attack variables
    private float _damage;
    private float _armorThrough;


    //Enemy to attack
    private Enemy _enemyToTrack;

    private Vector3 _goalPosition;



    //Method used to initialize class (like a constructor)
    //
    //Parameters => newDamage, Amount of damage done on attack
    //              newArmorThrough, armor malus done on attack
    //              newEnemy, new enemy to track
    //              newParent, new parent tower
    //              newPool, new projectile Pool
    public void Initialize(float newDamage, float newArmorThrough, Enemy newEnemy, Transform newParent, ProjectilePool newPool)
    {
        transform.position = newParent.position;
        _enemyToTrack = newEnemy;
        _damage = newDamage;
        _projectilePool = newPool;
        _armorThrough = newArmorThrough;
    }


    //Update method, called every frame
    private void Update()
    {
        if (_enemyToTrack != null)
            TrackEnemy();
        else if (FollowPoint(_goalPosition))
            StopProjectile();
    }


    //Method used to track an enemy moving 
    private void TrackEnemy()
    {
        if (_enemyToTrack.gameObject.activeSelf)
        {
            if (FollowPoint(_enemyToTrack.GetDamagePosition()))
            {
                _enemyToTrack.TakeDamage(_damage, _armorThrough);
                StopProjectile();
            }
        }
        else
        {
            _goalPosition = _enemyToTrack.GetDamagePosition();
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