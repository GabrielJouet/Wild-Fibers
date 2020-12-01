using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This class controls every level related elements
 */
public class LevelController : MonoBehaviour
{
    [Header("Constant")]
    [Range(3, 10)]
    //Time between an end of wave and next wave display
    [SerializeField]
    private float _timeBetweenNextWaveButtonDisplay = 5f;

    [Header("Level Parameters")]
    //Loaded level with parameters
    [SerializeField]
    private Level _level;

    public bool Ended { get; set; }

    public int LevelIndex { get => _level.Number; }

    public Level LoadedLevel { get => _level; }


    [Header("UI related")]
    //Text on UI that shows the current and max numbers of waves
    [SerializeField]
    private Text _waveText;
    //Game over screen script
    [SerializeField]
    private GameOverScreen _gameOverScreen;


    [Header("Prefab")]
    //Spawner used to spawn enemies
    [SerializeField]
    private Spawner _spawnerPrefab;
    //List of available spawners to use
    private readonly List<Spawner> _spawners = new List<Spawner>();
    //An enemy pool prefab, each enemy pool contains enemies references
    [SerializeField]
    private EnemyPool _enemyPoolPrefab;
    //List of available enemy pools to use
    private readonly List<EnemyPool> _enemyPools = new List<EnemyPool>();
    [SerializeField]
    private ProjectilePool _projectilePoolPrefab;
    private readonly List<ProjectilePool> _projectilePools = new List<ProjectilePool>();
    [SerializeField]
    private TowerPool _towerPoolPrefab;
    private readonly List<TowerPool> _towerPools = new List<TowerPool>();


    [Header("Components")]
    //Ressource controller that controls lives and money
    [SerializeField]
    private RessourceController _ressourceController;
    //Next Wave Button used in wave generation
    [SerializeField]
    private NextWaveButton _nextWaveButton;
    //Available Paths in level
    [SerializeField]
    private List<RandomPath> _availablePath;
    [SerializeField]
    private PlayerController _playerController;


    //Index for saving wave progress
    private int _waveIndex = 0;



    //Start Method
    //Called when the game object is initialized
    private void Start()
    {
        _waveText.text = 0 + " / " + _level.Waves.Count;
        SpawnEnemyPools();
        SpawnProjectilePools();
    }


    //Method used to spawn every needed enemy pool
    //Each enemy type will need its own pool
    private void SpawnEnemyPools()
    {
        foreach (Enemy current in _level.Enemies)
        {
            //TO CHANGE SHOULD BE AN INTERFACE INSTEAD
            if (current.TryGetComponent(out Boss currentBoos))
            {
                bool result = false;
                foreach (EnemyPool currentPool in _enemyPools)
                {
                    if (currentPool.Enemy.GetType() == currentBoos.Spawnling.GetType())
                    {
                        result = true;
                        break;
                    }
                }

                if (!result)
                    SpawnOneEnemyPool(currentBoos.Spawnling);
            }

            SpawnOneEnemyPool(current);
        }
    }


    //Method used to spawn one enemy pool
    private void SpawnOneEnemyPool(Enemy currentPrefab)
    {
        EnemyPool newEnemyPool = Instantiate(_enemyPoolPrefab, transform);
        newEnemyPool.Initialize(currentPrefab, _ressourceController);

        _enemyPools.Add(newEnemyPool);
    }


    //Method used to spawn every needed projectile pool
    //Each projectile type will need its own pool
    private void SpawnProjectilePools()
    {
        foreach (Tower current in _playerController.GetTowers())
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


    //Method used to spawn one projectile pool
    private void SpawnOneProjectilePool(Tower currentPrefab)
    {
        ProjectilePool newPool = Instantiate(_projectilePoolPrefab, transform);
        newPool.Initialize(currentPrefab.Projectile);

        _projectilePools.Add(newPool);
    }


    //Method used to spawn one tower pool
    private void SpawnOneTowerPool(Tower currentPrefab)
    {
        TowerPool newPool = Instantiate(_towerPoolPrefab, transform);
        newPool.Initialize(currentPrefab);

        _towerPools.Add(newPool);
    }


    //Method called when the next wave button is pressed
    //
    //Parameters => The time left before the next wave
    public void StartWaveViaButton(float timeLeft)
    {
        StartWave();

        //If there is time left, we gie money to player based on time left
        if (timeLeft > 0)
            _ressourceController.AddGold(Mathf.FloorToInt(_level.Waves[_waveIndex].BonusGold * (timeLeft / _level.Waves[_waveIndex].TimeWave)));
    }


    //Method used to start a brand new wave with level parameters
    private void StartWave()
    {
        _waveText.text = (_waveIndex + 1) + " / " + _level.Waves.Count;

        int spawnerLeft = _level.Waves[_waveIndex].EnemyGroups.Count - _spawners.Count;

        int i;
        //We instantiate enough spawner for each enemy group
        for(i = 0; i < spawnerLeft; i ++)
            _spawners.Add(Instantiate(_spawnerPrefab, transform));

        //And we give them instructions
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


    //Method called by spawner when they finish spawning enemies
    public void EndWave()
    {
        bool result = true;

        //If one of the spawner has not done its wave yet
        foreach (Spawner current in _spawners)
            if (!current.WaveFinished)
                result = false;

        //If every spawner has called the level controller
        //We launch the next wave
        if (result)
            StartCoroutine(DelayWave());
    }


    //Method used when a new wave should be called but there is no other ones
    public void EndLevel()
    {
        bool result = true;

        //If one of the spawner has not done its wave yet
        foreach (Spawner current in _spawners)
            if (!current.WaveFinished)
                result = false;

        //If the wave is finished and every enemy is dead 
        if (result)
            _gameOverScreen.Activate(true);

        Ended = result;
    }


    //Coroutine used to delay next wave by a short amount of time
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


    //Getter
    public EnemyPool RecoverPool(Enemy wantedEnemy)
    {
        foreach (EnemyPool current in _enemyPools)
            if (current.Enemy.GetType() == wantedEnemy.GetType())
                return current;

        return null;
    }

    public ProjectilePool RecoverProjectilePool(Projectile wantedProjectile)
    {
        foreach (ProjectilePool current in _projectilePools)
            if (current.Projectile.GetType() == wantedProjectile.GetType())
                return current;

        return null;
    }

    public TowerPool RecoverTowerPool(Tower wantedTower)
    {
        foreach (TowerPool current in _towerPools)
            if (current.Tower.GetType() == wantedTower.GetType())
                return current;

        return null;
    }
}