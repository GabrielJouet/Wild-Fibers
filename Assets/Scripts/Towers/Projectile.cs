using UnityEngine;

/// <summary>
/// Class used by projectile like object.
/// </summary>
public class Projectile : MonoBehaviour
{
    /// <summary>
    /// The related pool.
    /// </summary>
    protected ProjectilePool _projectilePool;


    /// <summary>
    /// Method called when a projectile hurt an enemy.
    /// </summary>
    protected void StopProjectile()
    {
        gameObject.SetActive(false);
        _projectilePool.AddOneProjectile(this);
    }
}
