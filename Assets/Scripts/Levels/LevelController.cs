using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    //Level loaded
    [SerializeField]
    private Level _level;


    [SerializeField]
    private Spawner _spawnerPrefab;
    private List<Spawner> _spawners = new List<Spawner>();


    [SerializeField]
    private List<Path> _availablePaths;

    [SerializeField]
    private RessourceController _ressourceController;

    [SerializeField]
    private EnemiesController _enemiesController;

    private int _waveIndex = 0;



    private void Start()
    {
        Debug.Log("Waves number " + _level.GetWaveCount());
        Debug.Log("Waves " + _waveIndex + " / " + _level.GetWaveCount());

        StartCoroutine(Delay());
    }


    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        StartWave();
    }



    private void StartWave()
    {
        int spawnerLeft = _level.GetWave(_waveIndex).GetNumberOfEnemyGroup() - _spawners.Count;

        //We instantiate enough spawner for each enemy group
        for(int i = 0; i < spawnerLeft; i ++)
            _spawners.Add(Instantiate(_spawnerPrefab, transform));

        //And we give them instructions
        for (int i = 0; i < _level.GetWave(_waveIndex).GetNumberOfEnemyGroup(); i++)
            _spawners[i].SetNewGroup(_level.GetWave(_waveIndex).GetEnemyGroup(i), this, _availablePaths, _ressourceController, _enemiesController);
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