using UnityEngine;

/*
 * Shock Wave is the projectile of Shock tower
 */
public class BrambleShockWave : Projectile
{
    //Time between start and end wave in second
    [SerializeField]
    private float _expandTime;


    //Max range of wave
    private float _maxRange;

    //Attack variables
    private float _damage;
    private float _armorThrough;
    
    //How much size expansion per frame?
    private float _expansionRate;

    private Vector3 _previousScale;



    //Method used to initialize class (like a constructor)
    //
    //Parameters => newDamage, Amount of damage done on attack
    //              newArmorThrough, armor malus done on attack
    //              newEnemy, new enemy to track
    //              newParent, new parent tower
    //              newPool, new projectile pool
    //              newRange, new wave range
    public void Initialize(float newDamage, float newArmorThrough, Transform newParent, ProjectilePool newPool, float newRange)
    {
        _previousScale = new Vector3(0, 0, 1);

        transform.localScale = _previousScale;
        transform.position = newParent.position;

        _damage = newDamage;
        _projectilePool = newPool;
        _armorThrough = newArmorThrough;
        _maxRange = newRange;

        _expansionRate = _maxRange / _expandTime;
    }


    //Update method, called every frame
    private void Update()
    {
        if (transform.localScale.x < _maxRange)
        {
            _previousScale.x += _expansionRate * Time.deltaTime;
            _previousScale.y += _expansionRate * Time.deltaTime;

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
            newEnemy.TakeDamage(_damage, _armorThrough);
    }
}