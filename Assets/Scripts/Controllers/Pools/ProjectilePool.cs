using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to store and reactivate old projectiles.
/// </summary>
public class ProjectilePool : MonoBehaviour
{
    /// <summary>
    /// Prefab used in this pool.
    /// </summary>
    public Projectile Projectile { get; set; }

    /// <summary>
    /// Pool of projectiles.
    /// </summary>
    private readonly Stack<Projectile> _projectilePool = new Stack<Projectile>();

    /// <summary>
    /// Projectiles instantiated but activated.
    /// </summary>
    private readonly List<Projectile> _projectileUsed = new List<Projectile>();



    /// <summary>
    /// Method used to get a new projectile.
    /// </summary>
    /// <returns>A projectile from the pool</returns>
    public Projectile GetOneProjectile()
    {
        Projectile newProjectile = _projectilePool.Count > 0 ? _projectilePool.Pop() : Instantiate(Projectile, transform);
        newProjectile.gameObject.SetActive(true);

        _projectileUsed.Add(newProjectile);

        return newProjectile;
    }


    /// <summary>
    /// Method used to add a desactivated projectile to the pool.
    /// </summary>
    /// <param name="newProjectile">The desactivated projectile</param>
    public void AddOneProjectile(Projectile newProjectile)
    {
        _projectileUsed.Remove(newProjectile);

        newProjectile.gameObject.SetActive(false);
        _projectilePool.Push(newProjectile);
    }


    /// <summary>
    /// Method called by pool controller when the level is either loading or unloading.
    /// </summary>
    public void DesactivateProjectiles()
    {
        List<Projectile> bufferList = new List<Projectile>(_projectileUsed);

        foreach (Projectile current in bufferList)
            AddOneProjectile(current);
    }
}
