using UnityEngine;

public class Twig : Projectile
{
    //Method used to initialize class (like a constructor)
    public void Initialize(Attack newData, Enemy newEnemy, ProjectilePool newPool, Transform newTransform, bool applyDot)
    {
        _enemyTracked = newEnemy;
        transform.position = newTransform.position;
        _data = newData;
        _projectilePool = newPool;

        if (applyDot)
        {
            _data.ArmorThroughMalus = 0;
            _data.DotDamage = 1;
            _data.DotDuration = 3;
        }
    }
}