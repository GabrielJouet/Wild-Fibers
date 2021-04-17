using UnityEngine;

public class Twig : Projectile
{
    private bool _applyDot;

    //Method used to initialize class (like a constructor)
    public void Initialize(TowerData newData, Enemy newEnemy, ProjectilePool newPool, Transform newTransform, bool applyDot)
    {
        _enemyTracked = newEnemy;
        transform.position = newTransform.position;
        _data = newData;
        _projectilePool = newPool;

        _applyDot = applyDot;
    }


    /// <summary>
    /// Method called to hurt an enemy.
    /// </summary>
    /// <param name="enemy">The related enemy</param>
    protected override void AttackEnemy(Enemy enemy)
    {
        if (enemy != null)
        {
            if (_applyDot)
                enemy.ApplyDot(0, 1, 3, _data.DotIcon);

            enemy.TakeDamage(_data.ArmorThrough, _data.Damage);
        }
    }
}