using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private Level _level;

    private int _waveIndex = 0;

    [SerializeField]
    private Spawner _spawnerPrefab;
    private List<Spawner> _spawners;


    private void Start()
    {
        Debug.Log("Waves number " + _level.GetWaveCount());
        Debug.Log("Waves " + _waveIndex + " / " + _level.GetWaveCount());

        StartWave();
    }


    private void StartWave()
    {
        int spawnerLeft = _level.GetWave(_waveIndex).GetNumberOfEnemyGroup() - _spawners.Count;

        for(int i = 0; i < spawnerLeft; i ++)
            InstantiateSpawners();

        for(int i = 0 ; i < _level.GetWave(_waveIndex).GetNumberOfEnemyGroup(); i ++)
            _spawners[i].SetNewGroup(_level.GetWave(_waveIndex).GetEnemyGroup(i));
    }


    private void InstantiateSpawners()
    {
        _spawners.Add(Instantiate(_spawnerPrefab));
    }


    public void EndWave()
    {
        bool result = true;

        foreach (Spawner current in _spawners)
            if (!current.GetCanSpawn())
                result = false;

        if(result)
            StartCoroutine(DelayWave());
    }


    private IEnumerator DelayWave()
    {
        if (_waveIndex < _level.GetWaveCount())
        {
            _waveIndex++;

            yield return new WaitForSeconds(_level.GetWave(_waveIndex).GetTimeBeforeNextWave());

            StartWave();
        }
        else
            Debug.Log("fini");
    }
}