using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [Header("Level Parameters")]
    [SerializeField]
    private Level _level;


    [Header("UI related")]
    [SerializeField]
    private Text _waveText;
    [SerializeField]
    private GameOverScreen _gameOverScreen;


    [Header("Prefab")]
    [SerializeField]
    private Spawner _spawnerPrefab;
    private readonly List<Spawner> _spawners = new List<Spawner>();
    [SerializeField]
    private List<Enemy> _enemiesAvailables;
    [SerializeField]
    private EnemyPool _enemyPoolPrefab;
    private readonly List<EnemyPool> _enemyPools = new List<EnemyPool>();


    [Header("Available Paths")]
    [SerializeField]
    private List<Path> _availablePaths;


    [Header("Components")]
    [SerializeField]
    private RessourceController _ressourceController;
    [SerializeField]
    private NextWaveButton _nextWaveButton;
    [SerializeField]
    private PauseController _pauseController;


    private int _waveIndex = 0;


    private void Start()
    {
        SpawnEnemyPools();
    }



    private void SpawnEnemyPools()
    {
        for (int i = 0; i < _enemiesAvailables.Count; i++)
        {
            EnemyPool newEnemyPool = Instantiate(_enemyPoolPrefab, transform);
            newEnemyPool.Initialize(_enemiesAvailables[i].gameObject, _ressourceController);

            _enemyPools.Add(newEnemyPool);
        }
    }


    public void StartWaveViaButton(float timeLeft)
    {
        StartWave();

        if (timeLeft > 0)
            _ressourceController.AddGold(Mathf.FloorToInt(_level.GetWave(_waveIndex).GetGoldBonus() * (timeLeft / _level.GetWave(_waveIndex).GetTimeBeforeNextWave())));
    }


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


    //Method used by Spawner when the wave is done
    public void EndWave()
    {
        bool result = true;

        //If one of the spawner has not done its wave yet
        foreach (Spawner current in _spawners)
            if (!current.GetWaveFinished())
                result = false;

        if (result)
            StartCoroutine(DelayWave());
    }


    public void EndLevel()
    {
        bool result = true;

        //If one of the spawner has not done its wave yet
        foreach (Spawner current in _spawners)
            if (!current.GetEnemiesKilled())
                result = false;

        if (result)
            _gameOverScreen.Activate(true);
    }


    //Coroutine used to delay next wave
    private IEnumerator DelayWave()
    {
        if (_waveIndex + 1 < _level.GetWaveCount())
        {
            _waveIndex++;

            yield return new WaitForSeconds(3);

            _nextWaveButton.ActivateNewWaveButton(_level.GetWave(_waveIndex).GetTimeBeforeNextWave());
        }
        else
        {
            foreach (Spawner current in _spawners)
                current.NotifyPool();
        }
    }
}