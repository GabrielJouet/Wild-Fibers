using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    /// <summary>
    /// Enemy pool that contains every enemies in storage.
    /// </summary>
    [SerializeField]
    private EnemyPool _enemyPoolPrefab;
    private readonly List<EnemyPool> _enemyPools = new List<EnemyPool>();

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

    /// <summary>
    /// Resource controller.
    /// </summary>
    private RessourceController _ressourceController;


    /// <summary>
    /// Awake method used when object is initialized.
    /// </summary>
    private void Awake()
    {
        if (FindObjectsOfType<PoolController>().Length > 1)
            Destroy(gameObject);

        TowerPool = Instantiate(_towerPoolPrefab, transform);
    }


    /// <summary>
    /// Method called everytime a level is loaded.
    /// </summary>
    public void ReInitialize()
    {
        _ressourceController = FindObjectOfType<RessourceController>();

        foreach (EnemyPool current in _enemyPools)
            current.RessourceController = _ressourceController;
    }


    #region Pools related
    /// <summary>
    /// Recover one enemy pool.
    /// </summary>
    /// <param name="wantedEnemy">The enemy type we want to recover</param>
    /// <returns>The wanted enemy pool</returns>
    public EnemyPool RecoverEnemyPool(Enemy wantedEnemy)
    {
        foreach (EnemyPool current in _enemyPools)
            if (current.Enemy.Name == wantedEnemy.Name)
                return current;

        EnemyPool buffer = Instantiate(_enemyPoolPrefab, transform);
        buffer.Initialize(wantedEnemy, _ressourceController);
        _enemyPools.Add(buffer);

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
    

    /// <summary>
    /// Method called by level controller or change scene when the level is either loading or unloading.
    /// </summary>
    public void DesactivateEntities()
    {
        TowerPool.DesactivateTowers();

        foreach (ProjectilePool current in _projectilePools)
            current.DesactivateProjectiles();

        foreach (EnemyPool current in _enemyPools)
            current.DesactivateEnemies();
    }
    #endregion
}
