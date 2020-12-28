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
    private readonly List<TowerPool> _towerPools = new List<TowerPool>();


    private RessourceController _ressourceController;



    private void Awake()
    {
        _ressourceController = GetComponent<RessourceController>();
    }


    #region Pools related
    /// <summary>
    /// Method used to spawn enemy pools.
    /// </summary>
    public void SpawnEnemyPools(List<Enemy> allEnemies)
    {
        foreach (Enemy current in allEnemies)
        {
            if (current.TryGetComponent(out Boss spawnable))
            {
                bool result = false;
                foreach (EnemyPool currentPool in EnemyPools)
                {
                    if (currentPool.Enemy.GetType() == spawnable.Spawnling.GetType())
                    {
                        result = true;
                        break;
                    }
                }

                if (!result)
                    SpawnOneEnemyPool(spawnable.Spawnling);
            }

            SpawnOneEnemyPool(current);
        }
    }


    /// <summary>
    /// Method used to spawn one enemy pool.
    /// </summary>
    /// <param name="currentPrefab">The enemy prefab that will be controlled by this pool</param>
    private void SpawnOneEnemyPool(Enemy currentPrefab)
    {
        EnemyPool newEnemyPool = Instantiate(_enemyPoolPrefab, transform);
        newEnemyPool.Initialize(currentPrefab, _ressourceController);

        EnemyPools.Add(newEnemyPool);
    }


    /// <summary>
    /// Method used to spawn projectile pools.
    /// </summary>
    public void SpawnProjectilePools(List<TowerData> allTowers)
    {
        foreach (TowerData current in allTowers)
        {
            SpawnOneTowerPool(current);
            if (_projectilePools != null)
            {
                bool result = false;
                foreach (ProjectilePool currentPool in _projectilePools)
                {
                    if (current.Projectile.GetComponent<Projectile>().GetType() == currentPool.Projectile.GetType())
                    {
                        result = true;
                        break;
                    }
                }

                if (!result)
                    SpawnOneProjectilePool(current);
            }
            else
                SpawnOneProjectilePool(current);
        }
    }


    /// <summary>
    /// Method used to spawn one projectile pool.
    /// </summary>
    /// <param name="currentPrefab">The projectile prefab that will be controlled by this pool</param>
    private void SpawnOneProjectilePool(TowerData currentPrefab)
    {
        ProjectilePool newPool = Instantiate(_projectilePoolPrefab, transform);
        newPool.Projectile = currentPrefab.Projectile.GetComponent<Projectile>();

        _projectilePools.Add(newPool);
    }


    /// <summary>
    /// Method used to spawn one tower pool.
    /// </summary>
    /// <param name="currentPrefab">The tower prefab that will be controlled by this pool</param>
    private void SpawnOneTowerPool(TowerData currentPrefab)
    {
        TowerPool newPool = Instantiate(_towerPoolPrefab, transform);
        newPool.Tower = currentPrefab;

        _towerPools.Add(newPool);
    }


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

        return null;
    }


    /// <summary>
    /// Recover one projectile pool.
    /// </summary>
    /// <param name="wantedProjectile">The projectile type we want to recover</param>
    /// <returns>The wanted projectile pool</returns>
    public ProjectilePool RecoverProjectilePool(Projectile wantedProjectile)
    {
        foreach (ProjectilePool current in _projectilePools)
            if (current.Projectile.GetType() == wantedProjectile.GetType())
                return current;

        return null;
    }


    /// <summary>
    /// Recover one tower pool.
    /// </summary>
    /// <param name="wantedTower">The tower type we want to recover</param>
    /// <returns>The wanted tower pool</returns>
    public TowerPool RecoverTowerPool(TowerData wantedTower)
    {
        foreach (TowerPool current in _towerPools)
            if (current.Tower.Name == wantedTower.Name)
                return current;

        return null;
    }
    #endregion
}
