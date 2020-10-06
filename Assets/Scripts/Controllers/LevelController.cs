using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This class controls every level related elements
 */
public class LevelController : MonoBehaviour
{
    [Header("Level Parameters")]
    //Loaded level with parameters
    [SerializeField]
    private Level _level;


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

    //Every available enemies in this level 
    //TO CHANGE CAN BE MORE DYNAMIC
    [SerializeField]
    private List<Enemy> _enemiesAvailables;
    //An enemy pool prefab, each enemy pool contains enemies references
    [SerializeField]
    private EnemyPool _enemyPoolPrefab;
    //List of available enemy pools to use
    private readonly List<EnemyPool> _enemyPools = new List<EnemyPool>();


    [Header("Available Paths")]
    //Available Bezier paths for enemies
    [SerializeField]
    private List<Path> _availablePaths;


    [Header("Components")]
    //Ressource controller that controls lives and money
    [SerializeField]
    private RessourceController _ressourceController;
    //Next Wave Button used in wave generation
    [SerializeField]
    private NextWaveButton _nextWaveButton;
    //Pause controller used to handle pause and stuff
    [SerializeField]
    private PauseController _pauseController;


    //Index for saving wave progress
    private int _waveIndex = 0;


    //Does the tower is currently paused? (by Pause Controller)
    protected bool _paused = false;

    //Coroutine Start Time (used if the tower is paused)
    protected DateTime _coroutineStartTime;

    //Coroutine time needed to reset
    protected float _coroutineTimeNeeded = 0f;



    //Start Method
    //Called when the game object is initialized
    private void Start()
    {
        _waveText.text = 0 + " / " + _level.GetWaveCount();
        SpawnEnemyPools();
    }


    //Method used to spawn every needed enemy pool
    //Each enemy type will need its own pool
    private void SpawnEnemyPools()
    {
        for (int i = 0; i < _enemiesAvailables.Count; i++)
        {
            EnemyPool newEnemyPool = Instantiate(_enemyPoolPrefab, transform);
            newEnemyPool.Initialize(_enemiesAvailables[i].gameObject, _ressourceController);

            _enemyPools.Add(newEnemyPool);
        }
    }


    //Method called when the next wave button is pressed
    //
    //Parameters => The time left before the next wave
    public void StartWaveViaButton(float timeLeft)
    {
        StartWave();

        //If there is time left, we gie money to player based on time left
        if (timeLeft > 0)
            _ressourceController.AddGold(Mathf.FloorToInt(_level.GetWave(_waveIndex).GetGoldBonus() * (timeLeft / _level.GetWave(_waveIndex).GetTimeBeforeNextWave())));
    }


    //Method used to start a brand new wave with level parameters
    private void StartWave()
    {
        _waveText.text = (_waveIndex + 1) + " / " + _level.GetWaveCount();

        int spawnerLeft = _level.GetWave(_waveIndex).GetNumberOfEnemyGroup() - _spawners.Count;

        //We instantiate enough spawner for each enemy group
        for(int i = 0; i < spawnerLeft; i ++)
            _spawners.Add(Instantiate(_spawnerPrefab, transform));

        //And we give them instructions
        EnemyPool bufferPool = null;
        for (int i = 0; i < _level.GetWave(_waveIndex).GetNumberOfEnemyGroup(); i++)
        {
            foreach(EnemyPool current in _enemyPools)
            {
                if (current.GetPrefab() == _level.GetWave(_waveIndex).GetEnemyGroup(i).GetEnemyUsed().gameObject)
                    bufferPool = current;
            }
            _spawners[i].SetNewGroup(_level.GetWave(_waveIndex).GetEnemyGroup(i), this, _availablePaths, bufferPool);
        }
    }


    //Method called by spawner when they finish spawning enemies
    public void EndWave()
    {
        bool result = true;

        //If one of the spawner has not done its wave yet
        foreach (Spawner current in _spawners)
            if (!current.GetWaveFinished())
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
            if (!current.GetEnemiesKilled())
                result = false;

        //If the wave is finished and every enemy is dead 
        if (result)
        {
            _gameOverScreen.gameObject.SetActive(true);
            _gameOverScreen.Activate(true);
        }
    }


    //Coroutine used to delay next wave by a short amount of time
    private IEnumerator DelayWave()
    {
        //If there is another wave after that one
        if (_waveIndex + 1 < _level.GetWaveCount())
        {
            _waveIndex++;

            _coroutineStartTime = DateTime.Now;
            _coroutineTimeNeeded = 3;
            yield return new WaitForSeconds(3);

            _nextWaveButton.ActivateNewWaveButton(_level.GetWave(_waveIndex).GetTimeBeforeNextWave());
        }
        else
            foreach (Spawner current in _spawners)
                current.NotifyPool();
    }


    //Method used by pause controller to pause level controller behavior
    public void PauseBehavior()
    {
        if (!_paused)
        {
            StopAllCoroutines();
            _coroutineTimeNeeded -= (float)(DateTime.Now - _coroutineStartTime).TotalSeconds;
        }
        else
            StartCoroutine(DelayUnPause());

        _paused = !_paused;
    }


    //Coroutine used to delay unpause after being paused
    private IEnumerator DelayUnPause()
    {
        yield return new WaitForSeconds(_coroutineTimeNeeded);
        _nextWaveButton.ActivateNewWaveButton(_level.GetWave(_waveIndex).GetTimeBeforeNextWave());
    }


    //Getter
    public int GetLevelIndex() { return _level.GetNumber(); }
}