using UnityEngine;

/*
 * Shock Wave is the projectile of Shock tower
 */
public class ShockWave : Projectile
{
    private Vector3 _previousScale;

    private bool _hitGroundHarder;


    //Method used to initialize class (like a constructor)
    public void Initialize(TowerData newData, ProjectilePool newPool, Transform newTransform, bool hitGroundHarder)
    {
        transform.position = newTransform.position;
        _data = newData;
        _projectilePool = newPool;
        _previousScale = new Vector3(0, 0, 1);

        _hitGroundHarder = hitGroundHarder;
        transform.localScale = _previousScale;
    }


    //Update method, called every frame
    protected override void Update()
    {
        if (transform.localScale.x < _data.Range)
        {
            _previousScale.x += _data.ProjectileSpeed * Time.deltaTime;
            _previousScale.y += _data.ProjectileSpeed * Time.deltaTime;

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
            newEnemy.TakeDamage(_data.ArmorThrough,  _data.Damage + (!newEnemy.Flying && _hitGroundHarder ? 2 : 0));
    }
}