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


    [Header("Prefab")]
    [SerializeField]
    private Spawner _spawnerPrefab;
    private readonly List<Spawner> _spawners = new List<Spawner>();


    [SerializeField]
    private List<Path> _availablePaths;


    [Header("Components")]
    [SerializeField]
    private RessourceController _ressourceController;


    private int _waveIndex = 0;

    [Header("Enemy-related prefabs")]
    [SerializeField]
    private List<Enemy> _enemiesAvailables;

    [SerializeField]
    private EnemyPool _enemyPoolPrefab;


    private readonly List<EnemyPool> _enemyPools = new List<EnemyPool>();


    private void Start()
    {
        for (int i = 0; i < _enemiesAvailables.Count; i++)
        {
            EnemyPool newEnemyPool = Instantiate(_enemyPoolPrefab, transform);
            newEnemyPool.Initialize(_enemiesAvailables[i].gameObject, _ressourceController);

            _enemyPools.Add(newEnemyPool);
        }

        _waveText.text = _waveIndex + " / " + _level.GetWaveCount();
        StartCoroutine(Delay());
    }


    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        StartWave();
    }


    private void StartWave()
    {
        _waveText.text = _waveIndex + " / " + _level.GetWaveCount();

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


    //Coroutine used to delay next wave
    private IEnumerator DelayWave()
    {
        if (_waveIndex + 1 < _level.GetWaveCount())
        {
            _waveIndex++;

            yield return new WaitForSeconds(_level.GetWave(_waveIndex).GetTimeBeforeNextWave());

            StartWave();
        }
        else
            Debug.Log("fini");
    }
}