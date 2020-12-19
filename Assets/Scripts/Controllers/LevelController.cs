using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to store and manage level resources.
/// </summary>
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(RessourceController))]
public class LevelController : MonoBehaviour
{
    [Header("Level Parameters")]

    /// <summary>
    /// Level loaded.
    /// </summary>
    [SerializeField]
    private Level _level;
    public Level LoadedLevel { get => _level; }

    /// <summary>
    /// Does the level is ended?
    /// </summary>
    public bool Ended { get; set; }

    /// <summary>
    /// Current level index.
    /// </summary>
    public int LevelIndex { get => _level.Number; }


    [Header("UI related")]

    /// <summary>
    /// Text component that displays wave number.
    /// </summary>
    [SerializeField]
    private Text _waveText;

    /// <summary>
    /// Game over object that is used when the player finish the level.
    /// </summary>
    [SerializeField]
    private GameOverScreen _gameOverScreen;


    [Header("Prefab")]

    /// <summary>
    /// Entity spawner prefab.
    /// </summary>
    [SerializeField]
    private Spawner _spawnerPrefab;
    private readonly List<Spawner> _spawners = new List<Spawner>();

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
    private readonly List<TowerPool> _towerPools = new List<TowerPool>();


    [Header("Components")]

    /// <summary>
    /// Next wave button component.
    /// </summary>
    [SerializeField]
    private NextWaveButton _nextWaveButton;

    /// <summary>
    /// Bezier curve paths.
    /// </summary>
    [SerializeField]
    private List<RandomPath> _availablePath;


    /// <summary>
    /// Resource controller used to track lives and gold.
    /// </summary>
    private RessourceController _ressourceController;

    /// <summary>
    /// Time between each next wave button display
    /// </summary>
    private readonly float _timeBetweenNextWaveButtonDisplay = 5f;

    /// <summary>
    /// Player controller component.
    /// </summary>
    private PlayerController _playerController;

    /// <summary>
    /// Current wave index
    /// </summary>
    private int _waveIndex = 0;



    /// <summary>
    /// Awake method used for initialization.
    /// </summary>
    private void Awake()
    {
        _ressourceController = GetComponent<RessourceController>();
        _playerController = GetComponent<PlayerController>();
    }


    /// <summary>
    /// Start method called after Awake.
    /// </summary>
    private void Start()
    {
        _waveText.text = 0 + " / " + _level.Waves.Count;
        SpawnEnemyPools();
        SpawnProjectilePools();
    }


    #region Pools related
    /// <summary>
    /// Method used to spawn enemy pools.
    /// </summary>
    private void SpawnEnemyPools()
    {
        foreach (Enemy current in _level.Enemies)
        {
            if (current.TryGetComponent(out Boss spawnable))
            {
                bool result = false;
                foreach (EnemyPool currentPool in _enemyPools)
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

        _enemyPools.Add(newEnemyPool);
    }


    /// <summary>
    /// Method used to spawn projectile pools.
    /// </summary>
    private void SpawnProjectilePools()
    {
        foreach (Tower current in _playerController.Towers)
        {
            SpawnOneTowerPool(current);
            if (_projectilePools != null)
            {
                bool result = false;
                foreach (ProjectilePool currentPool in _projectilePools)
                {
                    if(current.Projectile.GetComponent<Projectile>().GetType() == currentPool.Projectile.GetType())
                    {
                        result = true;
                        break;
                    }
                }

                if(!result)
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
    private void SpawnOneProjectilePool(Tower currentPrefab)
    {
        ProjectilePool newPool = Instantiate(_projectilePoolPrefab, transform);
        newPool.Projectile = currentPrefab.Projectile.GetComponent<Projectile>();

        _projectilePools.Add(newPool);
    }


    /// <summary>
    /// Method used to spawn one tower pool.
    /// </summary>
    /// <param name="currentPrefab">The tower prefab that will be controlled by this pool</param>
    private void SpawnOneTowerPool(Tower currentPrefab)
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
    public EnemyPool RecoverPool(Enemy wantedEnemy)
    {
        foreach (EnemyPool current in _enemyPools)
            if (current.Enemy.GetType() == wantedEnemy.GetType())
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
    public TowerPool RecoverTowerPool(Tower wantedTower)
    {
        foreach (TowerPool current in _towerPools)
            if (current.Tower.GetType() == wantedTower.GetType())
                return current;

        return null;
    }
    #endregion


    /// <summary>
    /// Method called when the new wave button is used.
    /// </summary>
    /// <param name="timeLeft">How much time left is available?</param>
    public void StartWaveViaButton(float timeLeft)
    {
        StartWave();

        //If there is time left, we gie money to player based on time left
        if (timeLeft > 0)
            _ressourceController.AddGold(Mathf.FloorToInt(_level.Waves[_waveIndex].BonusGold * (timeLeft / _level.Waves[_waveIndex].TimeWave)));
    }


    /// <summary>
    /// Method called when we start a new wave.
    /// </summary>
    private void StartWave()
    {
        _waveText.text = (_waveIndex + 1) + " / " + _level.Waves.Count;

        int spawnerLeft = _level.Waves[_waveIndex].EnemyGroups.Count - _spawners.Count;

        int i;
        for(i = 0; i < spawnerLeft; i ++)
            _spawners.Add(Instantiate(_spawnerPrefab, transform));

        EnemyPool bufferPool = null;
        i = 0;
        foreach(EnemyGroup current in _level.Waves[_waveIndex].EnemyGroups)
        {
            foreach (EnemyPool buffer in _enemyPools)
                if (buffer.Enemy == current.Enemy)
                    bufferPool = buffer;

            _spawners[i].SetNewGroup(_availablePath[current.Path], current, this, bufferPool);
            i++;
        }
    }


    /// <summary>
    /// Method called when the wave is finished.
    /// </summary>
    public void EndWave()
    {
        bool result = true;

        foreach (Spawner current in _spawners)
            if (!current.WaveFinished)
                result = false;

        if (result)
            StartCoroutine(DelayWave());
    }


    /// <summary>
    /// Method used when the level is finished.
    /// </summary>
    public void EndLevel()
    {
        bool result = true;

        foreach (Spawner current in _spawners)
            if (!current.EnemiesKilled)
                result = false;

        if (result)
            _gameOverScreen.Activate(true);

        Ended = result;
    }


    /// <summary>
    /// Coroutine used to delay the next wave.
    /// </summary>
    /// <returns>Yield the time between next wave display</returns>
    private IEnumerator DelayWave()
    {
        //If there is another wave after that one
        if (_waveIndex + 1 < _level.Waves.Count)
        {
            _waveIndex++;
            yield return new WaitForSeconds(_timeBetweenNextWaveButtonDisplay);

            _nextWaveButton.ActivateNewWaveButton(_level.Waves[_waveIndex].TimeWave);
        }
        else
            foreach (Spawner current in _spawners)
                current.NotifyPool();
    }
}