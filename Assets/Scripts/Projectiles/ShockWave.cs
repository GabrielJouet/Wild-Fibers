using UnityEngine;

/*
 * Shock Wave is the projectile of Shock tower
 */
public class ShockWave : Projectile
{
    private Vector3 _previousScale;

    private bool _hitGroundHarder;

    private float _range;
    
    
    //Method used to initialize class (like a constructor)
    public void Initialize(Attack newData, ProjectilePool newPool, Transform newTransform, bool hitGroundHarder, float range)
    {
        transform.position = newTransform.position;
        _data = newData;
        _projectilePool = newPool;
        _previousScale = new Vector3(0, 0, 1);

        _range = range;
        _hitGroundHarder = hitGroundHarder;
        transform.localScale = _previousScale;
    }


    //Update method, called every frame
    protected override void Update()
    {
        if (transform.localScale.x < _range)
        {
            _previousScale.x += _projectileSpeed * Time.deltaTime;
            _previousScale.y += _projectileSpeed * Time.deltaTime;

            transform.localScale = _previousScale;
        }
        else
            StopProjectile();
    }


    //On trigger method, called when an enemy is reached
    //
    //Parameter => collision, the collision object triggered
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy newEnemy))
        {
            if (_hitGroundHarder)
                newEnemy.TakeDamage(new Attack(_data.Damage + (!newEnemy.Flying ? 2 : 1), _data.ArmorThrough, _data.DotDuration, _data.ArmorThroughMalus, _data.DotDamage));
            else 
                newEnemy.TakeDamage(_data);
        }
    }
}