using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RessourceController))]
public class PoolController : MonoBehaviour
{
    /// <summary>
    /// Enemy pool that contains every enemies in storage.
    /// </summary>
    [SerializeField]
    private EnemyPool _enemyPoolPrefab;
    public List<EnemyPool> EnemyPools { get; private set; } = new List<EnemyPool>();

    /// <summary>
    /// Projectile pool that contains every projectile.
    /// </summary>
    [SerializeField]
    private ProjectilePool _projectilePoolPrefab;
    private readonly List<ProjectilePool> _projectilePools = new List<ProjectilePool>();

    /// <summary>
    /// Tower pool that contains every tower.
    /// </summary>
    [SerializeField]
    private TowerPool _towerPoolPrefab;
    public TowerPool TowerPool { get; private set; }


    private RessourceController _ressourceController;



    private void Awake()
    {
        _ressourceController = GetComponent<RessourceController>();
        TowerPool = Instantiate(_towerPoolPrefab, transform);
    }


    #region Pools related
    /// <summary>
    /// Recover one enemy pool.
    /// </summary>
    /// <param name="wantedEnemy">The enemy type we want to recover</param>
    /// <returns>The wanted enemy pool</returns>
    public EnemyPool RecoverEnemyPool(Enemy wantedEnemy)
    {
        foreach (EnemyPool current in EnemyPools)
            if (current.Enemy.Name == wantedEnemy.Name)
                return current;

        EnemyPool buffer = Instantiate(_enemyPoolPrefab, transform);
        buffer.Initialize(wantedEnemy, _ressourceController);
        EnemyPools.Add(buffer);

        return buffer;
    }


    /// <summary>
    /// Recover one projectile pool.
    /// </summary>
    /// <param name="wantedProjectile">The projectile type we want to recover</param>
    /// <returns>The wanted projectile pool</returns>
    public ProjectilePool RecoverProjectilePool(Projectile wantedProjectile)
    {
        foreach (ProjectilePool current in _projectilePools)
            if (current.Projectile.name == wantedProjectile.name)
                return current;

        ProjectilePool newPool = Instantiate(_projectilePoolPrefab, transform);
        newPool.Projectile = wantedProjectile;

        _projectilePools.Add(newPool);
        return newPool;
    }
    #endregion
}
